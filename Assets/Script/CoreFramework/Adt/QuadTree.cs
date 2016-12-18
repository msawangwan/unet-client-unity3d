using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CoreFramework.Adt {
    public class QuadTree : MonoBehaviour {
        public class Node {
            public Node[] Links = new Node[4];
            public Vector3 Position = Vector3.zero;
            public float rad = 0.5f;
            public Transform NodeTransform;

            public Node(Vector3 position) {
                this.Position = position;
            }

            public bool DoesOverlap(Node n) {
                float x1 = n.Position.x + n.rad;
                float x2 = Position.x + rad;
                float y1 = n.Position.x + n.rad;
                float y2 = Position.y + rad;
                float r2 = n.rad + rad;
                if (x1 - x2 < rad || y1 - y2 < rad) {
                    return true;
                } else {
                    return false;
                }
            }

            public GameObject TryInsert(Node n) {
                Debug.Log("start insert " + n.Position.x + " " + n.Position.y);
                float x1 = Position.x;
                float y1 = Position.y;
                float x2 = n.Position.x;
                float y2 = n.Position.y;

                if (x2 > x1 && y2 > y1) {
                    Debug.Log("quad1 " + n.Position.x + " " + n.Position.y);
                    if (Links[0] == null) {
                        if (!DoesOverlap(n)) {
                            Debug.Log("new quad1 node " + n.Position.x + " " + n.Position.y);
                            Links[0] = n;
                            return PlaceNode(n);
                        }
                    } else {
                        Links[0].TryInsert(n);
                    }
                } else if (x2 > x1 && y2 < y1) {
                    Debug.Log("quad2 " + n.Position.x + " " + n.Position.y);
                    if (Links[1] == null) {
                        if (!DoesOverlap(n)) {
                            Debug.Log("new quad2 node " + n.Position.x + " " + n.Position.y);
                            Links[1] = n;
                            return PlaceNode(n);
                        }
                    } else {
                        Links[1].TryInsert(n);
                    }
                } else if (x2 < x1 && y2 < y1) {
                    Debug.Log("quad3 " + n.Position.x + " " + n.Position.y);
                    if (Links[2] == null) {
                        if (!DoesOverlap(n)) {
                            Debug.Log("new quad3 node " + n.Position.x + " " + n.Position.y);
                            Links[2] = n;
                            return PlaceNode(n);
                        }
                    } else {
                        Links[2].TryInsert(n);
                    }
                } else if (x2 < x1 && y2 > y1) {
                    Debug.Log("quad4 " + n.Position.x + " " + n.Position.y);
                    if (Links[3] == null) {
                        if (!DoesOverlap(n)) {
                            Debug.Log("new quad4 node " + n.Position.x + " " + n.Position.y);
                            Links[3] = n;
                            return PlaceNode(n);
                        }
                    } else {
                        Links[3].TryInsert(n);
                    }
                } else {
                    Debug.LogWarningFormat("couldnt find a suitable quadrant to insert into[{0}]", Time.time);
                }

                return null;
            }

            public GameObject PlaceNode(Node n) {
                GameObject go = new GameObject("node");
                go.transform.position = n.Position;
                // go.transform.SetParent(parent);

                CircleCollider2D c = go.AddComponent<CircleCollider2D>();
                c.radius = 0.5f;
                // go.transform.parent =
                return go;
            }
        }

        public Transform RootTransform;
        public Node Root;

        public static QuadTree CreateQuadTree(Vector3 rootPosition) {
            GameObject goContainer = new GameObject("quad_tree");
            GameObject go = new GameObject("quad_tree_root");

            QuadTree tree = go.AddComponent<QuadTree>();
            tree.RootTransform = go.transform;
            tree.Root = new Node(rootPosition);

            go.transform.SetParent(goContainer.transform);
            go.transform.position = Vector3.zero;

            goContainer.transform.position = Vector3.zero;

            return tree;
        }

        public IEnumerator GenerateFromSeed(int numNodes, long seed) {
            // todo set seed
            int createdSoFar = 0;
            int spawnAttempts = 0;
            int maxAttempts = 20;
            List<GameObject> nn = new List<GameObject>();

            while (createdSoFar < 10) {
                while (spawnAttempts < maxAttempts) {
                    float multi = (spawnAttempts * 5);
                    Vector3 p = new Vector3(UnityEngine.Random.Range(-10 - multi, 10 + multi),UnityEngine.Random.Range(-10 - multi, 10 + multi),0f);
                    Debug.LogFormat("try one {0} {1}", p.x, p.y);
                    GameObject success = Root.TryInsert(new Node(p));
                    yield return new WaitForSeconds(0.3f);
                    if (success != null) {
                        Debug.LogFormat("got one {0} {1}", p.x, p.y);
                        nn.Add(success);
                        spawnAttempts = 0;
                        break;
                    }

                    ++spawnAttempts;
                    
                    if (spawnAttempts >= maxAttempts) { // debug
                        Debug.LogWarningFormat("max attempts reached [{0}]", Time.time);
                    }
                }
                ++createdSoFar;
            }

            Debug.LogFormat("length of list is {0}", nn.Count);
            yield return null;
        }
    }
}