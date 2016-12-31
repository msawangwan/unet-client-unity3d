using UnityEngine;

namespace UnityLib.UI {
    [CreateAssetMenuAttribute(fileName="title_menu", menuName="Model/UI/TitleMenuPanel", order=1)]
    public class TitleMenuInstance : ScriptableObject {
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
