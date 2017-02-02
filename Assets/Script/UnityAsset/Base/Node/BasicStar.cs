// using UnityEditor;
using UnityEngine;

namespace UnityAsset {
    [CreateAssetMenuAttribute(fileName="node_basic_star", menuName="Entity/Node/Basic Star", order=0)]
    public class BasicStar : ScriptableObject {
        public string gamename;
        public int capacity;
        public int defenseBonus;
    }
}
