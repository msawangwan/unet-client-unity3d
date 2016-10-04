using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PropDrawTest))]
public class AunitDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect amountRect = new Rect(position.x, position.y, 30, position.height);
        Rect unitRect = new Rect(position.x+35, position.y, 50, position.height);
        Rect nameRect = new Rect(position.x+90, position.y, position.width, position.height);

        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}