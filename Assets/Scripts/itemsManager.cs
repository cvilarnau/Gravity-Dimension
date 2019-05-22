using UnityEngine;

public class itemsManager : MonoBehaviour
{
    float speed = 5f;

    void Update()
    {
        transform.Rotate(0, 80 * Time.deltaTime, 0);

        float newY = Mathf.Sin(Time.time * speed);
        Vector3 pos = new Vector3(transform.position.x, newY - 1.5f, transform.position.z);
        transform.position = pos;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ship")
        {
            print("PLEXK");
        }
    }
}
