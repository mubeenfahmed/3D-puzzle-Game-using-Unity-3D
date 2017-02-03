using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LeaveGuestBathroom : MonoBehaviour {

	public static bool leaveGuestBathroom = false;
	public AudioSource door_sound;
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// set player in zone to true
			Debug.Log ("enter guest bathroom door zone");	// log message
		}				
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// set player in zone to false
			Debug.Log ("exit guest bathroom door zone");	// log message
		}
	}


	// Update is called once per frame
	void Update () {
		if (_isplayerinzone) { 				// checking if the player is inside the collider "door_collider"
			if (Input.GetKeyDown (KeyCode.Q)) { 	// checking if the user is pressing "e" on the keyboard
				Debug.Log ("guest bathroom door  open");// log message
				door_sound.Play ();		// play sound of door opening 
				leaveGuestBathroom = true; //set leave guest bathroom to true
				SceneManager.LoadScene ("Hallway", LoadSceneMode.Single);//load scene Hallway
			}
		}
	}
}
