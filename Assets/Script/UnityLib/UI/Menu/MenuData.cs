using UnityEngine;

namespace UnityLib.UI {
    [CreateAssetMenuAttribute(fileName="menu", menuName="Model/UI/Menu", order=1)]
    public class MenuData : ScriptableObject {
        public enum MenuName {
            None,
            Title,
            ProfileCreate,
            ProfileSelect,
            ProfileConfirmSelection,
        }

        public enum MenuType {
            None,
            Root,
            Child,
            Leaf,
        }

        public MenuName menuName;
        public MenuType menuType;

        public int menuLevel;
        public int menuInstanceID;
    }
}
