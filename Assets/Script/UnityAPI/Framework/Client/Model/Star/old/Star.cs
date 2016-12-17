using UnityEngine;

namespace UnityAPI.Game {
    public class Star {
        [System.Serializable]
        public class Properties {
            public enum Type { None, Gaseous, Solid, Liquid, Blackhole }

            public Type StarType = Type.None;
            public Vector3 GalaxyCoordinate = Vector3.zero;
            public int ID = -1;
            public int ResourceValue = 0;
            public float RotationOffset = 0.0f;
            public string Designation = "Star - One of Many";
        }

        public static LinkedList<Star> Stars = new LinkedList<Star>();

        public LinkedList<Star>.Node NodeLink = null;
        public Properties StarProperties = null;
        public StarNode NodeObject = null;

        public Star () {
            NodeLink = Stars.Add ( this ); // todo: use one or the other
        }
    }
}
