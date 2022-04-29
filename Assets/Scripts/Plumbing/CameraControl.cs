using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    bool validClick;
    Transform camera;
    Vector2 smoothPanAmount;
    Vector2 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        camera = transform.GetChild(0);
        lastMousePosition = Input.mousePosition;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = (Input.mousePosition - (Vector3)lastMousePosition) / Mathf.Clamp(25 / Mathf.Abs(camera.transform.localPosition.z), 2, 10);

        if (Input.GetMouseButton(0))
        {
            if (mouseDelta.magnitude > 0f)
                validClick = false;

            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x - mouseDelta.y, euler.y + mouseDelta.x, 0);
            if (transform.rotation.x > 85 || transform.rotation.x < -85)
                transform.rotation = Quaternion.Euler(85 * Mathf.Sign(transform.rotation.eulerAngles.x), transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
                smoothPanAmount = mouseDelta;

            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x - smoothPanAmount.y, euler.y + smoothPanAmount.x, 0);
            smoothPanAmount /= 1.05f;
        }

        if (Input.mouseScrollDelta.y != 0)
            camera.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(camera.transform.localPosition.z + Input.mouseScrollDelta.y, -30f, -3f));

        lastMousePosition = Input.mousePosition;
    }
}
