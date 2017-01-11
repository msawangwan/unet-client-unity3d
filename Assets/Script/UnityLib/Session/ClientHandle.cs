using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class ClientHandle : MonoBehaviour {
        public string ClientName { get; private set; } // set in constructor
        public int ClientSessionID { get; set; } // get from server on register

        public static ClientHandle New(string clientName) {
            ClientHandle ch = new GameObject(string.Format("client_handle_[{0}]", clientName)).AddComponent<ClientHandle>();
            ch.ClientName = clientName;
            return ch;
        }
    }
}
