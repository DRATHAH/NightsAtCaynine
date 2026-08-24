using UnityEngine;
using System;
using UnityEditor;

[CustomPropertyDrawer(typeof(DialogueNode), true)]
public class DialogueNodeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // If there isn't a concrete object assigned yet, show a button allowing the user to make one
        if (property.managedReferenceValue == null)
        {
            if (GUI.Button(position, "Add Dialogue Node"))
            {
                ShowNodeMenu(property);
            }

            EditorGUI.EndProperty();
            return;
        }

        // Draw the actual concrete type
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndProperty();
    }

    void ShowNodeMenu(SerializedProperty property)
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("Dialogue Line"), false, () => CreateNode<DialogueNode>(property));
    }

    void CreateNode<T>(SerializedProperty property)
        where T : DialogueNode
    {
        //property.managedReferenceValue = new T();
        property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue == null)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
