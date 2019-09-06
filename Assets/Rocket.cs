using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    bool audioIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void PlayAudio(bool togglePlaying)
    {
        if (audioIsPlaying == false && togglePlaying == true)
        {
            audioSource.Play();
            audioIsPlaying = true;
        }
    }

    void StopAudio()
    {
        audioSource.Stop();
        audioIsPlaying = false;
    }

    void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            PlayAudio(true);
        }
        else
        {
            StopAudio();
        }
    }

    void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.freezeRotation = true; //Take manual control of rotation
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //allow physics to control rotation now
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Do nothing
                print("Not dead");
                break;
            default:
                //We die
                print("REEEEEEE WE CRASHED");
                break;
        }
    }
}
