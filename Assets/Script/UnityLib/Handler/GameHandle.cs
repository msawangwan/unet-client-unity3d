﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityLib.Net;

namespace UnityLib {
    public class GameHandle : MonoBehaviour {
        [System.Serializable]
        public class JoinRequest : IJSONer {
            public int gameKey;
            public string playerName;
            public bool host;

            public JoinRequest() {}
            public JoinRequest(int gameKey, string playerName, bool host) { this.gameKey = gameKey; this.playerName = playerName; this.host = host; }

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        [System.Serializable]
        public class JoinResponse : IJSONer {
            public Player.Parameters playerParameters;
            public World.Parameters worldParameters;

            public JoinResponse() {}

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        [System.Serializable]
        public class PlayerTurnPollRequest : IJSONer {
            public int gameID;
            public int playerIndex;

            public PlayerTurnPollRequest() {}
            public PlayerTurnPollRequest(int gameID, int playerIndex) { this.gameID = gameID; this.playerIndex = playerIndex; }

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        [System.Serializable]
        public class PlayerTurnCompleteRequest : IJSONer { // UHHHH this is the exact same as the above so refactor
            public int gameID;
            public int playerIndex;

            public PlayerTurnCompleteRequest() {}
            public PlayerTurnCompleteRequest(int gameID, int playerIndex) { this.gameID = gameID; this.playerIndex = playerIndex; }

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        [System.Serializable]
        public class CheckNodeHQRequest : IJSONer {
            public int gameID;
            public int playerIndex;
            public string nodeString;

            public CheckNodeHQRequest() {}
            public CheckNodeHQRequest(int gameID, string nodeString) { this.gameID = gameID; this.nodeString = nodeString; }

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        public class GameScene {
            public Scene instance;
            public bool isLoaded;
            public GameScene(Scene instance, bool isLoaded) { this.instance = instance; this.isLoaded = isLoaded; }
        }

        public static readonly Resource LoadGameWorld = new Resource("game/world/load");
        public static readonly Resource JoinGameWorld = new Resource("game/world/join");
        public static readonly Resource ValidatePlayerHQChoiceRoute = new Resource("game/world/player/hq/validation");
        public static readonly Resource SendPlayerReadyRoute = new Resource("game/world/player/signal/ready");

        public static readonly Resource PollForTurnSignalRoute = new Resource("game/turn/poll");
        public static readonly Resource SendTurnCompletedRoute = new Resource("game/turn/complete");
        // public static readonly Resource SendTurnRoute = new Resource("game/turn/update");

        public static GameScene GameSceneInstance;

        public delegate Star selected();

        public Game Instance { get; set; }

        public string PlayerName { get; set; }
        public string GameName { get; private set; }
        public int GameKey { get; set; }

        public bool isHost { get; private set; }
        public bool isReadyToLoad { get; set; }
        public bool hasTurn { get; set; }
        public bool hasHq { get; set; }

        public PollHandle pollHandler { get; private set; }
        public WorldHandle worldHandler { get; private set; }
        public PlayerHandle playerHandler { get; private set; }

        public GameHUDController GameHUDCtrl { get; private set; }

        // public Action<Handler<JsonEmpty>> OnTurnSent;
        public Func<Handler<JsonEmpty>> OnTurnCompleted; // returns a handler to call on turn completed

        public void LoadWorldHandle(World.Parameters worldParameters) {
            this.worldHandler = WorldHandle.New(worldParameters);

            WorldHandle.WorldSceneInstance = SceneManager.CreateScene(Globals.scenename_worldhandle);
            SceneManager.MoveGameObjectToScene(WorldHandle.WorldHandleInstance.gameObject, WorldHandle.WorldSceneInstance);
            
            StartCoroutine(this.worldHandler.LoadWorldScene(this, null));
        }

        public void LoadPollHandler(PollHandle ph) {
            this.pollHandler = ph;
        }

        public void LoadPlayerHandle(string playername) {
            this.playerHandler = PlayerHandle.New(playername);
        }

        public void Notified(selected s) {
            Star star = s();
            if (hasTurn && !hasHq) {
                GameHUDCtrl.View.DisplayActionButtonAndOnPressExecute(
                    "choose hq",
                    ()=>{
                        StartCoroutine(this.CheckNodeValidHQ(star));
                    }
                ); // call game/world/player/hq/validation
            }
        }

        public static GameHandle New(GameHUDController gamehudctrl, string gameName, bool isHost) {
            GameHandle gh = new GameObject(string.Format("game_handle_[{0}]", gameName)).AddComponent<GameHandle>();

            gh.Instance = Game.New("", "");
            gh.GameHUDCtrl = gamehudctrl;
            gh.GameName = gameName;
            gh.isHost = isHost;

            Scene gameHandleScene = default(Scene);

            if (GameHandle.GameSceneInstance == null) {
                gameHandleScene = SceneManager.CreateScene(Globals.scenename_gamehandle);
                GameHandle.GameSceneInstance = new GameHandle.GameScene(gameHandleScene, true);
            } else if (GameHandle.GameSceneInstance.isLoaded) { // TODO: DESTROY OR UNLOAD ASYNC THE CURRENT STATIC INSTANCE OF THE SCENE
                gameHandleScene = SceneManager.CreateScene(Globals.scenename_gamehandle);
                GameHandle.GameSceneInstance = new GameHandle.GameScene(gameHandleScene, true);
            } else {// TODO: HANDLE THIS
                Debug.LogErrorFormat(gh.gameObject, "unhandled scene error");
            }

            SceneManager.MoveGameObjectToScene(gh.gameObject, GameHandle.GameSceneInstance.instance);

            return gh;
        }

        private void OnEnable() {
            Debug.LogWarningFormat("[+] {0} callback: OnEnable ... [{1}]", gameObject.name, Time.time);
        }

        private void OnDisable() {
            Debug.LogWarningFormat("[+] {0} callback: OnDisable ... [{1}]", gameObject.name, Time.time);
        }

        private void OnDestroy() {
            Debug.LogWarningFormat("[+] {0} callback: OnDestroy ... [{1}]", gameObject.name, Time.time);
        }
    }
}
