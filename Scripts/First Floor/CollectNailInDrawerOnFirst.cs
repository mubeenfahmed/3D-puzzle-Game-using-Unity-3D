using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectNailInDrawerOnFirst : MonoBehaviour
{

	public static bool Doorkey = false;	// static var for door key
	public GameObject nail;
	public AudioSource audio_DrawerOpen;	// audio source for the drawer
	public AudioSource audio_KeyFound;	// audio source for finding the key
	public GameObject cam1;
	public GameObject cam2;
	public GameObject target;
	private bool drawerOpen = false;	// bool in this script to control the light switch, if its on or off
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	private bool nailOnFirstFloorPicked;
	private bool nailDestroyAudioPlayed;
	private bool nailAlreadyInPanel;

	void Start(){

		if (GameControl.control.nurseryPuzzle.TryGetValue(PuzzleConstants.NAIL_ON_FIRST_FLOOR, out nailOnFirstFloorPicked)) { // check if the nail inside nurseryPuzzle  is already picked
			if (nailOnFirstFloorPicked == true) { //if nail picked is true
				Destroy (nail); //destroy nail
				nailDestroyAudioPlayed = true; //set nail picked audio to true
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			_isplayerinzone = true;			// if player is inside collider "Light_switch_collider" then this bool is set true
			Debug.Log ("enter drawer zone");	// log message
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
		}		
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit drawer zone");	// log message
			cam2.SetActive (false); //area camera focus set to false
			cam1.SetActive (true); //main camera focus set to true
			if (drawerOpen == true) { //if drawer open
				target.transform.Translate ((float)-0.18, 0, 0); //close the drawer
				drawerOpen = false; //set drawe open to false
			}
		}
	}

	public void Update ()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true) {					// checking if the player is inside the collider "Light_switch_collider"
			if (Input.GetKeyDown ("q")) {		// check if Q is pressed				
				if (drawerOpen == false) {		//if drawer is open is false
					target.transform.Translate ((float)0.18, 0, 0); //open the drawer
					audio_DrawerOpen.Play (); //play drawer opening audio
					drawerOpen = true;//set drawer open to true
				}
			}
			if (drawerOpen == true && Input.GetKeyDown (KeyCode.E) && nailOnFirstFloorPicked == false) { //if drawer is open, E is pressed and nail not picked
				Destroy (nail); //destroy nail
				audio_KeyFound.Play ();
				nailOnFirstFloorPicked = true; //set nail picked to true
				GameControl.control.nurseryPuzzle.Add(PuzzleConstants.NAIL_ON_FIRST_FLOOR,true);// add the clue picked to the nurseryPuzzle dictionary
				foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through the inventory items
					if (child.gameObject.tag == "Nails") { //if the icon is nail
						string count = child.Find ("Text").GetComponent<Text> ().text; //get the text component of the nail icon
						int countNails = System.Int32.Parse (count) + 1; //increment the count
						child.Find ("Text").GetComponent<Text> ().text = "" + countNails; //set the text component of the nail icon with current count
						nailAlreadyInPanel = true; //set nail already in panel to true
						return;
					}
				}
				if (nailAlreadyInPanel == false) { //if nail not in panel
					GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_NAILS]); //instantiate the nail icon to be displayed in inventory 
					GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the nail in inventory panel
				}
				if(nailDestroyAudioPlayed == false){ //if nail audio not played
					audio_KeyFound.Play ();		// play nail picked audio
					nailDestroyAudioPlayed = true; //set nail picked audio played to true
				}
				Doorkey = true;				
				Debug.Log ("key found");// log message
			}
		}
	}
}
