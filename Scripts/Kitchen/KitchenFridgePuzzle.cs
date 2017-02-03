using UnityEngine;
using System.Collections;

public class KitchenFridgePuzzle : MonoBehaviour {

	public static bool knifeFound = false;
	public GameObject knife;
	public GameObject fridgeDoor;
	public AudioSource audio_KnifeSound;
	public AudioSource audioClue;
	private bool audioCluePlayed;
	private bool fridgeDoorOpen = false;
	public GameObject cam1;
	public GameObject cam2;
	private bool _isplayerinzone = false;
	private bool knifePicked;

	void Start(){
		
		if (GameControl.control.kitchenPuzzle.TryGetValue(PuzzleConstants.KITCHEN_FRIDGE_KNIFE_TAKEN, out knifePicked)) {// check if knife is picked
			if (knifePicked == true) {
				Destroy (knife);//destroy knife
				audioCluePlayed = true;//set audio clue played to true
			}
		}
	}

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// if player is inside collider "Light_switch_collider" then this bool is set true
			Debug.Log ("enter fridge zone");	// log message
			cam1.SetActive(false);//main camera focus set to false
			cam2.SetActive(true);//area camera focus set to true
		}				

	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit fridge zone");	// log message
			cam1.SetActive(true);//main camera focus set to true
			cam2.SetActive(false);//area camera focus set to false
			if (fridgeDoorOpen == true) { //if fridge not open
				fridgeDoor.transform.Translate (0.0138f,0.0104f,-0.019f);//close fridge
				fridgeDoor.transform.Rotate (0,0,39.19f);//close fridge
				fridgeDoorOpen = false;//set fridge open to false
			}
		}
	}

	public void Update()					//function update where it updates with every frame of the play
	{
		if(_isplayerinzone == true)					// checking if the player is inside the collider "Light_switch_collider"
		{
			if (audioCluePlayed == false) {//if audio clue not played
				audioClue.Play ();//play audio clue 
				audioCluePlayed = true;//set audio clue played to true
			}
			if (Input.GetKey (KeyCode.Q) ) {//if Q is pressed				
				if (fridgeDoorOpen == false) {//if fridge door not open
					fridgeDoorOpen = true;//set fridge open to true
					Debug.Log ("fridgeDoorOpen =" + fridgeDoorOpen);//log message
					fridgeDoor.transform.Translate (-0.0138f,-0.0104f,0.019f);//open fridge
					fridgeDoor.transform.Rotate (0,0,-39.19f);//open fridge
				}
			}

			if (Input.GetKeyDown (KeyCode.E) && knifeFound == false && fridgeDoorOpen == true)// checking if the user is pressing "e" on the keyboard
			{
				knifeFound = true; //set knife found to true
				Destroy (knife);//destroy knife
				audio_KnifeSound.Play ();// play the sound of the light switch
				Debug.Log ("Knife found");// log message
				GameControl.control.kitchenPuzzle.Add(PuzzleConstants.KITCHEN_FRIDGE_KNIFE_TAKEN,true);// add the clue picked to the kitchenPuzzle dictionary
				GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_KNIFE]);//instantiate the knife icon to be displayed in inventory
				GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the knife in inventory panel
			}
		}
	}
}
