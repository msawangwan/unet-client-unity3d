using UnityEngine;
using UnityEditor;

namespace UnityAsset {
    public class Builder {
        [MenuItem ("Assets/AssetBundles/Build Bundles")]
        static void BuildAllAssetBundles () {
            BuildPipeline.BuildAssetBundles ("Assets/AssetBundle", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("Assets/AssetBundles/Get Bundle Names")]
        static void GetBundleNames() {
            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach(var n in names) {
                Debug.LogFormat("AssetBundle: {0}", n);
            }
        }
    }
}
