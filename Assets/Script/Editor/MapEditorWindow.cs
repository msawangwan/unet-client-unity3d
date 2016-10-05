using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow {

    static EditorWindow mapEditor = null;

    string fieldLabelInput = "<empty>";
    bool enableToggleGroup;   
    bool enableToggle = true;
    float floatValue = 0.0f;

    // add menu item, named after the string we pass in
    [MenuItem(StringConstant.WindowMenu.MapEditor)]
    public static void ShowWindow () {
        mapEditor = EditorWindow.GetWindow(typeof(MapEditorWindow));
        mapEditor.titleContent.text = "Map Editor";
    }

    private void OnGUI() {
        GUILayout.Label ( "Map Editor Settings", EditorStyles.boldLabel);
        fieldLabelInput = EditorGUILayout.TextField ("Field Label", fieldLabelInput);

        /* start grp */
        enableToggleGroup = EditorGUILayout.BeginToggleGroup ("Enable Group Label", enableToggleGroup);
        enableToggle = EditorGUILayout.Toggle ("Toggle Label", enableToggle);
        floatValue = EditorGUILayout.Slider ("Slider Label", floatValue, -10, 10);
        EditorGUILayout.EndToggleGroup ();
        /* end grp */
    }
}
