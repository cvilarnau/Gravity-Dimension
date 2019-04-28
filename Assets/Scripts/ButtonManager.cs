using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    /*public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }*/
}
