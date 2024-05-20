using UnityEngine;
using UnityEngine.SceneManagement;

public class TransationManager : MonoBehaviour
{
    public void SceneLoad(int _index)
    {
        SceneManager.LoadScene(_index);
    }

    public void NextSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
