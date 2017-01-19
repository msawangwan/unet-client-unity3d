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

        public static readonly Resource LoadGameWorld = new Resource("game/world/load");
        public static readonly Resource JoinGameWorld = new Resource("game/world/join");

        public string GameName { get; private set; }
        public int GameKey { get; set; }
        public bool isHost { get; private set; }
        public bool isReadyToLoad { get; set; }

        public WorldHandle worldHandler { get; private set; }

        public void LoadWorld(GameHandle.WorldParameters worldParameters) {
            this.worldHandler = WorldHandle.New(worldParameters);
            StartCoroutine(this.worldHandler.LoadWorldScene(null));
        }

        public static GameHandle New(string gameName, bool isHost) {
            GameHandle gh = new GameObject(string.Format("game_handle_[{0}]", gameName)).AddComponent<GameHandle>();

            gh.GameName = gameName;
            gh.isHost = isHost;
            
            Scene gameHandleScene = SceneManager.CreateScene(Globals.scenename_gamehandle);

            SceneManager.MoveGameObjectToScene(gh.gameObject, gameHandleScene);
            SceneManager.MoveGameObjectToScene(CameraRigController.S.gameObject, gameHandleScene);

            // CameraRigController.S.EnableMovement(); // TODO: do this elsewhere??

            // Globals.S.AppState = Globals.ApplicationState.Game; // TODO: revisit this and where to switch at a later date

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
