using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenMasterBedroom : MonoBehaviour
{

	private bool _isplayerinzone = false;
	private bool masterBedroomOpened = false;
	private bool masterBedroomKeyFound = false;
	public GameObject masterBedroomKey;
	public GameObject cam1;
	public GameObject cam2;
	public AudioSource audiolockedDoor;
	public AudioSource audioMasterBedClue;
	public AudioSource openDoor;
	public bool masterBedroomKeyActive;
	private bool audioCluePlayed;

	void Start ()
	{

		if (GameControl.control.hallwayDoorsUnlockPuzzle.TryGetValue (PuzzleConstants.MASTER_BEDROOM_OPENED, out masterBedroomOpened)) {// check if master bedroom is opened
			if (masterBedroomOpened == true) {
				masterBedroomKey.SetActive (true);//set key to active
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
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
		}
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider "door_collider" then this bool is set false
			Debug.Log ("exit door zone");	// log message
			cam1.SetActive (true);//main camera focus set to true
			cam2.SetActive (false);//area camera focus set to false
		}
	}

	void Update ()
	{
		if (_isplayerinzone == true && masterBedroomOpened == false) { 	// checking if the player is inside the collider "door_collider"

			if (GameControl.control.hallwayDoorsUnlockPuzzle.TryGetValue (PuzzleConstants.MASTER_BEDROOM_KEY, out masterBedroomKeyFound)) {	// check if the bedroom key is found
				if (masterBedroomOpened == false) {	//if bedroom not open			
					if (Input.GetKey (KeyCode.E)) {//if E is pressed
						masterBedroomKey.SetActive (true);//set key active
						masterBedroomKeyActive = true;//set keyactive to true
						foreach (Transform child in GameControl.control.inventoryPanel.transform) {//loop through inventory
							if (child.gameObject.tag == "MasterBedKey") {//if bedroom key present
								Destroy (child.gameObject);//destroy the key
							}
						}
					}
					if (Input.GetKeyDown (KeyCode.Q) && masterBedroomKeyActive == true) { 	// checking if the user is pressing "e" on the keyboard
						Debug.Log ("door open");// log message
						SceneManager.LoadScene ("Bedroom", LoadSceneMode.Single);//load bedroom scene
						GameControl.control.hallwayDoorsUnlockPuzzle.Add (PuzzleConstants.MASTER_BEDROOM_OPENED, true);// add the clue picked to the hallwayDoorsUnlockPuzzle dictionary
					}
				} 
			} 
			else 
			{
				if (Input.GetKeyDown (KeyCode.Q)) 	// checking if the user is pressing "e" on the keyboard
				{
					Debug.Log ("door locked");// log message
					audiolockedDoor.Play();//play locked door audio
					if(audioCluePlayed == false){//if audio clue not played
						audioMasterBedClue.Play ();		// play audio clue
						audioCluePlayed = true;//set audio clue played to true
					}
				}
			}
		}
		else if(_isplayerinzone == true && masterBedroomOpened == true){
			if (Input.GetKeyDown (KeyCode.Q)) 	// checking if the user is pressing "e" on the keyboard
			{
				openDoor.Play ();//play door opening audio
				SceneManager.LoadScene ("Bedroom", LoadSceneMode.Single); //load bedroom scene	
			}
		}
	}
}
