using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float maxSpeed = 5f;

    void Update()

    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, 0, maxSpeed * Time.deltaTime);

        pos -= transform.rotation * velocity;
        transform.position = pos;
    }
}
