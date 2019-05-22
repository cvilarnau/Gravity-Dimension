using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float maxSpeed;

    private void Start()
    {
        maxSpeed = 6f;
    }

    void Update()

    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, 0, maxSpeed * Time.deltaTime);

        pos -= transform.rotation * velocity;
        transform.position = pos;
    }
}
