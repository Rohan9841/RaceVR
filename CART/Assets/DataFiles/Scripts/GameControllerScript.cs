using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControllerScript : MonoBehaviour {

    //Stores the text that will be displayed during the play mode.
    public TextMesh infoText;

    //stores the speed text
    public TextMesh speedInfo;

    //Referecing the carScript from which we will take some values
    public CarScript player;

    //Keeps track of game time
    private float timer;

    //keeps track of lap
    private int kartLap;

    //keeps track of best time
    private float bestTime;

	// Use this for initialization
	void Start () {
        timer = 0f;
        bestTime = 999f;
        kartLap = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //if the cardboard button is clicked or mouse button is clicked
        if (Input.GetMouseButtonDown(0) || GvrPointerInputModule.Pointer.TriggerDown)
        {
            // menuScreen loaded
            SceneManager.LoadScene("MenuScene");
            Debug.Log("Menu Scene loaded");
        }

        //if the 3rd lap is completed
        if (kartLap == 4)
        {
            infoText.alignment = TextAlignment.Center;
            infoText.text = "                                                                           CONGRATULATIONS!\n                                                                          Your best time is: " + Mathf.Floor(bestTime) + "s.\n                                                                            Click the cardboard button to go to menu";
        }

        else
        {
            //increasing timer every second
            timer += Time.deltaTime;

            //if the getCurrentLap method returns value greater than our kartLap
            if (player.getCurrentLap > kartLap)
            {
                kartLap = player.getCurrentLap;

                if (timer < bestTime)
                {
                    //setting bestTime if the current timer is smaller than previous best Time.
                    bestTime = timer;
                }

                //resetting timer to zero after 1 lap
                timer = 0;
            }

            //The text that will be displayed
            infoText.text = "Time: " + Mathf.Floor(timer) + "s\nLap: " + kartLap;

            if (bestTime < 999f)
            {
                //adding bestTime to the text
                infoText.text += "\nBest Time: " + Mathf.Floor(bestTime) + "s";
            }
        }

        speedInfo.text = "Speed: " + Mathf.Floor(player.speed)+" mph";

    }

}
