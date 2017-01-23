using UnityEngine;

namespace UnityLib {
    public class Star : SelectableNode {
        public GameHandle gameHandler { get; private set; }

        public float x { get { return transform.position.x; } }
        public float y { get { return transform.position.y; } }

        public string AsRedisKey() {
            float xx = (float)((int)(x * 100)) / 100;
            float yy = (float)((int)(y * 100)) / 100;

            string sx = string.Format("{0}", xx);
            string sy = string.Format("{0}", yy);
            
            if (!sx.Contains(".")) {
                sx = string.Format("{0}.{1}", xx, "00");
            } else {
                string[] sxx = sx.Split('.');
                if (sxx[1].Length != 2) {
                    sx = string.Format("{0}{1}", sx, "0");
                }
            }

            if (!sy.Contains(".")) {
                sy = string.Format("{0}.{1}", yy, "00");
            } else {
                string[] syy = sy.Split('.');
                if (syy[1].Length != 2) {
                    sy = string.Format("{0}{1}", sy, "0");
                }
            }

            // return string.Format("{0}:{1}", xx, yy);
            return string.Format("{0}:{1}", sx, sy);
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
            switch (gameHandler.Instance.GamePhase) {
                case Game.Phase.Ready:
                    gameHandler.Notified(() => this);
                    return;
                case Game.Phase.Turn:
                    return;
                case Game.Phase.End:
                    return;
                default:
                    return;
            }
        }
    }
}
