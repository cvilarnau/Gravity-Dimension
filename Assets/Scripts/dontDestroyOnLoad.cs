using UnityEngine;
using System.Collections;

public class dontDestroyOnLoad : MonoBehaviour
{

    public GameObject[] music;
    private int count = 0;

    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("gameMusic");
        foreach (GameObject musics in music)
        {
            if (count == 1)
            {
                Destroy(musics);
            }
            count++;
        }
    }

    // Update is called once per frame
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
