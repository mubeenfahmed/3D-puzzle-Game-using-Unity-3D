using UnityEngine;
using System.Collections;

public class JigsawClue : MonoBehaviour {

	public AudioSource audioMainDoorClue2;
	private bool _isplayerinzone;
	public GameObject cam1;
	public GameObject cam2;
	private bool cribFixed;
	private bool audioCluePlayed;

	void Start(){

		if (GameControl.control.nurseryPuzzle.TryGetValue (PuzzleConstants.CRIB_FIXED, out cribFixed)) { // check if crib is fixed
			if (cribFixed == true) { //if cribFixed is true
				if (PuzzleConstants.MAIN_DOOR_CLUE == true) { //if main door clue is true
					audioCluePlayed = true; //play clue audio
				}
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			_isplayerinzone = true;			// set player in zone to true
			Debug.Log ("enter jigsaw zone");	// log message
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
			if (cribFixed == true && FixJigsaw.playAudioClue == true && audioCluePlayed == false) { //if crib is fixed, clue one is played and clue2 is not played
				audioMainDoorClue2.Play (); //play audio clue2
				audioCluePlayed = true; //set audio clue2 played to true
			}
		}		
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// set player in zone to false
			Debug.Log ("exit jigsaw zone");	// log message
			cam2.SetActive (false); //area camera focus set to false
			cam1.SetActive (true); //main camera focus set to true
		}
	}
}
