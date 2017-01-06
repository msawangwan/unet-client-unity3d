namespace UnityLib {
    [System.Serializable]
    public class SessionOwner {
        public string attachedSession;
        public string playerName;
        public int playerIndex;

        public SessionOwner() {}
        
        public SessionOwner(string attachedSession, string playerName) {
            this.attachedSession = attachedSession;
            this.playerName = playerName;
        }
        
        public SessionOwner(string attachedSession, string playerName, int playerIndex) {
            this.attachedSession = attachedSession;
            this.playerName = playerName;
            this.playerIndex = playerIndex;
        }
    }
}
