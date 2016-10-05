using UnityEngine.UI;
using System.Collections.Generic;

public static class UIMediator {
    public class UIData {
        public Dictionary<Text, string> Fields = null;

        public UIData() {
            Fields = new Dictionary<Text, string>();
        }
    }

    public static System.Action<UIData> RaiseOnUIUpdated { get; set; }
}
