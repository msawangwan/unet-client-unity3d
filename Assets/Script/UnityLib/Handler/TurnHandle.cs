using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib.Net;

namespace UnityLib {
    public class TurnHandle : MonoBehaviour {
        public Queue<Func<Handler<JsonEmpty>>> handlers = new Queue<Func<Handler<JsonEmpty>>>();
        private int actionPoints;

        public int ActionPoints {
            get {
                return actionPoints;
            }
            set {
                actionPoints = value;
                if (RaiseOnActionPointsChanged != null) {
                    RaiseOnActionPointsChanged(actionPoints);
                }
            }
        }

        public Action<int> RaiseOnActionPointsChanged;

        public static TurnHandle New(int apPerTurn=10) {
            TurnHandle th = new GameObject("turn_handle").AddComponent<TurnHandle>();
            th.actionPoints = apPerTurn;
            return th;
        }
    }
}
