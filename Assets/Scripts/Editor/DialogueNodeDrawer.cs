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

        // Draw the foldout and update isExpanded when clicked
        Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

        // Don't draw children if collapsed
        if (!property.isExpanded)
        {
            EditorGUI.EndProperty();
            return;
        }
        // Draw foldout's children
        EditorGUI.indentLevel++;
        float y = position.y + EditorGUIUtility.singleLineHeight;
        // Make a copy so we can iterate without modifying the original property
        SerializedProperty child = property.Copy();
        SerializedProperty endProperty = child.GetEndProperty();
        // Move into the first child
        bool enterChildren = true;
        while (child.NextVisible(enterChildren) && !SerializedProperty.EqualContents(child, endProperty))
        {
            float height = EditorGUI.GetPropertyHeight(child, true);
            Rect childRect = new Rect(position.x, y, position.width, height);
            EditorGUI.PropertyField(childRect, child, true);
            y += height + EditorGUIUtility.standardVerticalSpacing;
            enterChildren = false;
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    void ShowNodeMenu(SerializedProperty property)
    {
        GenericMenu menu = new GenericMenu();

        // Creates different menu buttons for each type of dialogue
        menu.AddItem(new GUIContent("Dialogue Line"), false, () => CreateNode<DialogueNode>(property));
        menu.AddItem(new GUIContent("Dialogue Question"), false, () => CreateNode<DialogueOptions>(property));
        menu.AddItem(new GUIContent("Swap Speaker"), false, () => CreateNode<DialogueSwapSpeaker>(property));
        menu.AddItem(new GUIContent("Prerequisite"),false, () => CreateNode<PrerequisiteCheck>(property));
        menu.AddItem(new GUIContent("End Dialogue"), false, () => CreateNode<EndDialogue>(property));

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

        if (!property.isExpanded)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        // Expanded foldout line + all child properties
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}