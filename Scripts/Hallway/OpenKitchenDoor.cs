using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenKitchenDoor : MonoBehaviour
{

	public AudioSource door_sound;
	public GameObject cam1;
	public GameObject cam2;
	public GameObject canvas;
	public GameObject colorOne;
	public GameObject colorTwo;
	public GameObject colorThree;
	public GameObject colorFour;
	public GameObject colorFive;
	private bool _isplayerinzone = false;
	private bool audioCluePlayed = false;
	public AudioSource audioClueKitchen;
	private bool kitchenDoorOpened;
	public AudioSource lockedDoor;
	public AudioSource audioDoorOpen;

	void Start ()
	{
		if (GameControl.control.hallwayDoorsUnlockPuzzle.TryGetValue (PuzzleConstants.KITCHEN_DOOR_PUZZLE, out kitchenDoorOpened)) {// check if kitchen door is opened
			if (kitchenDoorOpened == true) {//if true
				Debug.Log ("Inside start in kitchen");//log message
				audioCluePlayed = true;//set audio clue played to true
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			_isplayerinzone = true;			// if player is inside collider "door_collider" then this bool is set true
			Debug.Log ("enter door zone");	// log message
		}
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider "door_collider" then this bool is set false
			Debug.Log ("exit door zone");	// log message
			cam1.SetActive (true);//main camera focus set to true
			cam2.SetActive (false);//area camera focus set to false
			canvas.SetActive (false);//set canvas to false
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (_isplayerinzone == true && kitchenDoorOpened == false) { 				// checking if the player is inside the collider "door_collider"
			if (Input.GetKeyDown ("q")) { 	// checking if the user is pressing "e" on the keyboard
				cam2.SetActive (true);//area camera focus set to true
				cam1.SetActive (false);//main camera focus set to false
				canvas.SetActive (true);//set canvas active
			}

			if (Input.GetKeyDown ("e")) { 	// checking if the user is pressing "e" on the keyboard				
				if (kitchenDoorOpened == false) {	//if kitchen door is not open				
						if (colorOne.activeSelf && colorTwo.activeSelf && colorThree.activeSelf && colorFour.activeSelf && colorFive.activeSelf) {//check color combination
							Debug.Log ("kitchen door open");
							cam1.SetActive (true); //main camera focus set to true
							cam2.SetActive (false); //area camera focus set to false
							canvas.SetActive (false);//set canvas to false
							door_sound.Play ();//play door opening audio
							kitchenDoorOpened = true; //kitchen door opened to true
							GameControl.control.hallwayDoorsUnlockPuzzle.Add (PuzzleConstants.KITCHEN_DOOR_PUZZLE, true);// add the clue picked to the hallwayDoorsUnlockPuzzle dictionary
							audioDoorOpen.Play ();//play door open audio
							SceneManager.LoadScene ("KitchenRoom", LoadSceneMode.Single);//load kitchen scene

					} else {

						if (Input.GetKeyDown ("q")) { 	// checking if the user is pressing "e" on the keyboard
							Debug.Log ("door locked");// log message
							lockedDoor.Play ();		// play sound of door opening 	
							if (audioCluePlayed == false) {//if audio clue not played
								audioClueKitchen.Play ();//play audio clue
								audioCluePlayed = true;//set audio clue played to true
							}
						}
					}
				} 
			}
			else {

				if (Input.GetKeyDown ("q")) { 	// checking if the user is pressing "e" on the keyboard
					Debug.Log ("door locked");// log message
					lockedDoor.Play ();		// play sound of door opening 	
					if (audioCluePlayed == false) {//if audio clue not played
						audioClueKitchen.Play ();//play audio clue
						audioCluePlayed = true;//set audio clue played to true
					}
				}
			}
		} else if (kitchenDoorOpened == true && _isplayerinzone == true) {//if kitchen door is opened and player is in zone

			if (Input.GetKeyDown ("e")) { //if E is pressed
				audioDoorOpen.Play ();//play door opening audio
				SceneManager.LoadScene ("KitchenRoom", LoadSceneMode.Single);//load kitchen scene
			}
		}
	}
}
