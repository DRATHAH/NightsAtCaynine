using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharDialogueData))]
public class CharDialogueDataEditor : Editor
{
    SerializedProperty characterName;
    SerializedProperty dialogueColor;
    SerializedProperty interactions;

    private void OnEnable()
    {
        characterName = serializedObject.FindProperty("characterName");
        dialogueColor = serializedObject.FindProperty("dialogueColor");
        interactions = serializedObject.FindProperty("interactions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Character information
        EditorGUILayout.PropertyField(characterName);
        EditorGUILayout.PropertyField(dialogueColor);
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Interactions", EditorStyles.boldLabel);

        // Draw existing interactions
        for (int i = 0; i < interactions.arraySize; i++)
        {
            SerializedProperty interaction = interactions.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            SerializedProperty interactionName = interaction.FindPropertyRelative("interactionName");

            string displayName = string.IsNullOrEmpty(interactionName.stringValue) ? $"Interaction {i}" : interactionName.stringValue;
            EditorGUILayout.PropertyField(interaction, new GUIContent(displayName), true);

            if (GUILayout.Button("Remove Interaction"))
            {
                interactions.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        EditorGUILayout.Space(5);

        // Add interaction button
        if (GUILayout.Button("Add Interaction"))
        {
            AddInteraction();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void AddInteraction()
    {
        int index = interactions.arraySize;
        interactions.arraySize++;
        
        SerializedProperty newInteraction = interactions.GetArrayElementAtIndex(index);
        newInteraction.FindPropertyRelative("interactionName").stringValue = "New Interaction";
        SerializedProperty dialogue = newInteraction.FindPropertyRelative("dialogue");
        dialogue.arraySize = 0;
    }
}
