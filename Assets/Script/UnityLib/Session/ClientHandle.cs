using UnityEngine;

namespace UnityLib {
    public class ClientHandle : MonoBehaviour {
        public enum ClientRole : byte {
            None = 0,
            Host = 1,
            Client = 2,
        }

        public sealed class PayloadData {
            public string ClientName;
            public string ClientSessionID;
        }

        public static readonly Route Registration      = new Route("client/handle/register");
        public static readonly Route RequestHostingKey = new Route("client/handle/host/key");
        public static readonly Route RequestJoiningKey = new Route("client/handle/join/key");

        public PayloadData json { 
            get; 
            private set; 
        }

        public bool isHost { // unused currently.. update comment if in use 
            get; 
            set; 
        }

        public string ClientName {  // set in constructor the first time
            get {
                return clientName;
            }
            private set {
                clientName = value;
                json.ClientName = new JsonString(clientName).Marshall();
            }
        }

        public int ClientSessionID { // get from server on register 
            get {
                return clientId;
            }
            set {
                clientId = value;
                json.ClientSessionID = new JsonInt(clientId).Marshall();
            }
        }

        public int GameRoleKey {
            get {
                return gameRoleKey;
            }
            set {
                gameRoleKey = value;
            }
        }

        public ClientRole Role {
            get {
                return role;
            }
            set {
                role = value;
            }
        }

        private ClientRole role;
        private string clientName;
        private int clientId;
        private int gameRoleKey;

        public void Init(string clientPlayerName) {
            this.json = new PayloadData();
            this.ClientName = clientPlayerName;
        }

        public static ClientHandle New(string name) {
            ClientHandle ch = new GameObject(string.Format("client_handle_[{0}]", name)).AddComponent<ClientHandle>();
            ch.Init(name);
            return ch;
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
