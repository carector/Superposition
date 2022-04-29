using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeVisuals : MonoBehaviour
{
    float scale = 0.0000000125f;

    LineRenderer line;
    Transform tail;
    Transform point;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        line.positionCount = 2;
        tail = transform.GetChild(1).transform;
        point = GameObject.Find("Point").transform;
    }

    public void UpdateVisuals(Vector3 fieldOffset)
    {
        line.SetPositions(new Vector3[] { point.transform.position, point.transform.position + fieldOffset*scale });
        tail.transform.position = line.GetPosition(1);

        // Angle calculation
        Vector3 target = line.GetPosition(1) - line.GetPosition(0);
        tail.transform.rotation = Quaternion.LookRotation(target);
    }
}
