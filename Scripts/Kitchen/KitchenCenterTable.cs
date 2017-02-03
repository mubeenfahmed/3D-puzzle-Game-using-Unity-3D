using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KitchenCenterTable : MonoBehaviour
{

	public GameObject closedBox;
	public GameObject openBox;
	public AudioSource audio_KeySound;
	public AudioSource audioKitchenBoxClue;
	private bool audioBoxCluePlayed;
	private bool masterBathkeyPicked = false;
	public GameObject cam1;
	public GameObject cam2;
	private bool _isplayerinzone = false;
	public static bool isBoxOpen = false;
	private bool boxOpen;

	void Start(){
		
		if (GameControl.control.kitchenPuzzle.TryGetValue(PuzzleConstants.MASTER_BATHROOM_KEY_TAKEN, out masterBathkeyPicked)) {// check if bathroom key picked
			if (masterBathkeyPicked == true) {
				Destroy (closedBox);//destroy closed box
				openBox.SetActive (true);//set opened box active
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
			if (masterBathkeyPicked == false && audioBoxCluePlayed == false) {//if key is picked and audio clue not played
				audioKitchenBoxClue.Play ();//play audio clue
				audioBoxCluePlayed = true;//set audio clue played to true
			}
		}	
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit table zone");	// log message
			cam1.SetActive (true);//main camera focus set to true
			cam2.SetActive (false);//area camera focus set to false
		}
	}

	public void Update ()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true) {					// checking if the player is inside the collider "Light_switch_collider"
			
			if (KitchenFridgePuzzle.knifeFound == true && Input.GetKeyDown (KeyCode.E)) { //if knife is found and E is pressed
				Destroy (closedBox);//destroy closed box
				openBox.SetActive (true);//set open box active
				audioBoxCluePlayed = true;//set audio clue played to true
				foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through inventory icons
					if (child.gameObject.tag == "Knife") {//if knife icon found
						Destroy (child.gameObject);//destroy knife icon
					}
				}
				isBoxOpen = true;//set box opened to true
				GameControl.control.kitchenPuzzle.Add(PuzzleConstants.KITCHEN_BOX_OPENED,true);// add the clue picked to the kitchenPuzzle dictionary
			}
		}
	}	
}
