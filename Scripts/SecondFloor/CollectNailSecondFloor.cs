using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectNailSecondFloor : MonoBehaviour {

	public AudioSource audio_KeySound;			// audio source for finding the key
	public GameObject cam1;
	public GameObject cam2;
	public GameObject nail;
	private bool nailOnSecondFloorPicked;
	private bool _isplayerinzone = false;
	private bool nailAlreadyInPanel;

	void Start(){

		if (GameControl.control.nurseryPuzzle.TryGetValue(PuzzleConstants.NAIL_ON_SECOND_FLOOR, out nailOnSecondFloorPicked)) {//check if nail found
			if (nailOnSecondFloorPicked == true) {//if true
				Destroy (nail);//destroy nail
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			cam1.SetActive (false);//main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
			_isplayerinzone = true;//set player in zone to true
			Debug.Log ("enter on 2nd");	// log message
		}	
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)

			Debug.Log ("exit 2nd");	// log message
			cam2.SetActive (false);//area camera focus set to false
			cam1.SetActive (true);//main camera focus set to true
			_isplayerinzone = false;//set player in zone to false
		}
	}

	public void Update()					//function update where it updates with every frame of the play
	{
		if(_isplayerinzone == true)					// checking if the player is inside the collider "Light_switch_collider"
		{
			if (Input.GetKeyDown (KeyCode.E) && nailOnSecondFloorPicked == false)	// checking if the user is pressing "e" on the keyboard
			{
				nailOnSecondFloorPicked = true;//set nail found to true
				Destroy (nail);//destroy nail
				audio_KeySound.Play ();		// play the sound of the light switch
				Debug.Log ("Nail found on 2nd");// log message		
				GameControl.control.nurseryPuzzle.Add(PuzzleConstants.NAIL_ON_SECOND_FLOOR,true);//add clue to nurseryPuzzle dictionary
				foreach (Transform child in GameControl.control.inventoryPanel.transform) {//loop through inventory
					if (child.gameObject.tag == "Nails") {//if nail icon found
						string count = child.Find ("Text").GetComponent<Text> ().text;//get text component of nail icon
						int countNails = System.Int32.Parse (count) + 1;//increment the count
						child.Find ("Text").GetComponent<Text> ().text = "" + countNails;//update the text with current count
						nailAlreadyInPanel = true;//set nail already in panel to true
						return;
					}
				}
				if (nailAlreadyInPanel == false) {//if nail not already in panel
					GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_NAILS]);//instantiate the nail icon to be displayed in inventory
					GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform); // display the nail in inventory panel
				}
			}
		}
	}
}
