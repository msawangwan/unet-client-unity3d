using UnityEngine;
using UnityEditor;

public class TestWindow : EditorWindow {

    string mystr = "ffka";
    bool grpEnabled;
    bool myBo = true;
    float myFlot = 1.22222f;

    // add menu item, named after the string we pass in
    [MenuItem(StringConstant.WindowMenu.MapEditor)]
    public static void ShowWindow () {
        EditorWindow.GetWindow(typeof(TestWindow));
    }

    private void OnGUI() {
        GUILayout.Label ( "Base Settings", EditorStyles.boldLabel);
        mystr = EditorGUILayout.TextField ("Tessssssttext", mystr);

        /* toggle grp */
        grpEnabled = EditorGUILayout.BeginToggleGroup ("optional sett", grpEnabled);
        myBo = EditorGUILayout.Toggle ("togggleee", myBo);
        myFlot = EditorGUILayout.Slider ("sliiiiiiiiiiiiider", myFlot, -10, 10);
        EditorGUILayout.EndToggleGroup ();
    }
}
