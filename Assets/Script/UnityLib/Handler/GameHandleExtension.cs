﻿using System;
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

            if (gh.hasTurn) {
                gh.GameHUDCtrl.View.SetTextHUDMessageOverlayThenFade(string.Format("ready p{0}: select an HQ node", gh.playerHandler.PlayerInstance.Index));
            } else {
                gh.GameHUDCtrl.View.SetTextHUDMessageOverlay("waiting for opponent to select an hq...");
                yield return new WaitUntil(
                    () => {
                        if (!gh.hasTurn) {
                            Debug.LogFormat("waiting for opponent to set an hq ..");
                            return false;
                        }
                        return true;
                    }
                );
                gh.GameHUDCtrl.View.ClearTextHUDMessageOverlay();
                gh.GameHUDCtrl.View.SetTextHUDMessageOverlayThenFade(string.Format("ready p{0}: select an HQ node", gh.playerHandler.PlayerInstance.Index));
            }
        }

        public static IEnumerator CheckNodeValidHQ(this GameHandle gh, Star star) {
            Debug.LogFormat("[+] player requesting node as hq");
            if (!gh.hasHq) {
                Handler<JsonBool> checkHQHandler = new Handler<JsonBool>(
                    new GameHandle.CheckNodeHQRequest(gh.GameKey, star.AsRedisKey).Marshall()
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
                    Debug.LogFormat("[+] set hq [{0}][{1}]",gh.playerHandler.PlayerInstance.Name, star.AsRedisKey);
                    gh.hasHq = true;
                    gh.OnTurnCompleted = () => { // send to server we;re done withour turn
                        return new Handler<JsonEmpty>(
                            new GameHandle.PlayerTurnCompleteRequest(gh.Instance.Key, gh.playerHandler.PlayerInstance.Index).Marshall()
                        );
                    };
                } else {
                    Debug.Log("[+] choose another hq");
                }
            } else {
                Debug.LogFormat("[+] player already has hq");
            }
        }

        public static IEnumerator FetchAndCacheNodeData(this GameHandle gh, Star star) {
            Handler<GameHandle.CacheNodeResponse> cacheNodeHanlder = new Handler<GameHandle.CacheNodeResponse>(
                new GameHandle.NodeRequest(gh.Instance.Key, gh.playerHandler.PlayerInstance.Index, star.AsRedisKey).Marshall()
            );

            cacheNodeHanlder.POST(GameHandle.GetNodeAndCacheDataEndPoint.Route);

            gh.GameHUDCtrl.View.DisplayLoadingBlockPanel();

            yield return new WaitUntil(
                () => {
                    if (cacheNodeHanlder.hasLoadedResource) {
                        return true;
                    }
                    Debug.Log("-- -- -- -- -- -- -- [+] we're blocking .........");
                    // TODO: update screen with blocking UI animation
                    return false;
                }
            );

            GameHandle.CacheNodeResponse cnr = cacheNodeHanlder.onDone();
            star.CacheStarData(cnr.properties, cnr.state);

            gh.GameHUDCtrl.View.DisableLoadingBlockPanel();
        }

        public static IEnumerator PollForUpdate(this GameHandle gh) {
            Handler<JsonInt> turnPollHandler = null;
            WaitForSeconds ws = new WaitForSeconds(1.25f);
            bool turnToAct = false;

            do {
                yield return Wait.ForEndOfFrame;
                if (turnPollHandler == null && !gh.hasTurn) {
                    Debug.LogFormat("-- [+] start polling for turn [{0}]", Time.time);
                    turnPollHandler = new Handler<JsonInt>(
                        new GameHandle.PlayerTurnPollRequest(gh.Instance.Key, gh.playerHandler.PlayerInstance.Index).Marshall()
                    );
                    turnPollHandler.POST(GameHandle.PollForTurnSignalRoute.Route);
                    continue;
                }

                if (turnPollHandler != null && !gh.hasTurn) {
                    Debug.LogFormat("-- [+] start long polling for turn...");
                    yield return new WaitUntil(
                        () => {
                            if (turnPollHandler.hasLoadedResource) {
                                Debug.LogFormat("-- -- [+] ok it's our turn now, killing long poll and loading the end turn button [{0}]", Time.time);
                                // gh.GameHUDCtrl.View.SetTextHUDMessageOverlayThenFade(string.Format("p{0}: it's your turn", gh.playerHandler.PlayerInstance.Index));
                                gh.GameHUDCtrl.View.OnTurnEndAndButtonPress(
                                    ()=> {
                                        gh.OnTurnCompleted = () => {
                                            return new Handler<JsonEmpty>(
                                                new GameHandle.PlayerTurnCompleteRequest(gh.Instance.Key, gh.playerHandler.PlayerInstance.Index).Marshall()
                                            ); 
                                        };
                                        // TODO: DRAIN THE QUEUE HERE!!!
                                        // gh.GameHUDCtrl.View.SetTextHUDMessageOverlayThenFade(string.Format("p{0}: ended turn", gh.playerHandler.PlayerInstance.Index));
                                    }
                                );
                                return true;
                            }
                            return false;
                        }
                    );
                    gh.hasTurn = true;
                    turnToAct = true;
                    turnPollHandler = null;
                    gh.GameHUDCtrl.View.SetTextHUDMessageOverlayThenFade(string.Format("p{0}: it's your turn", gh.playerHandler.PlayerInstance.Index));
                    continue;
                }

                if (turnToAct && gh.hasTurn) {
                    Debug.LogFormat("[+] checking if player has completed their turn");
                    if (gh.OnTurnCompleted != null) { //i.e. a button press, means we sent our turn
                        Debug.LogFormat("[+] sending turn to server ...");
                        Handler<JsonEmpty> turncompleted = gh.OnTurnCompleted();
                        turncompleted.POST(GameHandle.SendTurnCompletedRoute.Route);
                        yield return new WaitUntil( // block until we set the handler null elsewhere
                            () => {
                                if (turncompleted.hasLoadedResource) {
                                    Debug.LogFormat("[+] server responded to turn completed request ...");
                                    return true;
                                }
                                return false;
                            }
                        );
                        gh.GameHUDCtrl.View.SetTextHUDMessageOverlayThenFade(string.Format("p{0}: ended turn", gh.playerHandler.PlayerInstance.Index));
                        Debug.LogFormat("[+] turn sent! will now go back to polling until it's my turn again...");
                        gh.hasTurn = false;
                        gh.OnTurnCompleted = null;
                        turnToAct = false;
                    }
                    continue;
                }
            } while (true);
        }
    }
}
