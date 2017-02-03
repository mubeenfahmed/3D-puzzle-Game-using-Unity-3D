using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//the script is for camera anglr change once it enters tap collider zone
public class TapScript : MonoBehaviour
{
	public GameObject target;
	public GameObject cam1;
	public GameObject cam2;
	public GameObject tapSymbol;
	public GameObject tapKnob;
	public static bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	private bool audioCluePlayed = false;
	public AudioSource audioClueGuestBath;
	public static bool tapKnobPicked;

	void Start(){
		
		if (GameControl.control.guestBathroomPuzzle.TryGetValue(PuzzleConstants.TAP_KNOB_TAKEN, out tapKnobPicked)) {// check if the tap knob inside guestBathroomPuzzle  is already picked
			if (tapKnobPicked == true) { //if yes
				Destroy (tapKnob);//destroy tap knob
				audioCluePlayed = true;//set audio clue played to true
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		if (other.tag == "Player") {//if player in zone
			_isplayerinzone = true;//set player in zone to true
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
			Debug.Log ("enter tap zone");	// log message
			if (audioCluePlayed == false) { //if audio clue not played
				audioClueGuestBath.Play ();//play audio clue
				audioCluePlayed = true;//set audio clue played to true
			}
		}
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {
			_isplayerinzone = false;
			Debug.Log ("exit tap zone");	// log message
			cam2.SetActive (false);//area camera focus set to false
			cam1.SetActive (true);//main camera focus set to true
			if (TapCover.tapCoverLifted == true) { //if toilet cover lifted
				target.transform.Translate (0, 0, -0.20f);//place the toilet cover back to its intial position
				TapCover.tapCoverLifted = false;//set toilet cover lifted to false
			}
		}
	}
}
