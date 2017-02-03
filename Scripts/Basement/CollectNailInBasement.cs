using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectNailInBasement : MonoBehaviour {

	public AudioSource audio_KeySound;	
	public GameObject cam1;
	public GameObject cam2;
	public GameObject nail;
	private bool nailInBasementPicked;
	private bool _isplayerinzone = false;
	private bool nailAlreadyInPanel;

	void Start(){
		
		if (GameControl.control.nurseryPuzzle.TryGetValue(PuzzleConstants.NAIL_IN_BASEMENT, out nailInBasementPicked)) { // check if the nail is already picked
			if (nailInBasementPicked == true) { //if nail already picked
				Destroy (nail); // if true destroy the nail
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
			_isplayerinzone = true; //player in area set to true
			Debug.Log ("enter tools zone");	// log message
		}	
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)

			Debug.Log ("exit tools zone");	// log message
			cam2.SetActive (false); //main camera focus set to true
			cam1.SetActive (true); //area camera focus set to false
			_isplayerinzone = false;  //player in area set to false
		}
	}

	public void Update()					//function update where it updates with every frame of the play
	{
		if(_isplayerinzone == true)					// checking if the player is inside the collider
		{
			if (Input.GetKeyDown (KeyCode.E) && nailInBasementPicked == false)	// checking if the user is pressing "e" on the keyboard
			{					
				Destroy (nail); // destroy the nail
				audio_KeySound.Play ();		// play audio of picking the clue
				Debug.Log ("Nail found in basement"); // log message		
				GameControl.control.nurseryPuzzle.Add(PuzzleConstants.NAIL_IN_BASEMENT,true); // add the clue picked to the nurseryPuzzle dictionary
				foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through the items in inventory panel
					if (child.gameObject.tag == "Nails") { //if nail alreay exists, increment its value by 1
						string count = child.Find ("Text").GetComponent<Text> ().text; //get text component of the item nail
						int countNails = System.Int32.Parse (count) + 1; //increment it by 1
						child.Find ("Text").GetComponent<Text> ().text = "" + countNails; //change the value of the item's text component
						nailAlreadyInPanel = true;
						return;
					}
				}
				if (nailAlreadyInPanel == false) { //if nail is not already in the inventory panel
					GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_NAILS]); //instantiate the nail icon to be displayed in inventory
					GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the nail in inventory panel
				}				
				nailInBasementPicked = true; //set nail in basement picked to true
			}
		}
	}
}
