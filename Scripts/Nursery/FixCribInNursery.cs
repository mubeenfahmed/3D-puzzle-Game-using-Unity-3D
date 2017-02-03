using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FixCribInNursery : MonoBehaviour
{

	public GameObject cam1;
	public GameObject cam2;
	public GameObject cribLeftOnFloor;
	public GameObject cribFrontOnFloor;
	public GameObject cribFront;
	public GameObject cribLeft;
	public AudioSource audioCribFixing;
	public AudioSource audioCribFixed;
	private bool _isplayerinzone = false;
	private bool cribFixed;
	private bool audioClue;
	private bool cribFixedAudioPlayed;


	void Start ()
	{
		
		if (GameControl.control.nurseryPuzzle.TryGetValue (PuzzleConstants.CRIB_FIXED, out cribFixed)) {// check if crib fixed
			if (cribFixed == true) { //if true
				Destroy (cribLeftOnFloor); //destroy crib on floor
				Destroy (cribFrontOnFloor);//destroy crib on floor
				cribLeft.SetActive (true);//set fixed crib to active
				cribFront.SetActive (true);//set fixed crib to active
				audioClue = true;//set audio clue played to active
				cribFixedAudioPlayed = true; //set crib fixed audio played to true
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
			Debug.Log ("enter crib zone");	// log message
			_isplayerinzone = true;//set player in zone to true
		}				

	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)

			Debug.Log ("exit crib zone");	// log message
			cam2.SetActive (false);//area camera focus set to false
			cam1.SetActive (true);//main camera focus set to true
			_isplayerinzone = false;//set player in zone to false
		}
	}

	public void Update ()
	{
		if (_isplayerinzone == true) {//if player in zone
			if (PuzzleConstants.NURSERY_AUDIO_CLUE_PLAYED == false) {//if audio clue not played
				audioCribFixing.Play ();//play audio clu
				PuzzleConstants.NURSERY_AUDIO_CLUE_PLAYED = true;//set audio clue played to true
				audioClue = true;//set audioclue to true
			}
			if (GameControl.control.nurseryPuzzle.Count == PuzzleConstants.MAX_CLUE_NURSERY_SCENE) {//if all nails and hammer picked

				if (Input.GetKey (KeyCode.E) && cribFixed == false) {//if E is pressed and crib is not fixed
					Destroy (cribLeftOnFloor);//destroy floor crib piece
					Destroy (cribFrontOnFloor);//destroy floor crib piece
					cribLeft.SetActive (true);//set crib piece to active
					cribFront.SetActive (true);//set crib piece to active
					GameControl.control.nurseryPuzzle.Add (PuzzleConstants.CRIB_FIXED, true);//add clue to nurseryPuzzle dictionary
					foreach (Transform child in GameControl.control.inventoryPanel.transform) {//loop through inventory
						if (child.gameObject.tag == "Nails") {//if nail icon found
							Destroy (child.gameObject);//destroy nails
						}
						if (child.gameObject.tag == "Hammer") {//if hammer icon found
							Destroy (child.gameObject);//destroy hammer
						}
					}
					cribFixed = true;//set crib fixed to true
					if (cribFixedAudioPlayed == false && cribFixed == true) {//if crib fixed audio played is false and crib fixed is true
						audioCribFixed.Play ();//play crib fixed audio
						cribFixedAudioPlayed = true;//set crib fixed audio played to true
					}
				}
			}
		}
	}
}