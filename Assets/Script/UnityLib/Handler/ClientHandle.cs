using UnityLib.Net;
using UnityEngine;

namespace UnityLib {
    public class ClientHandle : MonoBehaviour {
        public enum RoleType : byte {
            None = 0,
            Host = 1,
            Client = 2,
        }

        public sealed class PayloadData {
            public string ClientName;
            public string HandleID;
        }

        public static readonly Resource Registration      = new Resource("client/handle/register");
        public static readonly Resource GetSessionKey = new Resource("client/handle/session/key");

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

        public int HandleID { // get from server on register 
            get {
                return handleID;
            }
            set {
                handleID = value;
                json.HandleID = new JsonInt(handleID).Marshall();
            }
        }

        public int SessionKey {
            get {
                return sessionKey;
            }
            set {
                sessionKey = value;
            }
        }

        public RoleType Role {
            get {
                return role;
            }
            set {
                role = value;
            }
        }

        public bool hasSessionKey {
            get;
            set;
        }

        private RoleType role;
        private string clientName;
        private int handleID;
        private int sessionKey;

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
