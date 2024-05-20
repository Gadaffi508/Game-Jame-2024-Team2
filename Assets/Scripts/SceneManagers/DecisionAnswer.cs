using UnityEngine;

public class DecisionAnswer : MonoBehaviour
{
    public enum GameType
    {
        End,
        LoadGame
    }

    TransationManager transationManager;
    private void Start() => transationManager = GetComponent<TransationManager>();

    private void Update()
    {
        PressAnswer(KeyCode.Y,GameType.LoadGame);
        PressAnswer(KeyCode.N,GameType.End);
    }

    private void PressAnswer(KeyCode keyCode,GameType type)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (type == GameType.LoadGame) Sceneload();
            if (type == GameType.End) FinishGame();
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    void Sceneload()
    {
        Debug.Log("Game");
    }

    void FinishGame()
    {
        Debug.Log("Finish");
        transationManager.SceneLoad(4);
    }
}
