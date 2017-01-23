using System;

namespace UnityLib {
    public class Game {
        public enum Phase {
            None,
            Ready,
            Turn,
            End,
        }

        private int key;
        private int turnnumber;
        private string playername;
        private string opponentname;
        private Game.Phase gamePhase;

        private Action<string> put = (s) => { UnityEngine.Debug.LogWarningFormat("-- -- [+] game value set [{0}]", s); };

        public int Key { 
            get {
                return key;
            }
            set {
                key = value;
                if (RaiseGameKeyChanged != null) {
                    RaiseGameKeyChanged(key);
                }
                put("game key:" + key.ToString());
            }
        }

        public int TurnNumber {
            get {
                return turnnumber;
            }
            set {
                turnnumber = value;
                if (RaiseTurnNumberChanged != null) {
                    RaiseTurnNumberChanged(turnnumber);
                }
                put("turn number:" + turnnumber.ToString());
            }
        }

        public string PlayerName { 
            get {
                return playername;
            }
            set {
                playername = value;
                if (RaisePlayerNameChanged != null) {
                    RaisePlayerNameChanged(playername);
                }
                put("player name:" + playername);
            }
        }

        public string OpponentName {
            get {
                return opponentname;
            }
            set {
                opponentname = value;
                if (RaiseOpponentNameChanged != null) {
                    RaiseOpponentNameChanged(opponentname);
                }
                put("opponent name: " + opponentname);
            }
        }

        public Game.Phase GamePhase {
            get {
                return gamePhase;
            }
            set {
                gamePhase = value;
                if (RaiseGamePhasedChanged != null) {
                    RaiseGamePhasedChanged(gamePhase);
                }
                put("game phase: " + gamePhase);
            }
        }

        public Action<int> RaiseGameKeyChanged;
        public Action<int> RaiseTurnNumberChanged;
        public Action<string> RaisePlayerNameChanged;
        public Action<string> RaiseOpponentNameChanged;
        public Action<Game.Phase> RaiseGamePhasedChanged;

        private Game() {}

        private Game(string playername, string opponentname) {
            this.Key = -1;
            this.PlayerName = playername;
            this.OpponentName = opponentname;
        }

        public static Game New(string playername, string opponentname) {
            Game g = new Game(playername, opponentname);

            return g;
        }
    }
}
