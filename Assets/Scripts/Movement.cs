using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotatePower = 250f;
    [SerializeField] float thrustPower = 1000f;
    [SerializeField] AudioClip thruster;

    [SerializeField] ParticleSystem thrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) {
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(thruster);
            };
            if (!thrusterParticles.isEmitting) {
                thrusterParticles.Play();
            }
            float appliedThrust = Time.deltaTime * thrustPower;
            rb.AddRelativeForce(Vector3.up * appliedThrust);
        } else {
            thrusterParticles.Stop();
            audioSource.Stop();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotatePower);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotatePower);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
