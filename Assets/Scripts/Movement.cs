using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float ThrustUp = 1000f;
    [SerializeField] float ThrustRotation = 25f;

    [SerializeField] AudioClip ThrustSound;

    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(ThrustSound);
            }
            if (!engineParticles.isPlaying)
            {
                engineParticles.Play();
            }
        }
        // If Space !Pressed then stop thrustSound
        else
        {
            audioSource.Stop();
            engineParticles.Stop();
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
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-ThrustRotation);
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
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
