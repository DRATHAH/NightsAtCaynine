using UnityEngine;
using System;
using UnityEditor;

[CustomPropertyDrawer(typeof(DialogueNode), true)]
public class DialogueNodeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // This drawer is only supposed to handle managed references (if it's a normal serialized type, don't do anything)
        if (property.propertyType != SerializedPropertyType.ManagedReference)
        {
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndProperty();
            return;
        }

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

        // Creates different menu buttons for each type of dialogue
        menu.AddItem(new GUIContent("Dialogue Line"), false, () => CreateNode<DialogueNode>(property));
        menu.AddItem(new GUIContent("Dialogue Sequence"), false, () => CreateNode<DialogueSequence>(property));
        menu.AddItem(new GUIContent("Dialogue Question"), false, () => CreateNode<DialogueOptions>(property));
        menu.AddItem(new GUIContent("Swap Speaker"), false, () => CreateNode<DialogueSwapSpeaker>(property));

        menu.ShowAsContext(); // Actually shows the options in the editor
    }

    void CreateNode<T>(SerializedProperty property)
        where T : DialogueNode, new()
    {
        property.managedReferenceValue = new T();
        property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ManagedReference)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        if (property.managedReferenceValue == null)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}