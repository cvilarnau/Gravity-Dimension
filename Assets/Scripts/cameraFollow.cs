using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    Vector3 tempVec3 = new Vector3();

    void Start()
    {
        Destroy(GameObject.Find("Main CameraMenu"));
    }

    void LateUpdate()
    {
        tempVec3.x = this.transform.position.x;
        tempVec3.y = this.transform.position.y;
        tempVec3.z = targetTransform.position.z - 95.6f;
        this.transform.position = tempVec3;
    }
}
