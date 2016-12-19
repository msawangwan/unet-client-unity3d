using UnityEngine;
using Core.Lib;

namespace UnityAPI.Framework.Client {
    [System.Serializable]
    public class StarData {
        public Vector2f[] points;

        public StarData() {}

        public StarData(int size) {
            points = new Vector2f[size];
        }

        public void AddPoint(Vector3 v, int index) {
            points[index] = new Vector2f(v.x, v.y);
        }
    }
}