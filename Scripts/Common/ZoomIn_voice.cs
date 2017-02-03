using UnityEngine;
using System.Collections;

public class ZoomIn_voice : MonoBehaviour {


	public GameObject cam1;
	public GameObject cam2;
	public AudioSource audioClue;
	public static bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone


	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false);
			cam2.SetActive (true);
			_isplayerinzone = true;
			Debug.Log ("enter zone");	// log message
			audioClue.Play ();
		}				

	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)

			Debug.Log ("exit zone");	// log message
			cam2.SetActive (false);
			cam1.SetActive (true);
			_isplayerinzone = false;
		}
	}

}
