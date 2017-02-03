using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NurseryChest : MonoBehaviour {

	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	public GameObject BedroomKey;
	public GameObject ChestDoor;
	public AudioSource puzzleFound;
	private bool chestDoorOpen = false;
	private bool keyCollected = false;
	public GameObject camMain;
	public GameObject camToys;
	public GameObject camChestbox;
	public GameObject canvas;
	public Text safeText;
	private bool masterBedroomKeyFound;
	public GameObject ChestBoxClose;
	public GameObject ChestBoxOpen;

	void Start ()
	{

		if (GameControl.control.hallwayDoorsUnlockPuzzle.TryGetValue (PuzzleConstants.MASTER_BEDROOM_KEY, out masterBedroomKeyFound)) {// check if bedroom key is picked
			if (masterBedroomKeyFound == true) { //if true
				canvas.SetActive(false);//set canvas active
				Destroy (BedroomKey);//destroy key
				ChestBoxOpen.SetActive (true);//set chest box open
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
			camChestbox.SetActive(true);//area camera focus set to true
			camMain.SetActive (false);//main camera focus set to false
		}				

	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "door_collider" then this bool is set false
			Debug.Log ("exit door zone");	// log message
			canvas.SetActive (false);//set canvas active
			camChestbox.SetActive(false);//area camera focus set to false 
			camMain.SetActive (true);//main camera focus set to true
		}
	}

	void Update ()							//function update where it updates with every frame of the play
	{
		if (_isplayerinzone) { 				// checking if the player is inside the collider "door_collider"
			if (chestDoorOpen == false && masterBedroomKeyFound == false) {//if chest box not open and master bedroom key not found
				canvas.SetActive (true);//set canvas acctive
			}

			if (Input.GetKeyDown (KeyCode.E)) {		// checking if the user is pressing "e" on the keyboard
				if (chestDoorOpen == true && masterBedroomKeyFound == false) {//if chest box open and master key not picked
					puzzleFound.Play ();//play clue picked audio
					Debug.Log ("key Collected");//log message
					Destroy (BedroomKey);//destroy key
					canvas.SetActive (false);//set canvas to false
					keyCollected = true;//set key collected to true
					masterBedroomKeyFound = true;//set master key found to false
					GameControl.control.hallwayDoorsUnlockPuzzle.Add (PuzzleConstants.MASTER_BEDROOM_KEY, true);//add clue to nurseryPuzzle dictionary
					GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_BEDROOM_KEY]);//instantiate the key icon to be displayed in inventory
					GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform);// display the key in inventory panel
				}
			}
			openChest ();//call open chest function
		}
	}

	void openChest()
	{
		if (InputPasscodeNursery.passcodeCorrect == true) {//if passcode is correct
			canvas.SetActive (false);//set canvas to false
			ChestBoxClose.SetActive (false);//set chest box close to false
			ChestBoxOpen.SetActive (true);//set chest box open to true
			chestDoorOpen = true;//chest door open to true
			Debug.Log ("PASS CODE CORRECT");//log message
		}else
		{
			Debug.Log ("Passcode not correct");//log message
		}
	}
}
