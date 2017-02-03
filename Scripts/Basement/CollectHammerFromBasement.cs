using UnityEngine;
using System.Collections;

public class CollectHammerFromBasement : MonoBehaviour
{
	public GameObject cam1;
	public GameObject cam2;
	public GameObject hammerTool;
	public AudioSource audio_findHammer;
	public AudioSource audioToolsClue;
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	private bool _hammerFound = false;
	private bool hammerPicked;
	private bool audioToolsCluePlayed;

	void Start(){
		if (GameControl.control.nurseryPuzzle.TryGetValue(PuzzleConstants.HAMMER_IN_BASEMENT, out hammerPicked)) { // check if the hammer is already picked
			if (hammerPicked == true) { //if hammer already picked
				Destroy (hammerTool); //if hammer already picked, destroy the hammer game object
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false); // main camera focus set false
			cam2.SetActive (true); // area area focus set to true
			_isplayerinzone = true; // set player in zone to true
			Debug.Log ("enter tools zone");	// log message
			if (hammerPicked == false && audioToolsCluePlayed == false) { // if hammer not picked and clue audio not played
				audioToolsClue.Play(); //play the clue audio
				audioToolsCluePlayed = true;  //set clue audio played to true
			}
		}			
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)

			Debug.Log ("exit tools zone");	// log message
			cam2.SetActive (false); // area area focus set to false
			cam1.SetActive (true);    // main camera focus set true
			_isplayerinzone = false;
		}
	}

	public void Update ()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true && hammerPicked == false) {					// checking if the player is inside the collider "Light_switch_collider"
			
			if (Input.GetKeyDown(KeyCode.E)) {		// checking if the user is pressing "e" on the keyboard
				Debug.Log ("_hammerFound ="+_hammerFound);
				if (_hammerFound == false) { // if hammer is not found
					  	hammerPicked = true; // set hammerpicked to true
						_hammerFound = true; // set hammerFound to true
						audio_findHammer.Play ();	// play the hammer picked audio
						Debug.Log ("Hammer found"); // log message
						GameControl.control.nurseryPuzzle.Add(PuzzleConstants.HAMMER_IN_BASEMENT,true); // add the clue picked to the nurseryPuzzle dictionary
						Destroy (hammerTool); // Destroy the gameobject hammer
						GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_HAMMER]); // instantiate the hammer icon to be displayed in inventory
						GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the hammer in inventory panel
				}
			}
		}
	}
}
