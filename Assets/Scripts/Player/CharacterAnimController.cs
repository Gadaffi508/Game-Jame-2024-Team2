using UnityEditor;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    FirstPersonController _firstPersonController;
    Animator _anim;

    private void Start()
    {
        _firstPersonController = GetComponentInParent<FirstPersonController>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Sprinting() is false)
            _anim.SetFloat("Velocity", 0);
        else
            _anim.SetFloat("Velocity", 1);


        if (Walking() is false)
            _anim.SetFloat("CrouchVelocity", 0);
        else
            _anim.SetFloat("CrouchVelocity", 1);


        _anim.SetBool("walk",_firstPersonController.isWalking);

        _anim.SetBool("crouch", _firstPersonController.isCrouched);
    }

    private bool Sprinting() => _firstPersonController.isSprinting;
    private bool Walking() => _firstPersonController.isWalking;
}

// Custom Editor
#if UNITY_EDITOR
[CustomEditor(typeof(CharacterAnimController)), InitializeOnLoadAttribute]
public class CharacterAnimControllerEditor : Editor
{
    CharacterAnimController fpc;
    SerializedObject SerFPC;

    private void OnEnable()
    {
        fpc = (CharacterAnimController)target;
        SerFPC = new SerializedObject(fpc);
    }

    public override void OnInspectorGUI()
    {
        SerFPC.Update();

        EditorGUILayout.Space();
        GUILayout.Label("Modular Anim Controller ", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
        GUILayout.Label("Yusuf Arslan", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        GUILayout.Label("version 1.0.1", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        EditorGUILayout.Space();
    }
}
#endif
