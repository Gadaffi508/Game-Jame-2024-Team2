#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using TMPro;

[CustomEditor(typeof(FutureRobot))]
public class FutureRobotEditor : Editor
{
    private FutureRobot futureRobot;
    private SerializedObject serializedFutureRobot;

   
    SerializedProperty destinationsProp;
    SerializedProperty playerProp;
    SerializedProperty maxDistanceProp;
    SerializedProperty warningTextProp;
    SerializedProperty dialogueTextProp;
    SerializedProperty messagesProp;

    private void OnEnable()
    {
        futureRobot = (FutureRobot)target;
        serializedFutureRobot = new SerializedObject(futureRobot);

        // Initialize serialized properties
        destinationsProp = serializedFutureRobot.FindProperty("destinations");
        playerProp = serializedFutureRobot.FindProperty("player");
        messagesProp = serializedFutureRobot.FindProperty("messages");
        maxDistanceProp = serializedFutureRobot.FindProperty("maxDistance");
        warningTextProp = serializedFutureRobot.FindProperty("warningText");
        dialogueTextProp = serializedFutureRobot.FindProperty("dialogueText");
    }

    public override void OnInspectorGUI()
    {
        serializedFutureRobot.Update();

        EditorGUILayout.Space();
        GUILayout.Label("Future Robot Settings", new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 18
        });
        EditorGUILayout.Space();

        
        EditorGUILayout.PropertyField(destinationsProp, new GUIContent("Destinations"), true);
        EditorGUILayout.PropertyField(playerProp, new GUIContent("Player"), true);
        EditorGUILayout.PropertyField(maxDistanceProp, new GUIContent("Max Distance"), true);
        EditorGUILayout.PropertyField(warningTextProp, new GUIContent("Warning Text"), true);
        EditorGUILayout.PropertyField(dialogueTextProp, new GUIContent("Dialogue Text"), true);
        EditorGUILayout.PropertyField(messagesProp, new GUIContent("Messages"), true);

        EditorGUILayout.Space();

        serializedFutureRobot.ApplyModifiedProperties();
    }
}
#endif