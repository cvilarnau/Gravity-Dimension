using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
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
    bool barrelTuto = false;

    float maxfuel = 100f;
    public Image fuelbar;
    public float fuel;

    public GameObject startMessage;
    public GameObject endMessage;
    public GameObject barrelMessage;

    public GameObject explosion;
    private Renderer[] rend;
    private BoxCollider colli;
    public GameObject candle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        menuGameOver.SetActive(false);
        menuPause.SetActive(false);
        barrelMessage.SetActive(false);
        Time.timeScale = 0;

        fuel = maxfuel;

        endMessage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !dead && !pause && !levelFinished && !barrelTuto)
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

        if (!pause && !dead && start && !levelFinished && !barrelTuto)
        {
            transform.Translate(Input.acceleration.x, 0, 0);
        }

        fuelbar.fillAmount = fuel / maxfuel;

        if (touchingPlatform && !barrelTuto && !dead)
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

        if (barrelTuto && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            barrelMessage.SetActive(false);
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
            Time.timeScale = 0;
            endMessage.SetActive(true);
            levelFinished = true;
        }
    }

    public void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Exit()
    {
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

        if (collision.gameObject.tag == "barrelTrigger")
        {
            if (!barrelTuto)
            {
                barrelMessage.SetActive(true);
                Time.timeScale = 0;
                barrelTuto = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "barrelTrigger")
        {
            if (barrelTuto && !dead)
            {
                barrelTuto = false;
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
}
