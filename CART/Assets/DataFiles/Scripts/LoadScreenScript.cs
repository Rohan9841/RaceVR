using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Slider
using UnityEngine.SceneManagement; //change scene

public class LoadScreenScript : MonoBehaviour {

    //we put canvas here
    public GameObject loadingScreenObj;

    //slider goes here
    public Slider slider;

    //for the loading of scenes simultaneously
    AsyncOperation async;
     
    public void Start()
    {
        //needed for ieNumnerator
        StartCoroutine(LoadingScreen());
        Debug.Log("Game Started");
    }
    
    IEnumerator LoadingScreen()
    {
        //setting the screenActive
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while(async.isDone == false)
        {
            slider.value = async.progress;
            if(async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
                
            }

            yield return null;
        }
    }
}
