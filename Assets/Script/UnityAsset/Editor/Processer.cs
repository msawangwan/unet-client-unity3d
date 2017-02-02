using UnityEngine;
using UnityEditor;

namespace UnityAsset {
    public class Processer : AssetPostprocessor {
        private void OnPostprocessAssetbundleNameChanged(string path, string previous, string next) {
            string prompt = "";
            if (string.IsNullOrEmpty(previous)) {
                prompt = string.Format("New Bundle was created [{0}] New [{2}]", path, next);
            } else {
                prompt = string.Format("Existing Bundle was changed [{0}] Old [{1}] New [{2}]", path, previous, next);
            }
            Debug.LogFormat(prompt);
        }
    }
}
