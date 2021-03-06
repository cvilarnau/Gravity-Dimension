﻿using UnityEngine;

public class itemsManagerNoTuto : MonoBehaviour
{
    float speed = 5f;
    public Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

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
            GameObject ship = GameObject.Find("StarSparrow5");
            ShipMovementNoTuto script = ship.GetComponent<ShipMovementNoTuto>();
            script.fuel = 100f;

            rend.enabled = false;
        }
    }
}
