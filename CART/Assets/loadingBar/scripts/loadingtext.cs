using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for image component
using UnityEngine.SceneManagement; // for scene change

public class loadingtext : MonoBehaviour {

    //stores canvas
    private RectTransform rectComponent;

    //stores image 
    private Image imageComp;
    
    //stores the percentage
    public Text text;

    //stores some other texts
    public Text textNormal;

    //declaring async operation
    AsyncOperation async;
    // Use this for initialization
    void Start () {

        //getting canvas and storing in rectComponent
        rectComponent = GetComponent<RectTransform>();

        //getting image inside canvas and storing in imageComp
        imageComp = rectComponent.GetComponent<Image>();

        //setting fill to zero 
        imageComp.fillAmount = 0.0f;

        //starting the loadingScreen() 
        StartCoroutine(LoadingScreen());

        Debug.Log("Game Started");
    }

    IEnumerator LoadingScreen()
    {
        //loading flatRoadScene in the background   
        async = SceneManager.LoadSceneAsync(1);

        //flatRoadScene is deactivated
        async.allowSceneActivation = false;

        int a = 0;

        //until the flatRoadScene is completed loaded
        while (async.isDone == false)
        {
            //async.progress gives float value and the imageComp.fillAmount also gives the float value
            imageComp.fillAmount = async.progress;

            //this is the percentage that will be shown
            a = (int)(async.progress * 100);

            //actual text for showing percentage
            text.text = a + "%";

            if (a > 0 && a <= 33)
            {
                textNormal.text = "Loading...";
            }
            else if (a > 33 && a <= 67)
            {
                textNormal.text = "Downloading...";
            }
            else if (a > 67 && a <= 100)
            {
                textNormal.text = "Please wait...";
            }

            //if the progress is complete
            if (async.progress == 0.9f)
            {
                //fill the image completely
                imageComp.fillAmount = 1f;

                //show 100%
                text.text = 100 + "%";

                //activate the new scene
                async.allowSceneActivation = true;
            }

            //yield from returning null
            yield return null;
        }
    }
}
