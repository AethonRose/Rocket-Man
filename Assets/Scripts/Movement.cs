using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float ThrustUp = 1000f;
    [SerializeField] float ThrustRotation = 25f;

    Rigidbody rb;
    AudioSource thrustSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Keyboard (Space) - ProcessThrust
    void ProcessThrust()
    {
        // If Space is Pressed AddRelativeForce going Up and Play thrustSound
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * ThrustUp * Time.deltaTime);
            if(!thrustSound.isPlaying)
            {
                thrustSound.Play();
            }
        }
        // If Space !Pressed then stop thrustSound
        else
        {
            thrustSound.Stop();
        }
    }

    // Keyboard (A/D) - ProcessRotation
    void ProcessRotation()
    {
        // If A/D is pressed ApplyRotation of a value of ThrustRotation (A gets priority)
        if (Input.GetKey(KeyCode.A))
        {
            // See ApplyRotation Method
            ApplyRotation(ThrustRotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-ThrustRotation);
        }
    }

    // Method for Applying Rotation so ProcessRotation doesn't get too cluttered
    void ApplyRotation(float rotationDirection)
    {
        // FreezeRotation while Rotating and Rotate in rotationDirection unfreezeRotation after
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);
        rb.freezeRotation = false;
        
    }
}
