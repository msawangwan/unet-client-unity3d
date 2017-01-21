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
        public class WorldParameters : IJSONer {
            public int nodeMaxSpawnAttempts;
            public int nodeCount;
            public float nodeRadius;
            public float worldScale;
            public long worldSeed;

            public WorldParameters() {}

            public string Marshall() { return JsonUtility.ToJson(this); }
            public override string ToString() { return string.Format("[node count: {0}][node radius: {1}][world scale: {2}][max spawn attempts: {3}][world seed: {4}]", nodeCount, nodeRadius, worldScale, nodeMaxSpawnAttempts, worldSeed); }
        }

        public class GameScene {
            public Scene instance;
            public bool isLoaded;
            public GameScene(Scene instance, bool isLoaded) { this.instance = instance; this.isLoaded = isLoaded; }
        }

        public static readonly Resource LoadGameWorld = new Resource("game/world/load");
        public static readonly Resource JoinGameWorld = new Resource("game/world/join");

        public static GameScene GameSceneInstance;

        public Game Instance { get; set; }

        public string GameName { get; private set; }
        public int GameKey { get; set; }

        public bool isHost { get; private set; }
        public bool isReadyToLoad { get; set; }

        public WorldHandle worldHandler { get; private set; }
        public TurnHandle turnHandler { get; private set; }

        public GameHUDController GameHUDCtrl { get; private set; }

        public void LoadWorldHandle(GameHandle.WorldParameters worldParameters) {
            this.worldHandler = WorldHandle.New(worldParameters);

            WorldHandle.WorldSceneInstance = SceneManager.CreateScene(Globals.scenename_worldhandle);
            SceneManager.MoveGameObjectToScene(WorldHandle.WorldHandleInstance.gameObject, WorldHandle.WorldSceneInstance);
            
            StartCoroutine(this.worldHandler.LoadWorldScene(null));
        }

        public void LoadTurnHandler() {
            this.turnHandler = TurnHandle.New();
            SceneManager.MoveGameObjectToScene(this.turnHandler.gameObject, GameHandle.GameSceneInstance.instance);
            StartCoroutine(this.turnHandler.SpoolUp());
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
