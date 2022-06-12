using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;
    float movementFactor;

    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    void Update()
    {
        // Fixes NaN error if period = 0
        if (period <= Mathf.Epsilon) // using Mathf.Epsilon since period is of float type and Epsilon is smallest float number  
        { 
            return; 
        } 

        // Some Sin wave shit using tau cause PI is wrong?
        float cycles = Time.time / period; //continually growing over time
        const float tau = Mathf.PI * 2; // const value of 6.283

        float sinWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        movementFactor = (sinWave + 1f) / 2f; // recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor; // setting offset

        transform.position = startingPosition + offset; // setting new position
    }
}
