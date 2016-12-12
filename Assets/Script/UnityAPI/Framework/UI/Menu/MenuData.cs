using UnityEngine;

namespace UnityAPI.Framework.UI {
    [CreateAssetMenuAttribute(fileName="menu", menuName="Model/UI/Menu", order=1)]
    public class MenuData : ScriptableObject {
        public enum MenuName {
            None,
            Title,
            ProfileCreate,
            ProfileSelect,
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
