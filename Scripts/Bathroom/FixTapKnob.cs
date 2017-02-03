using UnityEngine;
using System.Collections;

public class FixTapKnob : MonoBehaviour
{

	public GameObject tapKnob;
	private bool _isplayerinzone = false;
	public static bool tapKnobFixed = false;
	public AudioSource audioKnobFixed;
	private bool cluePicked;
	private bool masterBedroomSolved;
	private bool guestBedroomSolved;
	public GameObject cam1;
	public GameObject cam2;
	public GameObject jigsaw;

	void Start ()
	{
		if (GameControl.control.masterBathroomPuzzle.TryGetValue(PuzzleConstants.TAP_KNOB_FIXED, out masterBedroomSolved)) {
			if (masterBedroomSolved == true) {
				Debug.Log ("Inside start in master bathrrom");
				tapKnobFixed = true;
				tapKnob.SetActive (true);
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		if (other.tag == "Player"){	
			_isplayerinzone = true; // if player is inside collider
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
			Debug.Log ("enter basin zone");	// log message
		}
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {	
			_isplayerinzone = false;// if player is outside collider
			Debug.Log ("exit basin zone");	// log message
			cam2.SetActive (false); //area camera focus set to false
			cam1.SetActive (true); //main camera focus set to true
		}
	}

	// Update is called once per frame
	void Update ()
	{

		if (_isplayerinzone == true) { // check if the player is inside the collider
			if (GameControl.control.guestBathroomPuzzle.TryGetValue (PuzzleConstants.TAP_KNOB_TAKEN, out guestBedroomSolved)) { // check if the tap knob is taken from guest bathroom
				if (Input.GetKeyDown(KeyCode.Q) && tapKnobFixed == false) {	//if Q is pressed and tap knob is not fixed			
					tapKnob.SetActive (true); //set active tap knob gameobject
					Debug.Log ("Tap knob fixed"); //log message
					audioKnobFixed.Play();
					tapKnobFixed = true; //set tap knob fixed to true
					GameControl.control.masterBathroomPuzzle.Add (PuzzleConstants.TAP_KNOB_FIXED, true); //add the clue picked to the masterBathroomPuzzle dictionary
					foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through the items in inventory panel
						if (child.gameObject.tag == "TapKnob") { //if tap knob icon exists
							Destroy (child.gameObject); //destroy the tap knob icon
						}
					}
				}
			}	
		}
	}
}
