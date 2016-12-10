using UnityEngine;
using UnityAPI;

namespace UnityAPI.Framework.UI {
    [CreateAssetMenuAttribute(fileName="menu", menuName="Model/UI/Menu", order=1)]
    public class Menu : ModelObject {
        public string Header;
        public int Key;
        public int SubIndex;
    }
}
