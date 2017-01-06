using System.Collections;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public static class GameHandleExtension {
        public static IEnumerator Init(this GameHandle gh) {
            do {
                yield return null;
                if (gh.isReadyToLoad) {
                    break;
                }
            } while (true);

            if (gh.isExisting) {
                gh.StartCoroutine(gh.LoadAsExisting());
            } else {
                gh.StartCoroutine(gh.LoadAsNew());
            }

        }

        public static IEnumerator GetFrame(this GameHandle gh) {
            do {
                yield return null;
                if (gh.Session.SessionInstance == null) {
                    continue;
                } else {
                    break;
                }
            } while (true);

            Handler<Frame> handler = new Handler<Frame>(
                JsonUtility.ToJson(gh.Session.SessionInstance.sessionID)
            );

            handler.POST(RouteHandle.Game_FetchFrameUpdate);

            do {
                yield return null;
                if (handler.onDone != null) {
                    Frame first = handler.onDone();
                    break;
                }
            } while (true);

            Debug.LogFormat("--- [+] got frame");
        }

        public static IEnumerator BeginUpdate(this GameHandle gh) {
            yield return null;
        }

        public static IEnumerator LoadAsNew(this GameHandle gh) {
            do {
                yield return null;;
                if (gh.Session.SessionInstance != null) {
                    break;
                }
            } while (true);

            Debug.LogFormat("-- [+][Create] Session label and key: {0} {1}", gh.Session.SKey.bareFormat, gh.Session.SKey.redisFormat);

            Handler<Frame> handler = new Handler<Frame>(
                JsonUtility.ToJson(gh.Session.SKey)
            );

            handler.POST(RouteHandle.Game_StartUpdate);

            do {
                yield return null;
                if (handler.onDone != null) {
                    handler.onDone();
                    break;
                }
            } while (true);

            Debug.LogFormat("--- [+] started a new game on the server");
        }

        public static IEnumerator LoadAsExisting(this GameHandle gh) {
            do {
                yield return null;
                if (gh.Session.SessionInstance != null) {
                    break;
                }
            } while (true);

            Debug.LogFormat("-- [+][Join] Session label and key {0} {1}", gh.Session.SKey.bareFormat, gh.Session.SKey.redisFormat);

            Handler<Frame> handler = new Handler<Frame>(
                JsonUtility.ToJson(gh.Session.SKey)
            );

            handler.POST(RouteHandle.Game_EnterUpdate);

            do {
                yield return null;
                if (handler.onDone != null) {
                    handler.onDone();
                    break;
                }
            } while (true);

            Debug.LogFormat("--- [+] joined an existing game on the server");
        }

        public static void EndGameSession(this GameHandle gb) {
            
        }
    }
}
