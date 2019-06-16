using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipMovementNoTuto : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;

    bool touchingPlatform = false;
    bool dead = false;
    bool tunnel = false;

    public GameObject menuGameOver;
    public GameObject menuPause;

    bool pause = false;
    bool velocityFast = false;
    bool start = false;
    bool levelFinished = false;

    float maxfuel = 100f;
    public Image fuelbar;
    public float fuel;

    public GameObject startMessage;
    public GameObject endMessage;

    public GameObject explosion;
    private Renderer[] rend;
    private BoxCollider colli;
    public GameObject candle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        menuGameOver.SetActive(false);
        menuPause.SetActive(false);
        Time.timeScale = 0;

        fuel = maxfuel;

        endMessage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !dead && !pause && !levelFinished)
        {
            Time.timeScale = 1;
            start = true;
            Destroy(startMessage);
        }

        rb.AddForce(Physics.gravity, ForceMode.Acceleration);

        // movimiento provisional para pruebas en local con teclado
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * 25f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.left * -25f * Time.deltaTime;
        }
        ////////////////////////////////////////////////////////////

        if (!pause && !dead && start && !levelFinished && velocityFast)
        {
            transform.Translate(Input.acceleration.x, 0, 0);
        }

        fuelbar.fillAmount = fuel / maxfuel;

        if (touchingPlatform && !dead && velocityFast)
        {
            if (Input.GetMouseButtonDown(0) && (EventSystem.current.currentSelectedGameObject == null) && (fuelbar.fillAmount != 0f))
            {
                rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
                Jump();
            }
        }

        if (transform.position.y < -100 && !dead)
        {
            dead = true;
            Time.timeScale = 0;
            menuGameOver.SetActive(true);
        }

        if (tunnel)
        {
            //sonido.PlayOneShot(sonidoExplosion);

            rend = GetComponentsInChildren<MeshRenderer>();
            foreach (Renderer render in rend)
            {
                render.enabled = false;
            }

            colli = GetComponent<BoxCollider>();
            colli.enabled = false;

            candle.SetActive(false);

            Instantiate(explosion, transform.position, transform.rotation);

            Invoke("Dead", 4);

            dead = true;

            tunnel = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingPlatform = true;
        } 
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingPlatform = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "tunnel")
        {
            tunnel = true;
        }

        if (collision.gameObject.tag == "endCube")
        {
            GameObject.Find("Platforms").GetComponent<PlatformMovement>().maxSpeed = 0f;
            endMessage.SetActive(true);
            levelFinished = true;
            Invoke("End", 4);
        }
    }

    public void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Exit()
    {
        GameObject[] music = GameObject.Find("gameMusic").GetComponent<dontDestroyOnLoad>().music;
        Destroy(music[0]);
        SceneManager.LoadScene("MainScreen");
    }

    public void Pause()
    {
        if (!pause && !dead)
        {
            menuPause.SetActive(true);
            Time.timeScale = 0;
            pause = true;
        }
    }

    public void Resume()
    {
        if (!dead)
        {
            menuPause.SetActive(false);
            Time.timeScale = 1;
            pause = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "velocityTrigger")
        {
            if (!velocityFast)
            {
                GameObject.Find("Platforms").GetComponent<PlatformMovement>().maxSpeed = 100f;
                velocityFast = true;
            }
        }
    }

    // Restamos fuel con el salto
    public void Jump()
    {
        fuel -= 20f;
    }

    public void Dead()
    {
        Time.timeScale = 0;
        menuGameOver.SetActive(true);
    }

    public void End()
    {
        GameObject[] music = GameObject.Find("gameMusic").GetComponent<dontDestroyOnLoad>().music;
        Destroy(music[0]);
        SceneManager.LoadScene("Credits");
    }
}
