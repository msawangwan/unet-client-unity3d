using System;
using UnityEngine;

namespace UnityLib {
    public class Star : SelectableNode {
        [System.Serializable]
        public class State {
            public bool isHQ;
            public bool occupied;
            public int occupant;
        }

        [System.Serializable]
        public class Properties {
            public string name;
            public string info;
            public int capactiy;
            public int deployCost;
            public int moveCost;
            public int attackPenalty;
        }

        public static readonly Star.State EmptyState = new Star.State();
        public static readonly Star.Properties EmptyProperties = new Star.Properties();

        private string redisKey = "";
        private bool cached = false;

        public Star.State StarState { get; private set; }
        public Star.Properties StarProperties { get; private set; }

        // public GameHandle gameHandler { get; private set; }

        public float x { get { return transform.position.x; } }
        public float y { get { return transform.position.y; } }

        public bool Cached { get { return cached; } }

        // takes x,y pair of coords and first truncates and then concats them into one as a redis string
        public string AsRedisKey {
            get {
                if (redisKey.Length == 0) {
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
                    redisKey = string.Format("{0}:{1}", trunc(x), trunc(y));
                }
                return redisKey;
            }
        }

        public void CacheStarData(Star.Properties properties, Star.State state) {
            this.StarProperties = properties;
            this.StarState = state;
        }

        public void AttachListener(System.Action action, bool clearAllListeners=false) {
            if (clearAllListeners) {
                base.Pressed = null;
            }
            base.Pressed += action;
        }
    }
}
