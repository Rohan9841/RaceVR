using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModelScript : MonoBehaviour {

    private GameObject person;

	// Use this for initialization
	void Start () {

        //finding camera
        person = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

        //car rotates only on y-axis
        transform.eulerAngles = new Vector3(
            0,
            person.transform.eulerAngles.y,
            0);
	}
}
