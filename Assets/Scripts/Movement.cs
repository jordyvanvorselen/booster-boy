using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime, ForceMode.Force);

            if (!audio.isPlaying)
            {
                audio.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audio.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
