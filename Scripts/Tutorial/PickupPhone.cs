using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PickupPhone : MonoBehaviour {

	private bool _isplayerinzone = false;

	// Update is called once per frame

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
		{	
			
			if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
				_isplayerinzone = true;			// if player is inside collider, then this bool is set true
				Debug.Log ("enter phone zone");	// log message
			}				

		}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider , then this bool is set false
			Debug.Log ("exit phone zone");	// log message

		}
	}


	void Update () {
			
		if (_isplayerinzone == true) {					// checking if the player is inside the collider
			if (Input.GetKeyDown (KeyCode.E)) {			// checking if player press "e" 
				SceneManager.LoadScene ("cutScene", LoadSceneMode.Single); // load next scene
			}	
		}
	}
}
