using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){ return; }
        float cycles = Time.time / period; // Contunally growing over time
        const float tau = Mathf.PI * 2; // Constant value
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // Recalculating to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
