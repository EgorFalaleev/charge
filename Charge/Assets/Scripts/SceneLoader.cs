using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // state variables
    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextLevel()
    {
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings) LoadMainMenu();
        else SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevelSelectionScreen()
    {
        SceneManager.LoadScene("Level Selection Screen");
    }

    public void LoadLevelWithIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }    

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
