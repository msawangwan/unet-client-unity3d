using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAsset {
    [CreateAssetMenuAttribute(fileName="node_ship", menuName="Entity/Node/Ship", order=1)]
    public class Ship : ScriptableObject {
        [System.Serializable]
        public class Property {
            public enum Class {
                None = 0,
                Light,
                Medium,
                Heavy,
            }

            public Ship.Property.Class Classification;

            public int HitPoints;
            public int AttackPower;
            public int MoveCost;
        }

        public Ship.Property Properties;
        public string[] SpecialProperties;
    }
}
