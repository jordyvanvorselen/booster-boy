using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime, ForceMode.Force);

        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void StopThrusting()
    {
        audio.Stop();
        mainBooster.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
