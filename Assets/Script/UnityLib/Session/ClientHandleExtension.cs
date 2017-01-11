using UnityEngine;
using UnityLib.Net;
using System;
using System.Collections;

namespace UnityLib {
    public static class ClientHandleExtension {
        public static IEnumerator Register(this ClientHandle ch, Action onSuccess) {
            Debug.LogFormat("-- [+] new client, register [name: {0}]", ch.ClientName);
            Debug.LogFormat("{0}", ClientHandle.Registration.ToString());

            Handler<JsonInt> registerHandler = new Handler<JsonInt>(ch.json.ClientName);
            JsonInt id = null;

            registerHandler.POST(ClientHandle.Registration.Resource);

            do {
                yield return null;
                Debug.LogFormat("-- -- [+] requesting client handle registration [name: {0}] ... [{1}]", ch.ClientName, Time.time);
                if (registerHandler.hasLoadedResource) {
                    id = registerHandler.onDone();
                    break;
                }
            } while (true);

            ch.ClientSessionID = id.value;

            if (onSuccess != null) {
                onSuccess();
            }

            Debug.LogFormat("-- [+] registered client [name: {0}] [id: {1}]", ch.ClientName, ch.ClientSessionID);
        }

        /* a client can either:
         *
         * - host a session or
         * - join an existing session
         * 
         * both actions require that the client request a key from the server and
         * with this key, the client can then validate a host or join request
         */


        public static IEnumerator RequestHostKey(this ClientHandle ch, Action onSuccess) {
            Debug.LogFormat("-- [+] {0} is requesting a host key ... [{1}]", ch.name, Time.time);
            Debug.LogFormat("{0}", ClientHandle.RequestHostingKey.ToString());

            Handler<JsonInt> keyHandler = new Handler<JsonInt>(ch.json.ClientSessionID);
            JsonInt id = null;

            keyHandler.POST(ClientHandle.RequestHostingKey.Resource);

            do {
                yield return null;
                Debug.LogFormat("-- -- [+] fetching host key ... [{0}]", Time.time);
                if (keyHandler.hasLoadedResource) {
                    id = keyHandler.onDone();
                    break;
                }
            } while (true);

            ch.GameRoleKey = id.value;
            ch.Role = ClientHandle.ClientRole.Host;

            if (onSuccess != null) {
                onSuccess();
            }

            Debug.LogFormat("-- [+] got host key [client handle key: {0}] [client host key: {1}]", Time.time);
        }

        public static IEnumerator RequestJoinKey(this ClientHandle ch, Action<string[]> onSuccess) {
            yield return null;
        }
    }

}
