using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenMasterBathroom : MonoBehaviour {

	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	private bool masterBathroomOpened;
	private bool masterBathroomKeyFound;
	private bool masterBathroomKeyActive;
	public GameObject masterBathroomKey;
	public AudioSource lockedDoor;
	public GameObject cam1;
	public GameObject cam2;
	public AudioSource audioDoorOpen;
	public AudioSource audioBathdoorClue;
	private bool audioCluePlayed;

	void Start ()
	{

		if (GameControl.control.hallwayDoorsUnlockPuzzle.TryGetValue (PuzzleConstants.MASTER_BATHROOM_OPENED, out masterBathroomOpened)) {// check if master bathroom is opened
			if (masterBathroomOpened == true) {
				masterBathroomKey.SetActive (true);//set key to active
				audioCluePlayed = true;//set audio clue played to true
			}
		}
	}

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// if player is inside collider "door_collider" then this bool is set true
			Debug.Log ("enter door zone");	// log message
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
		}
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "door_collider" then this bool is set false
			Debug.Log ("exit door zone");	// log message
			cam1.SetActive (true);//main camera focus set to true
			cam2.SetActive (false);//area camera focus set to false
		}
	}


	void Update () {
		if (_isplayerinzone && masterBathroomOpened == false) { // checking if the player is inside the collider "door_collider"

			if (GameControl.control.kitchenPuzzle.TryGetValue (PuzzleConstants.MASTER_BATHROOM_KEY_TAKEN, out masterBathroomKeyFound)) {// check if the bathroom key is found
				if (Input.GetKey (KeyCode.E)) {//if E is pressed
						masterBathroomKey.SetActive (true);//set key active
						masterBathroomKeyActive = true;//set key active to true
					foreach (Transform child in GameControl.control.inventoryPanel.transform) {//loop through inventory
						if (child.gameObject.tag == "MasterBathKey") {//if bathrooom key present
							Destroy (child.gameObject);//destroy the key
							}
						}
				}

				if (masterBathroomKeyActive == true && Input.GetKeyDown (KeyCode.Q)) { 	// checking if the user is pressing "e" on the keyboard
					Debug.Log ("door open");// log message
					audioDoorOpen.Play();//play door opening audio
					SceneManager.LoadScene ("LockedBathroom", LoadSceneMode.Single);//load bathroom
					GameControl.control.hallwayDoorsUnlockPuzzle.Add (PuzzleConstants.MASTER_BATHROOM_OPENED, true);// add the clue picked to the hallwayDoorsUnlockPuzzle dictionary
				}
			}else 
			{
				if (Input.GetKeyDown (KeyCode.Q)) 	// checking if the user is pressing "e" on the keyboard
				{
					Debug.Log ("door locked");// log message
					lockedDoor.Play ();		// play sound of door opening 
					if (audioCluePlayed == false) {//if audio clue not played
						audioBathdoorClue.Play ();// play audio clue
						audioCluePlayed = true;//set audio clue played to true
					}
				}
			}
		}
		else if(_isplayerinzone == true && masterBathroomOpened == true){//if player in zone and bathroom opened
			if (Input.GetKeyDown (KeyCode.Q)) 	// checking if the user is pressing "e" on the keyboard
			{
				audioDoorOpen.Play();//play door opening audio
				SceneManager.LoadScene ("LockedBathroom", LoadSceneMode.Single); //load bathroom scene
			}
		}
	}
}
