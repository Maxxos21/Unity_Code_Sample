using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Range(-1, 1)] [SerializeField] float xSpin = 0f;
    [Range(-1, 1)] [SerializeField] float ySpin = 0f;
    [Range(-1, 1)] [SerializeField] float zSpin = 0f;


    // Update is called once per frame
    void Update()
    {
        Spinning();
    }

    private void Spinning()
    {
        transform.Rotate(xSpin, ySpin, zSpin);
    }
}
