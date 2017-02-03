using UnityEngine;
using System.Collections;

public class JigsawInBasement : MonoBehaviour {

	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	private bool carJigsawCollected;
	private bool carTrunkOpened;
	private bool carCluePlayed;
	private bool masterBathroomKeyActive;
	public GameObject greenJigsaw;
	public GameObject carTrunkClosed;
	public GameObject carNoClosed;
	public GameObject carTrunkOpen;
	public AudioSource audioFoundJigsaw;
	public AudioSource audioCarClue;
	public GameObject light;
	public GameObject cam1;
	public GameObject cam2;

	void Start ()
	{

		if (GameControl.control.mainDoorPuzzle.TryGetValue (PuzzleConstants.CAR_JIZSAW, out carJigsawCollected)) { // check if the jigsaw inside car is already picked
			if (carJigsawCollected == true) { //if jigsaw already picked
				Destroy(greenJigsaw); //if yes destroy the jigsaw gameobject
				carTrunkClosed.SetActive (false); //set carTrunkClosed to inactive
				carNoClosed.SetActive (false); //set carNoClosed to inactive
				carTrunkOpen.SetActive (true); //set carTrunkOpen to active
				carCluePlayed = true;
			}
		}
	}

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// player is inside collider 
			Debug.Log ("enter car zone");	// log message
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
		}
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider then this bool is set false
			Debug.Log ("exit car zone");	// log message
			cam1.SetActive (true); //main camera focus set to true
			cam2.SetActive (false); //area camera focus set to false
		}
	}


	void Update () {
		
		if (_isplayerinzone && carJigsawCollected == false) { 	// checking if the player is inside the collider and jigsaw is picked
				
			if (carCluePlayed == false && carJigsawCollected == false) {
				audioCarClue.Play (); //play clue audio
				carCluePlayed = true; //set clue audio played to true
			}
			if (Input.GetKey (KeyCode.Q)) { // if Q is pressed
			 	carTrunkOpened = true; //set car trunk opened to true
				carTrunkClosed.SetActive (false); //set gameobject closed trunk to false
				carNoClosed.SetActive (false); //set carNoClosed to inactive
				light.SetActive (true);
				carTrunkOpen.SetActive (true); //set gameobject closed trunk to true
			}
			if (Input.GetKeyDown ("e") && carTrunkOpened == true) { 	// checking if the user is pressing "e" on the keyboard
					
				Destroy (greenJigsaw);	//destroy gameobject jigsaw
				audioFoundJigsaw.Play (); //play audio of clue found
				GameControl.control.mainDoorPuzzle.Add (PuzzleConstants.CAR_JIZSAW, true); // add the clue picked to the mainDoorPuzzle dictionary
				GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_CAR_JIGSAW]); //instantiate the jigsaw icon to be displayed in inventory
				GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the jigsaw in inventory panel
			}
		}
	}
}
