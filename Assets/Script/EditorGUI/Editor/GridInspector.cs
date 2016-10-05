using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(GizmoGrid))]
public class GridInspector : Editor {

    GizmoGrid grid = null;

    void OnEnable () {
        grid = target as GizmoGrid;
        SceneView.onSceneGUIDelegate -= GridUpdate;
        SceneView.onSceneGUIDelegate += GridUpdate;
    }

    void OnDisable () {
        //SceneView.onSceneGUIDelegate -= GridUpdate;
    }

    void GridUpdate (SceneView sceneView) {
        Event e = Event.current;

        if (e.isKey && e.character == 'a') {
            Debug.Log("keypress");
            GameObject go;
            Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);

            if (prefab) {
                go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                go.transform.position = Vector3.zero;
            }
        }
    }

    public override void OnInspectorGUI() {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ( "Grid Cell Width" );
        grid.CellWidth = EditorGUILayout.FloatField(grid.CellWidth, GUILayout.Width(50));
        GUILayout.EndHorizontal ();

        GUILayout.BeginHorizontal ();
        GUILayout.Label ( "Grid Cell Height" );
        grid.CellHeight = EditorGUILayout.FloatField(grid.CellHeight, GUILayout.Width(50));
        GUILayout.EndHorizontal ();

        SceneView.RepaintAll();
    }
}
