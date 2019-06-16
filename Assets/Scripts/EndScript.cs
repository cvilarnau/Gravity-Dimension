using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    void Start()
    {
        Invoke("End", 6);
    }

    void End()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
