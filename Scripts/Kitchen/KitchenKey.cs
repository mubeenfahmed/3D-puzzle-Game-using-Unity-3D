using UnityEngine;
using System.Collections;

public class KitchenKey : MonoBehaviour {

	private bool _isplayerinzone = false;
	private bool nearBox = false;
	public GameObject cam1;
	public GameObject key;
	public GameObject cam2;
	public AudioSource audio_KeySound;
	private bool keyPicked;
	private bool boxOpen;

	void Start(){

		if (GameControl.control.kitchenPuzzle.TryGetValue(PuzzleConstants.MASTER_BATHROOM_KEY_TAKEN, out keyPicked)) {// check if key is picked
			if (keyPicked == true) {
				Destroy (key);//destroy key
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			_isplayerinzone = true;			// if player is inside collider "Light_switch_collider" then this bool is set true
			Debug.Log ("enter table zone");	// log message
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
			nearBox = true;//set near box to true
		}	
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit table zone");	// log message
			cam1.SetActive (true);//main camera focus set to true
			cam2.SetActive (false);//area camera focus set to false
			nearBox = false;//set near box to false
		}
	}

	public void Update ()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true) {					// checking if the player is inside the collider "Light_switch_collider"

			if ( KitchenCenterTable.isBoxOpen == true && nearBox == true && keyPicked == false) {		// checking if the user is pressing "e" on the keyboard
				Debug.Log ("inside isBoxOpen");// log message
				Debug.Log ("inside nearBox");// log message
				if (Input.GetKeyDown(KeyCode.E)) {//if E is pressed
					Destroy (key);//destroy key
					keyPicked = true;
					audio_KeySound.Play ();		// play the sound of the light switch
					Debug.Log ("Key found");// log message
					GameControl.control.kitchenPuzzle.Add(PuzzleConstants.MASTER_BATHROOM_KEY_TAKEN,true);// add the clue picked to the kitchenPuzzle dictionary
					GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_BATHROOM_KEY]);//instantiate the key icon to be displayed in inventory
					GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform);// display the key in inventory panel
				}
			}
		}
	}
}
