using System;

namespace UnityLib {
    [System.Serializable]
    public class Frame {
        public string sessionID;

        public bool hasStarted;
        public bool hasWinner;

        public int connectionCount;
        public int playerToAct;
        public int currentTurnNumber;
        public int packetID;

        public DateTime timestamp;
    }
}
