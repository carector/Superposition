using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotateTowards : MonoBehaviour
{
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>().transform;
    }

    private void LateUpdate()
    {
        Vector3 rot = cam.eulerAngles;
        transform.rotation = Quaternion.Euler(rot);
    }
}
