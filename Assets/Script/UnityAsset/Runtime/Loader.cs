using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLib;

namespace UnityAsset {
    public static class Loader {
        public class AssetBundleCache {
            private Dictionary<string, AssetBundle> store = new Dictionary<string, AssetBundle>();

            public AssetBundle this[string k] {
                get {
                    if (!store.ContainsKey(k)) {
                        return null;
                    }
                    return store[k];
                }
            }

            public bool alreadyCached(string k) { return store.ContainsKey(k); }
        }

        public class AssetBundleFetchHandler {
            public readonly string resource;
            public readonly string bundlename;
            public readonly bool loadAsync;
            
            public Func<AssetBundle> onLoad;

            public bool ready { get { return onLoad != null; } }

            public AssetBundleFetchHandler() {}
            public AssetBundleFetchHandler(string resource, bool loadAsync) {
                string[] components = resource.Split('/');
                this.resource = resource;
                this.loadAsync = loadAsync;
                this.bundlename = components[components.Length - 1];
            }
        }

        public class AssetBundleJSONHandler {
            public readonly string assetname;
            public readonly bool loadAsync;

            public Func<string> onLoad;

            public bool ready { get { return onLoad != null; } }

            public AssetBundleJSONHandler() {}
            public AssetBundleJSONHandler(string assetname, bool loadAsync) {
                this.assetname = assetname;
                this.loadAsync = loadAsync;
            }
        }

        public const string testpath = "Assets/AssetBundle/data/prototype/test";
        public const string testasset = "sometest";

        public static readonly AssetBundleCache BundleCache = new AssetBundleCache();

        public static IEnumerator Fetch(this Loader.AssetBundleCache cache, Loader.AssetBundleFetchHandler request) {
            AssetBundle b = null;

            if (cache.alreadyCached(request.resource)){
                b = cache[request.resource];
            } else {
                if (request.loadAsync) {
                    AssetBundleCreateRequest breq = AssetBundle.LoadFromFileAsync(request.resource);
                    yield return breq;
                    b = breq.assetBundle;
                } else {
                    b = AssetBundle.LoadFromFile(request.resource);
                }

                if (b == null) Debug.LogErrorFormat("tried to load an asset bundle but got null [{0}]", request.resource);
            }

            request.onLoad = () => b;
        }

        public static IEnumerator MarshallJSON(this AssetBundle bundle, Loader.AssetBundleJSONHandler handler) {
            TextAsset asset = null;
            if (handler.loadAsync) {
                AssetBundleRequest abr = bundle.LoadAssetAsync<TextAsset>(handler.assetname);
                yield return abr;
                asset = abr.asset as TextAsset;
            } else {
                asset = bundle.LoadAsset(handler.assetname) as TextAsset;
            }
            byte[] bytes = asset.bytes;
            handler.onLoad= () => Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}
