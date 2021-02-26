using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string levelToLoad;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
