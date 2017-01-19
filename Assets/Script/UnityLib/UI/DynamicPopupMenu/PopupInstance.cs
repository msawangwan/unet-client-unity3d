using UnityEngine;

namespace UnityLib {
    [CreateAssetMenuAttribute(fileName="popup_submenu", menuName="Model/UI/PopupMenuPanel", order=1)]
    public class PopupInstance : ScriptableObject {
        public int panelID;
    }
}
