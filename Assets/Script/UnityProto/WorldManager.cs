using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAsset;

/// <summary>
///     'may peace be upon you'
/// </summary>

namespace UnityProto {
    public class WorldManager : MonoBehaviour {
        public const int map_width = 10;
        public const int map_height = 10;

        private const string sprite_bundle_path = "Assets/AssetBundle/Sprite/Tile";
        private const string sprite_labelString = "32x32_yellow_outlined_alpha";

        private Sprite tileSprite;

        private IEnumerator Start() {
            Camera cam = Camera.main;
            cam.transform.position = new Vector3(map_width / 2, map_height / 2, cam.transform.position.z);

            yield return null;

            IEnumerator spriteLoader = LoadSpriteAsset();
            IEnumerator mapLoader = DrawMap();

            GameplayManager.Instance.EnqueueRoutine(spriteLoader, true);
            GameplayManager.Instance.EnqueueRoutine(mapLoader, true);
        }

        private IEnumerator LoadSpriteAsset() {
            Loader.AssetBundleFetchHandler resource = new Loader.AssetBundleFetchHandler(sprite_bundle_path, false);
            yield return Loader.BundleCache.Fetch(resource);
            AssetBundle b = resource.bundle;
            tileSprite = b.LoadAsset<Sprite>(sprite_labelString);
            b.Dispose();
        }

        private IEnumerator DrawMap() {
            GameObject prefab = new GameObject("tile");
            for (int x = 0; x < map_width; x++) {
                for (int y = 0; y < map_height; y++) {
                    GameObject tile = Instantiate<GameObject>(prefab);
                    tile.name = string.Format("tile <{0}, {1}>", x, y);
                    tile.transform.position = new Vector3(x, y, 0);
                    tile.transform.rotation = Quaternion.identity;
                    tile.AddComponent<SpriteRenderer>().sprite = tileSprite;
                    yield return null;
                }
            }
        }
    }
}
