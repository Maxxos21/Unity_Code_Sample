using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillater : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartingPos();
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    private void StartingPos()
    {
        startingPosition = transform.position;
    }

    private void Oscillate()
    {
        if (period <= Mathf.Epsilon) { return; } // remove NaN error, Mathf.Epislon smallest 0 (float)

        float cycle = Time.time / period; // Growing over time 

        const float tau = Mathf.PI * 2; //constant value of 6,283 (tau)
        float rawSinWave = Mathf.Sin(cycle * tau); // going between -1 and 1


        movementFactor = rawSinWave; // -1 to 1 (starting = mid point) 
        //movementFactor = (rawSinWave + 1f) / 2f; to get 0 to 1(0 starting poitn)



        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
