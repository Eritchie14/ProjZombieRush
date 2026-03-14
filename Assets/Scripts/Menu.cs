using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string ScenetoLoad = "Level1";
    public void playGame()
    {
        SceneManager.LoadScene(ScenetoLoad);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
