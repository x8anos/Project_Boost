using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip hitObstacle;
    [SerializeField] AudioClip success;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending }
    State state = State.Alive;   
   

    // Use this for initialization
    void Start () {
           
        rigidBody=GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();    

    }

    // Update is called once per frame
    void Update () {

        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotationInput();
        }  
        
	}

    void OnCollisionEnter(Collision collision)
    {

        if (state!=State.Alive) { return;  } // ignore collisions when dead. In order not to collide more than once to stop....

       switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(hitObstacle);
        Invoke("LoadFirstScene", 2f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", 1f);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
        
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
       
    }

    private void RespondToThrustInput()
    {
       
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
    }

    private void RespondToRotationInput()
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
