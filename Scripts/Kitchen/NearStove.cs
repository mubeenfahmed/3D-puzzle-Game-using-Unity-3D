using UnityEngine;
using System.Collections;

public class NearStove : MonoBehaviour {


	public GameObject cam1;
	public GameObject cam2;
	public AudioSource audioClue;
	public static bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	private bool keyPicked;
	private bool audioCluePlayed;

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
			_isplayerinzone = true;
			Debug.Log ("enter zone");	// log message
			if(audioCluePlayed == false) {//if audio clue not played
					audioClue.Play ();//play audio clue
					audioCluePlayed = true;//set audio clue played to true
			}
		}	
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)

			Debug.Log ("exit zone");	// log message
			cam2.SetActive (false);//area camera focus set to false
			cam1.SetActive (true);//main camera focus set to true
			_isplayerinzone = false;//area camera focus set to false
		}
	}

}
