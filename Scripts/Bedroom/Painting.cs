using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Painting : MonoBehaviour {

	public GameObject PaintingDoorClosed;
	public GameObject PaintingDoorOpened;
	public GameObject PaintingDoor;
	public AudioSource audioPaintingMoved;
	public AudioSource puzzleFound;
	public AudioSource audioPainting;
	private bool paintingOpen = false;
	private bool safeDoorOpen = false;
	public GameObject cam1;
	public GameObject cam2;
	public GameObject cam3;
	public GameObject canvas;
	public GameObject safe;
	private bool _isplayerinzone = false;
	public Text safeText;
	public GameObject JigsawPiece;
	private bool masterBedroomJigsawFound;
	bool audioCluePlayed = false;
			
	void Start ()
	{
		if (GameControl.control.mainDoorPuzzle.TryGetValue(PuzzleConstants.MASTER_BEDROOM_JIZSAW, out masterBedroomJigsawFound)) { // check if the jigsaw inside mainDoorPuzzle  is already picked
			if (masterBedroomJigsawFound == true) {
				PaintingDoorClosed.SetActive (false); //paintingClosed set to false
				PaintingDoorOpened.SetActive (true); //paintingOpen set to true
				Destroy (JigsawPiece); //destroy jigsaw gameObject
				safe.SetActive (false); //safe set to false
				canvas.SetActive (false); //set canvas to false
				PaintingDoor.SetActive (false); //set painting door to false
				safeDoorOpen = true; //set safe door open to true
				audioCluePlayed = true; //audio clue played to true
			}
		}
	}

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// set player in zone to true
			Debug.Log ("enter painting zone");	// log message
			cam1.SetActive(false); //main camera focus set to false
			cam2.SetActive(true); //area camera focus set to true
		}				
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit painting zone");	// log message
			cam1.SetActive(true); //main camera focus set to true
			cam2.SetActive(false); //area camera focus set to false
			cam3.SetActive (false); //area camera focus set to false
			canvas.SetActive(false); //set canvas to false		
		}
	}

	public void Update()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true) {					// checking if the player is inside the collider "Light_switch_collider"
			if (audioCluePlayed == false) { //if audio clue played is false
				audioPainting.Play (); //play audio clue
				audioCluePlayed = true; //set audio clue played tp true
			}
			if (Input.GetKey (KeyCode.Q)) { //if Q is pressed
				
				if (paintingOpen == false) { //if painting opened is false
					paintingOpen = true; //set painting opened to true
					audioPaintingMoved.Play ();		// play audio of paiting safe opening
					Debug.Log ("paintingOpen =" + paintingOpen); //log message
					PaintingDoorClosed.SetActive (false); //set closed painting to false
					PaintingDoorOpened.SetActive (true); //set open paiting to true
					cam3.SetActive (true); //area camera focus set to true
					cam2.SetActive (false); //area camera focus set to false
					canvas.SetActive (true); //set canvas to true
				}
			}
			if (InputPasscode.passcodeCorrect == true) { //if passcode entered is correct
				safe.SetActive (false); //set safe to false
				canvas.SetActive (false); //set canvas to false
				PaintingDoor.SetActive (false); //set painting door to false
				safeDoorOpen = true; //set safe door open to true
				if (Input.GetKeyDown (KeyCode.E)) {	//if E is pressed
					if (safeDoorOpen == true) { //if safe door is open
						puzzleFound.Play (); //play clue found audio
						Debug.Log ("puzzle Collected"); //log message
						Destroy (JigsawPiece); //destroy jigsaw
						GameControl.control.mainDoorPuzzle.Add (PuzzleConstants.MASTER_BEDROOM_JIZSAW, true); // add the clue picked to the mainDoorPuzzle dictionary
						GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_PAINTING_JIGSAW]); //instantiate the jigsaw icon to be displayed in inventory
						GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the jigsaw in inventory panel
						canvas.SetActive (false);//set canvas to false
					}
				}
			}
		}
	}
}
