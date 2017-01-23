using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib {
    public class Player {
        [System.Serializable]
        public class Parameters : IJSONer {
            public int id;
            public int index;

            public Parameters() {}

            public string Marshall() { return JsonUtility.ToJson(this); }
        }

        private string name;
        private int id;
        private int index;
        private Star hqstar;

        public string Name {
            get {
                return name;
            }
            set {
                name = value;
                if (RaiseOnPlayerNameChanged != null) {
                    RaiseOnPlayerNameChanged(name);
                }
            }
        }

        public int ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public int Index {
            get {
                return index;
            }
            set {
                index = value;
            }
        }

        public Star HQStar { 
            get {
                return hqstar;
            }
            set {
                hqstar = value;
                if (RaiseOnHQStarChanged != null) {
                    RaiseOnHQStarChanged(hqstar);
                }
            }
        }

        public Action<string> RaiseOnPlayerNameChanged;
        public Action<Star> RaiseOnHQStarChanged;

        private Player() {}

        public static Player New(string playername) {
            Player p = new Player();
            p.Name = playername;
            return p;
        }
    }
}
