using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "openmain"; 

    public void OnNewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnSettings()
    {
        Debug.Log("Open Settings Panel");
    }

    public void OnQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}