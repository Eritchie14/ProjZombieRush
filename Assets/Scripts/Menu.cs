using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string ScenetoLoad = "Level 1";
    public void playGame()
    {
        SceneManager.GetActiveScene();;
        Time.timeScale = 1;
    }

    public void exitGame()
    {
        Application.Quit();
    }
    public void goToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void restartGame()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }
}
