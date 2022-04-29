using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDisplayer : MonoBehaviour
{
    public ChargeVisuals point;
    public GameObject chargePrefab;

    ElectricFieldCalculator efc;
    ElectricFieldCalculator.Charge[] charges;
    Animator pointAnimator;

    public ChargeVisuals[] spawnedCharges;
    Vector3 lastPointPosition;
    bool movingPoint;

    // Start is called before the first frame update
    void Start()
    {
        efc = new ElectricFieldCalculator();
        efc.TestCases();
        pointAnimator = point.GetComponent<Animator>();

        charges = new ElectricFieldCalculator.Charge[8];
        spawnedCharges = new ChargeVisuals[charges.Length];
        for (int i = 0; i < charges.Length; i++)
            spawnedCharges[i] = Instantiate(chargePrefab, Vector2.zero, Quaternion.identity).GetComponent<ChargeVisuals>();

        StartCoroutine(InitialDelay());
    }

    void RandomizeChargePositions()
    {
        for (int i = 0; i < charges.Length; i++)
        {
            charges[i] = new ElectricFieldCalculator.Charge(1, new Vector3(Random.Range(-10, 10f), Random.Range(-10, 10f), Random.Range(-10, 10f)));
            spawnedCharges[i].transform.position = charges[i].position;
        }
        Recalculate();
    }

    IEnumerator InitialDelay() // Single frame delay for charge points to spawn
    {
        yield return new WaitForEndOfFrame();
        RandomizeChargePositions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Only recalculate fields if point moves
        if (point.transform.position != lastPointPosition)
        {
            Recalculate();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RandomizeChargePositions();
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (movingPoint)
                pointAnimator.Play("IdlePoint");
            else
                pointAnimator.Play("MovePoint");
            movingPoint = !movingPoint;
        }
    }

    void Recalculate()
    {
        // Calculate net field
        efc.CalculateNetFieldAtPoint(charges, point.transform.position);

        // Update visuals
        point.UpdateVisuals(efc.netField);
        for (int i = 0; i < spawnedCharges.Length; i++)
            spawnedCharges[i].UpdateVisuals(efc.fields[i]);

        // Store the last charge position so we don't have to calculate every frame
        lastPointPosition = point.transform.position;
    }
}
