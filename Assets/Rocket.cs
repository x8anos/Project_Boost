﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 50f;



    Rigidbody rigidBody;
    AudioSource audioSource;
    
   

    // Use this for initialization
    void Start () {

     

        rigidBody=GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();    

    }

    // Update is called once per frame
    void Update () {
        Thrust();
        Rotate();		
	}

    void OnCollisionEnter(Collision collision)
    {
       switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Fuel":
                print("Refill");
                break;
            default:
                print("HIT!!!!!!!!!!!!!");
                break;

        }
    }

    private void Thrust()
    {
       
        if (Input.GetKey(KeyCode.Space))
        {
            //print("Thrusting!");
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual control of physics

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotate Left");
            transform.Rotate(Vector3.forward*rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //print("Rotate Right");
            transform.Rotate(-Vector3.forward*rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // return control of physics

    }

}
