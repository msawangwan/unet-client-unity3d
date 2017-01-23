using System;
using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator SendHostGameParameters(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game world parameters sent (host) ... [{0}]", Time.time);

            Handler<JsonEmpty> hostWorldHandler = new Handler<JsonEmpty>(
                new JsonStringWithKey(gh.GameKey, gh.GameName).Marshall()
            );

            hostWorldHandler.POST(GameHandle.LoadGameWorld.Route);

            do {
                Debug.LogFormat("-- -- [+] sending parameters ... [{0}]", Time.time);
                yield return null;
            } while (!gh.isReadyToLoad);

            if (onComplete != null) {
                onComplete(); // chained to join
            }

            Debug.LogFormat("-- [+]  game world parameters verifed (host) ... [{0}]", Time.time);
        }

        public static IEnumerator SendClientGameParameters(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("-- [+] new game world parameters sent (client) ... [{0}]", Time.time);

            Handler<JsonEmpty> clientWorldhandler = new Handler<JsonEmpty>(
                new JsonStringWithKey(gh.GameKey, gh.GameName).Marshall()
            );

            do {
                Debug.LogFormat("-- -- [+] sending parameters ... [{0}]", Time.time);
                yield return null;
            } while (!gh.isReadyToLoad);

            if (onComplete != null) {
                onComplete(); // chained to join
            }

            Debug.LogFormat("-- [+] game world parameters verifed (client) ... [{0}]", Time.time);
        }

        public static IEnumerator Join(this GameHandle gh, string playername, Action onComplete) {
            gh.LoadPlayerHandle(playername);

            Handler<GameHandle.JoinResponse> joinHandler = new Handler<GameHandle.JoinResponse>(
                new GameHandle.JoinRequest(gh.GameKey, playername, gh.isHost).Marshall()
            );

            joinHandler.POST(GameHandle.JoinGameWorld.Route); // requests all game params from the server

            GameHandle.JoinResponse onjoin = null;
            do {
                Debug.LogFormat("-- -- [+] joining ... [{0}]", Time.time);
                yield return null;
                if (joinHandler.hasLoadedResource) {
                    onjoin = joinHandler.onDone();
                    Debug.LogFormat("-- [+] server sent game world parameters {0}", onjoin.worldParameters.ToString());
                    break;
                }
            } while (true);

            gh.PlayerName = playername; // TODO: deprecate this as we handle it in the load player handler method
            gh.playerHandler.OnPlayerJoinedGame(onjoin.playerParameters);

            if (gh.isHost) {
                Debug.LogFormat("-- -- [+] joined as host");
            } else {
                Debug.LogFormat("-- -- [+] joined as client");
            }

            if (onComplete != null) {
                onComplete();
            }

            gh.LoadWorldHandle(onjoin.worldParameters);

            Debug.LogFormat("-- [+] joined game successfully");
        }

        public static IEnumerator StartPollHandler(this GameHandle gh, PollHandle ph, Action onComplete) {
            Globals.S.AppState = Globals.ApplicationState.Game;
            CameraRigController.S.EnableMovement();

            gh.LoadPollHandler(ph);
            
            yield return Wait.ForEndOfFrame;
            yield return ph.WaitForGameStart(
                gh.GameKey,
                gh.PlayerName,
                () => {
                    gh.StartCoroutine(gh.EnterPlayerSetup(null));
                    gh.StartCoroutine(gh.PollForUpdate());
                }
            );

            if (onComplete != null) {
                onComplete();
            }
        }

        public static IEnumerator EnterPlayerSetup(this GameHandle gh, Action onComplete) {
            Debug.LogFormat("[+] enter player setup");
            gh.Instance.GamePhase = Game.Phase.Ready;
            yield return new WaitUntil(
                () => {
                    if (!gh.GameHUDCtrl.View.isActiveAndEnabled) {
                        return false;
                    }
                    return true;
                }
            );

            gh.GameHUDCtrl.View.SetTextHUDMessageOverlay("ready up: select an HQ node");

            do {
                Debug.Log("[+] player readyup phase");
                yield return Wait.ForEndOfFrame;
            } while (true);
        }

        public static IEnumerator CheckNodeValidHQ(this GameHandle gh, Star star, Action onComplete) {
            Debug.LogFormat("[+] player requesting node as hq");
            if (!gh.hasHq) {
                Handler<JsonBool> checkHQHandler = new Handler<JsonBool>(
                    new GameHandle.CheckNodeHQRequest(gh.GameKey, star.AsRedisKey()).Marshall()
                );

                checkHQHandler.POST(GameHandle.ValidatePlayerHQChoiceRoute.Route);

                do {
                    Debug.Log("--[+] checking...");
                    yield return Wait.ForEndOfFrame;
                    if (checkHQHandler.hasLoadedResource) {
                        break;
                    }
                } while (true);

                JsonBool isValidHQ = checkHQHandler.onDone();
                Debug.LogFormat("[+] is valid hq choice [{0}]", isValidHQ.value);
                if (isValidHQ.value) {
                    Debug.LogFormat("[+] set hq [{0}][{1}]",gh.playerHandler.PlayerInstance.Name, star.AsRedisKey());
                    gh.hasHq = true;
                    // TODO send player ready sig??
                }
            } else {
                Debug.LogFormat("[+] player already has hq");
            }
        }

        public static IEnumerator NotifyHQSetOrNot(this GameHandle gh, Star star, Action onComplete) {
            do {
                yield return Wait.ForEndOfFrame;
            } while (true);
        }

        public static IEnumerator PollForUpdate(this GameHandle gh) {
            Handler<JsonInt> turnPollHandler = null;
            Handler<JsonEmpty> sendTurnUpdateHandler = null;
            // bool isPollingForTurn = true;
            do {
                yield return Wait.ForEndOfFrame;
                if (turnPollHandler == null && !gh.hasTurn) {
                    Debug.LogFormat("-- [+] start polling for turn");
                    turnPollHandler = new Handler<JsonInt>(
                        new GameHandle.PlayerTurnPollRequest(gh.Instance.Key, gh.playerHandler.PlayerInstance.Index).Marshall()
                    );
                    turnPollHandler.POST(GameHandle.PollForTurnSignalRoute.Route);
                    continue;
                }

                if (turnPollHandler != null && !gh.hasTurn) {
                    Debug.LogFormat("-- [+] polling for turn");
                    if (turnPollHandler.hasLoadedResource) {
                        Debug.LogFormat("-- [+] read poll response to check if turn");
                        JsonInt toact = turnPollHandler.onDone();
                        if (toact.value == gh.playerHandler.PlayerInstance.Index) {
                            Debug.LogFormat("[+] player got response from server that it's players turn");
                            gh.hasTurn = true;
                            // isPollingForTurn = false;
                        }
                        Debug.LogFormat("-- [+] not our turn so lets start it over");
                        turnPollHandler = null;
                    }
                    continue;
                }

                if (sendTurnUpdateHandler == null && gh.hasTurn) {
                    sendTurnUpdateHandler = new Handler<JsonEmpty>();
                    continue;
                }

                if (sendTurnUpdateHandler != null && gh.hasTurn) {
                    if (gh.OnTurnSent != null) { //i.e. a button press, means we sent our turn
                        gh.OnTurnSent(sendTurnUpdateHandler);
                        yield return new WaitUntil( // block until we set the handler null elsewhere
                            ()=>{
                                if (sendTurnUpdateHandler == null) {
                                    // isPollingForTurn = true;
                                    gh.hasTurn = false;
                                    return true;
                                }
                                return false;
                            }
                        );
                    }
                    continue;
                }
            } while (true);
        }
    }
}
