using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    Vector3 tempVec3 = new Vector3();

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset.z = transform.position.z - player.transform.position.z;
        Destroy(GameObject.Find("Main CameraMenu"));
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        tempVec3.x = this.transform.position.x;
        tempVec3.y = this.transform.position.y;
        tempVec3.z = targetTransform.position.z - 95.6f;
        this.transform.position = tempVec3;
    }
}
