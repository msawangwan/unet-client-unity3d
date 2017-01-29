using System;
using UnityEngine;

namespace UnityLib {
    public class Star : SelectableNode {
        public class State {
            public bool IsHQ;
            public bool Occupied;
            public int Occupant;
        }

        public class Properties {
            public string Name;
            public string Info;
            public int Capactiy;
            public int DeployCost;
            public int MoveCost;
            public int AttackPenalty;
        }

        private string redisKey = "";
        private bool cached = false;

        public Star.State StarState { get; private set; }
        public Star.Properties StarProperties { get; private set; }

        public GameHandle gameHandler { get; private set; }

        public float x { get { return transform.position.x; } }
        public float y { get { return transform.position.y; } }

        public bool Cached { get { return cached; } }


        // takes x,y pair of coords and first truncates and then concats them into one as a redis string
        public string AsRedisKey {
            get {
                if (redisKey.Length == 0) {
                    // float xx = (float)((int)(x * 100)) / 100;
                    // float yy = (float)((int)(y * 100)) / 100;

                    // string sx = string.Format("{0}", xx);
                    // string sy = string.Format("{0}", yy);

                    // // todo: test the robust-ness of this
                    // Func<float, string, string> trunc = (component, s) => {
                    //     if (!s.Contains(".")) {
                    //         s = string.Format("{0}.{1}", component, "00");
                    //     } else {
                    //         string[] ss = s.Split('.');
                    //         if (ss[1].Length != 2) {
                    //             s = string.Format("{0}{1}", s, "0");
                    //         }
                    //     }
                    //     return s;
                    // };
                    // todo: test the robust-ness of this
                    Func<float, string> trunc = (component) => {
                        float c = (float)((int)(component * 100)) / 100;
                        string s = string.Format("{0}", c);
                        if (!s.Contains(".")) {
                            s = string.Format("{0}.{1}", component, "00");
                        } else {
                            string[] ss = s.Split('.');
                            if (ss[1].Length != 2) {
                                s = string.Format("{0}{1}", s, "0");
                            }
                        }
                        return s;
                    };
                    // string sx = trunc(x);
                    // string sy = trunc(y);
                    // string sx = trunc(xx, sx);
                    // string sy = trunc(yy, sy);

                    // redisKey = string.Format("{0}:{1}", sx, sy);
                    redisKey = string.Format("{0}:{1}", trunc(x), trunc(y));
                }
                return redisKey;
            }
        }

        public void CacheStarProperties(Star.Properties properties) {
            this.StarProperties = properties;
        }

        public void RegisterWithGameHandler(GameHandle gameHandler) {
            this.gameHandler = gameHandler;
        }

        public void AttachListener(System.Action action, bool clearAllListeners=false) {
            if (clearAllListeners) {
                base.Pressed = null;
            }
            base.Pressed += action;
        }

        protected override void Notify() {
            if (gameHandler == null) return;
            gameHandler.Notified(() => this);
            // switch (gameHandler.Instance.GamePhase) {
            //     case Game.Phase.Ready:
            //         gameHandler.Notified(() => this);
            //         return;
            //     case Game.Phase.Turn:
            //         return;
            //     case Game.Phase.End:
            //         return;
            //     default:
            //         return;
            // }
        }

        // private void LoadProperties() {
        //     base.Pressed += () => {
        //         if (!cached) {
        //             Debug.LogFormat("-- [+] loading properties for [node: {0}] and caching the data", gameObject.name);
        //             cached = true;
        //         }
        //     };
        // }
    }
}
