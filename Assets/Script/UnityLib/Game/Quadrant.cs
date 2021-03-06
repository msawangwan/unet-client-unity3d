﻿using System.Collections.Generic;
using UnityAdt;
using UnityEngine;

namespace UnityLib {
    public class Quadrant : MonoBehaviour {
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

        public static float QuadrantScale { get; private set; } // todo: should not be static
        public static float QuadrantNodeRadius { get; private set; } // todo: should not be static

        public bool isInitialised { get; private set; }

        private Quadrant[] subQuadrant = new Quadrant[4];
        private Transform nodeTransform = null;
        private int nodeID = -1;
        private int depth = -1;
        private string label = string.Empty;


        public static Quadrant AddQuadrantComponentToGameObject(GameObject parentQuadrant, GameObject node, Vector3 point, int depth, string label) {
            Quadrant q = node.AddComponent<Quadrant>();

            q.nodeTransform = node.transform;
            q.nodeID = nextId;
            q.depth = depth;
            q.label = label;

            CircleCollider2D c = node.AddComponent<CircleCollider2D>();
            c.radius = QuadrantNodeRadius;

            node.gameObject.name += string.Format("[subquadrant: {0}][depth: {1}]", label, depth);
            node.transform.SetParent(parentQuadrant.transform);
            node.transform.position = point;

            return q;
        }

        public static Quadrant InstantiateQuadrantRootGameObject(Vector3 rootPoint, float scale, float nodeRadius) {
            GameObject rootContainer = new GameObject("quadrant_tree");
            GameObject root = new GameObject("quadrant_root");

            rootContainer.transform.position = Vector3.zero;

            Quadrant.QuadrantScale = scale;
            Quadrant.QuadrantNodeRadius = nodeRadius;

            return AddQuadrantComponentToGameObject(rootContainer, root, rootPoint, -1, "quadrant_root");
        }

        public static List<GameObject> InstantiateSubQuadrantGameObjects(Quadrant root, int nodeCount) {
            List<GameObject> gos = new List<GameObject>();
            gos.Add(root.gameObject);

            int i = 0;
            while (i < nodeCount) {
                GameObject go = new GameObject(string.Format("quadrant_node [{0}]", i));
                go.transform.SetParent(root.transform);
                gos.Add(go);
                ++i;
            }

            return gos;
        }

        public static void Partition(Quadrant root, List<GameObject> gos, pRNG generator, int maxSpawnAttempts) {
            List<int> created = new List<int>();

            int numcreated = 0;
            int attempts = 0;
            int maxattempts = maxSpawnAttempts;

            float scalemin = -QuadrantScale;
            float scalemax = QuadrantScale;
            
            while (numcreated < gos.Count) {
                foreach (GameObject go in gos) {
                    Quadrant q = go.GetComponent<Quadrant>();
                    if (q == null) {
                        Vector3 p = new Vector3(
                            generator.InRangef(scalemin, scalemax),
                            generator.InRangef(scalemin, scalemax),
                            0f
                        );
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

            root.isInitialised = true;
        }

        private bool isOverlappedBy(Vector3 point) {
            float dx = Mathf.Abs(nodeTransform.position.x - point.x);
            float dy = Mathf.Abs(nodeTransform.position.y - point.y);

            if (dx <= QuadrantNodeRadius || dy <= QuadrantNodeRadius) {
                return true;
            } else {
                return false;
            }
        }

        private void TryInsert(GameObject go, Vector3 point, int level) {
            ++level;

            float x1 = nodeTransform.position.x;
            float y1 = nodeTransform.position.y;

            float x2 = point.x;
            float y2 = point.y;

            if (x2 > x1 && y2 > y1) {
                if (subQuadrant[0] == null) {
                    if (!isOverlappedBy(point)) {
                        subQuadrant[0] = AddQuadrantComponentToGameObject(nodeTransform.gameObject, go, point, level, "quadrant 1");
                    }
                } else {
                    subQuadrant[0].TryInsert(go, point, level);
                }
            } else if (x2 > x1 && y2 < y1) {
                if (subQuadrant[1] == null) {
                    if (!isOverlappedBy(point)) {
                        subQuadrant[1] = AddQuadrantComponentToGameObject(nodeTransform.gameObject, go, point, level, "quadrant 2");
                    }
                } else {
                    subQuadrant[1].TryInsert(go, point, level);
                }
            } else if (x2 < x1 && y2 < y1) {
                if (subQuadrant[2] == null) {
                    if (!isOverlappedBy(point)) {
                        subQuadrant[2] = AddQuadrantComponentToGameObject(nodeTransform.gameObject, go, point, level, "quadrant 3");
                    }
                } else {
                    subQuadrant[2].TryInsert(go, point, level);
                }
            } else if (x2 < x1 && y2 > y1) {
                if (subQuadrant[3] == null) {
                    if (!isOverlappedBy(point)) {
                        subQuadrant[3] = AddQuadrantComponentToGameObject(nodeTransform.gameObject, go, point, level, "quadrant 4");
                    }
                } else {
                    subQuadrant[3].TryInsert(go, point, level);
                }
            } else {
                Debug.LogWarningFormat("couldnt find a suitable quadrant to insert into [{0}]", point.ToString());
            }
        }

        public override string ToString() {
            return string.Format("quadrant node: <{0}, {1}>", nodeTransform.position.x, nodeTransform.position.y);
        }
    }
}
