using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;
    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;

    // Use this for initialization
    void Start () {

        rigidBody=GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play when needed
       // m_Play = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
		
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //print("Thrusting!");
            rigidBody.AddRelativeForce(Vector3.up);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            }
        }
        else
        {
            audioSource.Stop();
        }

            //Check to see if you just set the toggle to positive
            /* if (m_Play == true && m_ToggleChange == true)
             {
                 audioSourse.Play();
                 //Ensure audio doesn’t play more than once
                 m_ToggleChange = false;
             }
             //Check if you just set the toggle to false
             if (m_Play == false && m_ToggleChange == true)
             {
                 //Stop the audio
                 audioSourse.Stop();
                 //Ensure audio doesn’t play more than once
                 m_ToggleChange = false;
             }*/
        


        if (Input.GetKey(KeyCode.A))
        {
            //print("Rotate Left");
            transform.Rotate(Vector3.forward);
        }else if (Input.GetKey(KeyCode.D))
        {
            //print("Rotate Right");
            transform.Rotate(-Vector3.forward);
        }
    }

  /*  void OnGUI()
    {
        //Switch this toggle to activate and deactivate the parent GameObject
        m_Play = GUI.Toggle(new Rect(10, 10, 100, 30), m_Play, "Play Music");

        //Detect if there is a change with the toggle
        if (GUI.changed)
        {
            //Change to true to show that there was just a change in the toggle state
            m_ToggleChange = true;
        }
    }*/
}
