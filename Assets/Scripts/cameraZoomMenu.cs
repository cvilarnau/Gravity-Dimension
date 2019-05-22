using UnityEngine;

public class cameraZoomMenu : MonoBehaviour
{
    float speed = 4f;
    bool ping = true;
    bool pong = false;

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, 0, speed * Time.deltaTime);

        if (transform.position.z < 400f && !pong)
        {
            ping = false;
            pong = true;
        }

        if (transform.position.z > 660f && !ping)
        {
            ping = true;
            pong = false;
        }

        if (ping)
        {
            pos -= transform.rotation * velocity;
            transform.position = pos;
        }

        if (pong)
        {
            pos += transform.rotation * velocity;
            transform.position = pos;
        }
        
    }
}
