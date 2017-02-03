using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenLivingroom : MonoBehaviour {

	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	public AudioSource audioDoorOpen;

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// if player is inside collider "door_collider" then this bool is set true
			Debug.Log ("enter door zone");	// log message
		}
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "door_collider" then this bool is set false
			Debug.Log ("exit door zone");	// log message
		}
	}


	void Update () {
		if (_isplayerinzone) { 				// checking if the player is inside the collider "door_collider"
			if (Input.GetKeyDown (KeyCode.Q)) { 	// checking if the user is pressing "e" on the keyboard
				Debug.Log ("door open");// log message
				audioDoorOpen.Play();
				SceneManager.LoadScene ("LivingRoom", LoadSceneMode.Single);
			}
		}
	}
}
