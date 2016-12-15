using UnityEngine;
using System;

namespace UnityAPI.Game {
    public class StarMapController : MonoBehaviour {
        public static StarMapController Instance = null;

        public static Action<StarNode> RaiseNodeSelected { get; set; }
        public static Action RaiseNodeDeselected { get; set; }

        public static void NotifyNodeSelected (StarNode starNode) {
            RaiseNodeSelected.InvokeSafe(starNode);
        }

        private static void NotifyNodeDeselect () {
            RaiseNodeDeselected.InvokeSafe();
        }

        void Start () {
            Instance = CommonUtil.EnablePersistance(this, gameObject);
            SelectionArea.RaiseSelectionAreaDownEvent += NotifyNodeDeselect;
        }
    }
}
