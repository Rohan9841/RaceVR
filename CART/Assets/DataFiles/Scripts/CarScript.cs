using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

    //Time until boost
    public float speedTime = 3f;

    //if the checkpoints are triggered
    public bool triggered = false;

    //Sound variables
    public AudioSource aSource;
    public AudioClip aClip;

    //speed of the kart
    public float speed = 0f;

    //top speed of the car
    public float topSpeed = 100f;

    //the rate of acceleration
    public float acceleration = 4f;

    //check if the car is touching the rock
    private bool collided;

    //moving direction of the camera
    private Vector3 movingDirection;

    //Here we will put camera in start method
    public GameObject person;

    //keeps track of current check point
    private int currentCheckPoint;

    //keeps track of current lap
    private int lap;

    // Use this for initialization
    void Start () {
        //setting person to camera
        person = GameObject.FindGameObjectWithTag("MainCamera");
        currentCheckPoint = -1;
        lap = 1;

        aSource.PlayOneShot(aClip);
    }
	
	// Update is called once per frame
	void Update () {

        if(triggered)
        {
            topSpeed = 150f;
            speed = 150f;

            speedTime -= Time.deltaTime;

            if(speedTime <= 0)
            {
                triggered = false;
                topSpeed = 100f;
                speedTime = 3f;
            }
        }
        //constraining the transform.forward method to x and z direction
        movingDirection = new Vector3(
            person.transform.forward.x,
            0,
            person.transform.forward.z
            );

        //if the car is constantly touching the rock
        if (collided == true)
        {
            speed -= acceleration* 15 * Time.deltaTime;
        }
        else
        {
            //accelerating each second
            speed += acceleration * Time.deltaTime;
        }

        //if speed rises above 100
        if (speed > topSpeed)
        {
            speed = topSpeed;
        }

        if(speed < 0)
        {
            if(lap == 4)
            {
                speed = 0f;
            }
            else
            { 
                speed = 0.9f;
            }
        }

        //finding camera and moving forward in the direction the camera is facing. Normalizing the movingDirection so that the vector is 1
        //and doesn't behave irrationally.
        transform.position += movingDirection.normalized * speed * Time.deltaTime;
        
    }


    //We set the box collider of the checkpoint to trigger so we use onTriggerEnter method
    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.transform.tag == "CheckPoint")
            {
                //converting string value to integer
                int checkpoint  = int.Parse(other.transform.name);

                //if we pass throught a checkpoint whose number is greater than the currentCheckPoint
                if(checkpoint == currentCheckPoint+1)
                {
                    currentCheckPoint += 1;
                }

                //increase topspeed
                if(checkpoint == 1 || checkpoint == 2)
                {
                    triggered = true;
                    Debug.Log("Speed increased");
                }

                //after making through one lap
                if(checkpoint == 0 && currentCheckPoint == 2)
                {
                    //if 3rd lap is over, the speed decreased to '0'
                    if(lap == 3)
                    {
                        collided = true;
                    }

                    //resetting current check point to zero
                    currentCheckPoint = 0;
                    lap += 1;
                }
            }
        } 
    }

    //method that returns the current lap. This will be used by GameControllerScript.
    public int getCurrentLap
    {
        get 
        {
            return lap;
        }

    }

    //if the car collides with the rock
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Rock")
        {
            collided = true;
        }
    }

    //if the collision breaks
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Rock")
        {
            collided = false;
        }
    }

}
