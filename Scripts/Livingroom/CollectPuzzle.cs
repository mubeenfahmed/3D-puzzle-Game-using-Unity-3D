using UnityEngine;
using System.Collections;

public class CollectPuzzle : MonoBehaviour
{

	public GameObject cam1;
	public GameObject cam2;
	private bool _isplayerinzone = false; // bool in this script to check if the player is in the collider zone
	public GameObject jigsawLivingroom;
	public GameObject bird;
	public AudioSource audioJigsawPicked;
	bool jigsawPicked;
	bool cluePicked = false;

	void Start(){

		if (GameControl.control.livingRoomPuzzle.TryGetValue(PuzzleConstants.LIVINGROOM_JIZSAW, out jigsawPicked)) {
			if (jigsawPicked == true) {
				jigsawLivingroom.SetActive (false);
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false);
			cam2.SetActive (true);
			_isplayerinzone = true;			// if player is inside collider "Light_switch_collider" then this bool is set true
			Debug.Log ("enter clock zone");	// log message
		}				
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit clock zone");	// log message
			cam2.SetActive (false);
			cam1.SetActive (true);
		}
	}

	public void Update ()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true && jigsawPicked == false && ClockPuzzle.jigsawDropped == true) {					// checking if the player is inside the collider "Light_switch_collider"
			if (Input.GetKeyDown ("e")) {		// checking if the user is pressing "e" on the keyboard

				audioJigsawPicked.Play ();
				Debug.Log ("Jigsaw Collected");
				jigsawLivingroom.SetActive (false);
				bird.SetActive (false);
				GameControl.control.livingRoomPuzzle.Add (PuzzleConstants.LIVINGROOM_JIZSAW, true);
				GameControl.control.mainDoorPuzzle.Add (PuzzleConstants.LIVINGROOM_JIZSAW, true);
				GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_CLOCK_JIGSAW]);
				GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform);
			}
		}
	}
}
