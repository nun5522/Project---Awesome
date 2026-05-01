using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    public string firstSceneName = "Scene1";

    public void StartGame()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}