using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI.Framework.Client {
    public class Quadrant : MonoBehaviour {
        public static float QuadrantRadius = 1.2f;

        private static List<int> ids = new List<int>();
        private static int id = -1;

        private static int nextId {
            get {
                while (true) {
                    ++id;
                    if (!ids.Contains(id)) {
                        ids.Add(id);
                        break;
                    }
                }
                return id;
            }
        }

        private Quadrant[] SubQuadrants = new Quadrant[4];
        private Transform QuadrantNode = null;
        private int nodeID = -1;
        private int depth = -1;
        private string label = string.Empty;

        public static Quadrant NewQuadrantNodeFromGameObject(GameObject parentQuadrant, GameObject node, Vector3 point, int depth, string label) {
            Quadrant q = node.AddComponent<Quadrant>();

            q.QuadrantNode = node.transform;
            q.nodeID = nextId;
            q.depth = depth;
            q.label = label;

            CircleCollider2D c = node.AddComponent<CircleCollider2D>();
            c.radius = QuadrantRadius;

            node.transform.SetParent(parentQuadrant.transform);
            node.transform.position = point;

            Debug.LogFormat("instantiated a new quadrant node: {0}", q.ToString());
            node.gameObject.name += string.Format("[subquadrant: {0}][depth: {1}]", label, depth);

            return q;
        }

        public static Quadrant InstantiateQuadrantRoot(Vector3 rootPoint = default(Vector3)) {
            GameObject rootContainer = new GameObject("quadrant_tree");
            GameObject root = new GameObject("quadrant_root");

            rootContainer.transform.position = Vector3.zero;

            return NewQuadrantNodeFromGameObject(rootContainer, root, rootPoint, -1, "quadrant_root");
        }

        public static List<GameObject> InstantiateSubQuadrantGameObjects(Quadrant root, int nodeCount) {
            List<GameObject> gos = new List<GameObject>();

            int i = 0;
            while (i < nodeCount) {
                GameObject go = new GameObject(string.Format("quadrant_node [{0}]", i));
                go.transform.SetParent(root.transform);
                gos.Add(go);
                ++i;
            }

            return gos;
        }

        public static void SortQuadrants(Quadrant root, List<GameObject> gos) {
            List<int> created = new List<int>();
            int numcreated = 0;
            int maxattempts = 20;
            int attempts = 0;
            float scalemin = -50;
            float scalemax = 50;
            while (numcreated < gos.Count) {
                foreach (GameObject go in gos) {
                    Quadrant q = go.GetComponent<Quadrant>();
                    if (q == null) {
                        Vector3 p = new Vector3(UnityEngine.Random.Range(scalemin, scalemax),UnityEngine.Random.Range(scalemin, scalemax),0f);
                        root.TryInsert(go, p, -1);
                    } else {
                        if (!created.Contains(q.nodeID)) {
                            created.Add(q.nodeID);
                            ++numcreated;
                        }
                    }
                }
                if (attempts > maxattempts) {
                    break;
                }
                ++attempts;
            }
        }

        private bool isOverlapping(Vector3 point) {
            float x1 = QuadrantNode.position.x;
            float y1 = QuadrantNode.position.y;

            float x2 = point.x;
            float y2 = point.y;
            // return false;
            if (Mathf.Abs(x1 - x2) <= QuadrantRadius) {
                return true;
            }  else if (Mathf.Abs(y1 - y2) <= QuadrantRadius) {
                return true;
            } else {
                return false;
            }
        }

        private void TryInsert(GameObject go, Vector3 point, int level) {
            ++level;

            float x1 = QuadrantNode.position.x;
            float y1 = QuadrantNode.position.y;

            float x2 = point.x;
            float y2 = point.y;

            if (x2 > x1 && y2 > y1) {
                if (SubQuadrants[0] == null) {
                    if (!isOverlapping(point)) {
                        SubQuadrants[0] = NewQuadrantNodeFromGameObject(QuadrantNode.gameObject, go, point, level, "quadrant 1");
                    }
                } else {
                    SubQuadrants[0].TryInsert(go, point, level);
                }
            } else if (x2 > x1 && y2 < y1) {
                if (SubQuadrants[1] == null) {
                    if (!isOverlapping(point)) {
                        SubQuadrants[1] = NewQuadrantNodeFromGameObject(QuadrantNode.gameObject, go, point, level, "quadrant 2");
                    }
                } else {
                    SubQuadrants[1].TryInsert(go, point, level);
                }
            } else if (x2 < x1 && y2 < y1) {
                if (SubQuadrants[2] == null) {
                    if (!isOverlapping(point)) {
                        SubQuadrants[2] = NewQuadrantNodeFromGameObject(QuadrantNode.gameObject, go, point, level, "quadrant 3");
                    }
                } else {
                    SubQuadrants[2].TryInsert(go, point, level);
                }
            } else if (x2 < x1 && y2 > y1) {
                if (SubQuadrants[3] == null) {
                    if (!isOverlapping(point)) {
                        SubQuadrants[3] = NewQuadrantNodeFromGameObject(QuadrantNode.gameObject, go, point, level, "quadrant 4");
                    }
                } else {
                    SubQuadrants[3].TryInsert(go, point, level);
                }
            } else {
                Debug.LogWarningFormat("couldnt find a suitable quadrant to insert into [{0}]", point.ToString());
            }
        }

        public override string ToString() {
            return string.Format("quadrant node: <{0}, {1}>", QuadrantNode.position.x, QuadrantNode.position.y);
        }
    }
}
