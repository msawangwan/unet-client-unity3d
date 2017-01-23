using UnityEngine;

namespace UnityLib {
    public class Star : SelectableNode {
        public GameHandle gameHandler { get; private set; }

        public float x { get { return transform.position.x; } }
        public float y { get { return transform.position.y; } }

        public string AsRedisKey() {
            float xx = (float)((int)(x*100)) / 100;
            float yy = (float)((int)(y*100)) / 100;
            return string.Format("{0}:{1}", xx, yy);
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
