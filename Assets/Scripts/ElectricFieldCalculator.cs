/************************

Physics 122: Superposition Mini-Project
Caleb Rector

Several other scripts were written for the interactive portion of the project, but this script does all the heavy lifting.
You can view the other scripts here: https://github.com/carector/Superposition/tree/main/Assets/Scripts/Plumbing

*************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFieldCalculator
{
    // Most recent calculations, utilized by FieldDisplayer for visuals
    public List<Vector3> fields = new List<Vector3>();
    public Vector3 netField;

    float epsilon = 8.85419f * Mathf.Pow(10, -12);

    // Charge class stores charge magnitude and its position in space
    public class Charge
    {
        public float magnitude;
        public Vector3 position;
        public Charge(float _magnitude, Vector3 _position)
        {
            magnitude = _magnitude;
            position = _position;
        }
    }

    public void TestCases()
    {
        Charge c1, c2, c3, c4;
        Vector3 point = Vector3.zero;
        Vector3 result;

        // Test 1:
        // Charge of 1C placed at {4, 0, 0}
        c1 = new Charge(1, new Vector3(4, 0, 0));
        result = CalculateNetFieldAtPoint(new Charge[] { c1 }, point);
        Debug.Log(result);

        // Output: (-561721900.0, 0.0, 0.0)

        // Test 2:
        // Charge of 1C placed at {4, 0, 0}
        // Charge of 1C placed at {2, 2, 0}
        c1 = new Charge(1, new Vector3(4, 0, 0));
        c2 = new Charge(1, new Vector3(2, 2, 0));
        result = CalculateNetFieldAtPoint(new Charge[] { c1, c2 }, point);
        Debug.Log(result);

        // Output: (-1356116000.0, -794394600.0, 0.0)

        // Test 3:
        // Charge of 1C placed at {4, 0, 0}
        // Charge of 1C placed at {-4, 0, 0}
        // Charge of 1C placed at {0, 4, 0}
        // Charge of 1C placed at {0, -4, 0}
        c1 = new Charge(1, new Vector3(4, 0, 0));
        c2 = new Charge(1, new Vector3(-4, 0, 0));
        c3 = new Charge(1, new Vector3(0, 4, 0));
        c4 = new Charge(1, new Vector3(0, -4, 0));
        result = CalculateNetFieldAtPoint(new Charge[] { c1, c2, c3, c4 }, point);
        Debug.Log(result);

        // Output: (0.0, 0.0, 0.0)
    }

    // Calculates the net field at a point in space
    public Vector3 CalculateNetFieldAtPoint(Charge[] charges, Vector3 point)
    {
        // Delete any leftover data from last calculation
        Vector3 net = Vector3.zero;
        fields.Clear();

        // Calculate field for each charge influencing a point
        for (int i = 0; i < charges.Length; i++)
        {
            Vector3 field = CalculateFieldAtPoint(charges[i], point);
            net += field;
            fields.Add(field);
        }

        netField = net;
        return netField;
    }

    // Calculates an individual electric field between a charge and a point
    public Vector3 CalculateFieldAtPoint(Charge q0, Vector3 point)
    {
        float r = Vector2.Distance(q0.position, point);
        Vector3 direction = (point - q0.position).normalized; // Direction points from q0 to point

        // Based on formula 26-2 in textbook, calculate Coulomb's Law
        float magnitude = Mathf.Abs(q0.magnitude) / (4 * Mathf.PI * r * r * epsilon);

        return direction * magnitude * Mathf.Sign(q0.magnitude);
    }
}
