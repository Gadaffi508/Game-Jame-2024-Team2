using System.Collections;
using UnityEditor;
using UnityEngine;

public class ChracterTimeController : MonoBehaviour
{
    public float normalTimeScale = 1f;
    public float fastTimeScale = 2f;
    public float slowTimeScale = 0.5f;

    public float abilityDuration = 10f;
    public float cooldownDuration = 30f;

    private bool canUseAbility = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canUseAbility)
        {
            StartCoroutine(UseAbility(fastTimeScale));
            Debug.Log("1");
        }
        else if (Input.GetKeyDown(KeyCode.L) && canUseAbility)
        {
            StartCoroutine(UseAbility(slowTimeScale));
            Debug.Log("2");
        }

    }

    private IEnumerator UseAbility(float timeScale)
    {
        canUseAbility = false;
        Time.timeScale = timeScale;

        yield return new WaitForSecondsRealtime(abilityDuration);

        SetNormalTime();

        yield return new WaitForSecondsRealtime(cooldownDuration - abilityDuration);

        canUseAbility = true;
    }

    private void SetNormalTime()
    {
        Time.timeScale = normalTimeScale;
    }
}

// Custom Editor
#if UNITY_EDITOR
[CustomEditor(typeof(ChracterTimeController)), InitializeOnLoadAttribute]
public class ChracterTimeControllerEditor : Editor
{
    ChracterTimeController fpc;
    SerializedObject SerFPC;

    private void OnEnable()
    {
        fpc = (ChracterTimeController)target;
        SerFPC = new SerializedObject(fpc);
    }

    public override void OnInspectorGUI()
    {
        SerFPC.Update();

        EditorGUILayout.Space();
        GUILayout.Label("Modular Time Player Controller ", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
        GUILayout.Label("Yusuf Arslan", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        GUILayout.Label("version 1.0.1", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Time Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        fpc.abilityDuration = EditorGUILayout.Slider(new GUIContent("Ability Duration", "Determines how fast the player will move while walking."), fpc.abilityDuration, .1f,20);
        fpc.cooldownDuration = EditorGUILayout.Slider(new GUIContent("Cooldown Duration", "Determines how fast the player will move while walking."), fpc.cooldownDuration, .1f,20);


        SerFPC.ApplyModifiedProperties();
    }
}
#endif
