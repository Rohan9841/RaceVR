using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    //when the start button is clicked load another scene
    public void StartGame()
    {
        //loading load scene
        SceneManager.LoadScene("scene0");
        Debug.Log("Loading Scene");
    }


    //when exit button is clicked, exit the application
    public void endGame()
    {
        Application.Quit();
        Debug.Log("Application quit");
    }
}
