using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NewPlayerController))]

public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //기본 인스펙터 를 그리기
        base.OnInspectorGUI();

        //타겟  컴포넌트 참조 가져오기
        NewPlayerController playerController = (NewPlayerController)target;


        // 여백 추가
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("상태 디버그 정보",EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        switch(playerController.CurrentState)
        {
            case PlayerState.Idle:
                GUI.backgroundColor = new Color(1, 1, 1, 1);
                break;
            case PlayerState.Move:
                GUI.backgroundColor = new Color(1, 0, 0, 1); 
                break;
            case PlayerState.Jump:
                GUI.backgroundColor = new Color(0, 1, 0, 1); 
                break;
            case PlayerState.Attack:
                GUI.backgroundColor = new Color(0, 0, 1, 1); 
                break;
            case PlayerState.Hit:
                GUI.backgroundColor = new Color(1, 1, 0, 1); 
                break;
            case PlayerState.Dead:
                GUI.backgroundColor = new Color(0, 1, 1, 1); 
                break;
            case PlayerState.None:
                GUI.backgroundColor = new Color(1, 0, 1, 1); 
                break;
        }
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("현재 상태", playerController.CurrentState.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();

        //지면 접촉 상태
        GUI.backgroundColor = Color.white;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("플레이어 위치디버그  정보", EditorStyles.boldLabel);
        GUI.enabled = false; // 읽기전용으로 만들기
        EditorGUILayout.Toggle("지면 접촉", playerController.IsGrounded);
        GUI.enabled = true;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        if (GUILayout.Button("Idle"))
        {
            playerController.SetState(PlayerState.Idle);
        }
        if (GUILayout.Button("Move"))
        {
            playerController.SetState(PlayerState.Move);
        }
        if (GUILayout.Button("Jump"))
        {
            playerController.SetState(PlayerState.Jump);
        }
        if (GUILayout.Button("Attack"))
        {
            playerController.SetState(PlayerState.Attack);
        }
        if (GUILayout.Button("Hit"))
        {
            playerController.SetState(PlayerState.Hit);
        }
        if (GUILayout.Button("Dead"))
        {
            playerController.SetState(PlayerState.Dead);
        }

            EditorGUILayout.EndVertical();
    }
}
