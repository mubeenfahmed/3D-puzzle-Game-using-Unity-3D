using UnityEngine;
using System.Collections;

public class CollectJigsawMasterBathroom : MonoBehaviour {

	public GameObject jigsaw;
	private bool _isplayerinzone = false;
	public AudioSource audioKnobFixed;
	public AudioSource audioJigsawPicked;
	private bool cluePicked;
	private bool masterBathJigsawPicked;
	public GameObject cam1;
	public GameObject cam2;
	public static bool jigsawDropped = false;

	void Start ()
	{
		if (GameControl.control.masterBathroomPuzzle.TryGetValue(PuzzleConstants.MASTER_BATHROOM_JIZSAW, out masterBathJigsawPicked)) { // check if the jigsaw inside bathroom is already picked
			if (masterBathJigsawPicked == true) { //if jigsaw already picked
				Debug.Log ("Inside start in master bathrrom"); //log message
				cluePicked = true; //set clue picked to true
				Destroy(jigsaw); //if yes destroy the jigsaw gameobject
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		if (other.tag == "Player"){		// the tag is reference to the gameobject (Player)

			_isplayerinzone = true; // if player is inside collider 
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
			Debug.Log ("enter basin zone");	// log message
		}
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										
		if (other.tag == "Player"){		// the tag is reference to the gameobject (Player)
			_isplayerinzone = false; // if player is outside collider then this bool is set false
			Debug.Log ("exit basin zone");	// log message
			cam2.SetActive (false); //main camera focus set to true
			cam1.SetActive (true); //area camera focus set to false
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (_isplayerinzone == true && cluePicked == false) { // check if the player is inside the collider and jigsaw is picked
			
			if (Input.GetKeyDown(KeyCode.Q) && FixTapKnob.tapKnobFixed == true) { //check if Q is pressed and tap knob is fixed

					jigsaw.SetActive (true); //set the gameobject jigsaw to active
					Debug.Log ("Jigsaw found"); //log message
					jigsawDropped = true; // jigsaw dropped from tap to true
				}
			if (Input.GetKeyDown(KeyCode.E) && jigsawDropped == true) { //if E is pressed and jigsaw
				Debug.Log ("Jigsaw picked");
				GameControl.control.masterBathroomPuzzle.Add (PuzzleConstants.MASTER_BATHROOM_JIZSAW, true);//add the clue picked to the masterBathroomPuzzle dictionary
				GameControl.control.mainDoorPuzzle.Add (PuzzleConstants.MASTER_BATHROOM_JIZSAW, true);//add the clue picked to the mainDoorPuzzle dictionary
				cluePicked = true; //set clue picked to true
				Destroy(jigsaw); // destroy the gameobject jigsaw
				audioJigsawPicked.Play (); //play the audio clue picked
				GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_TAP_JIGSAW]); //instantiate the jigsaw icon to be displayed in inventory
				GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform);// display the jigsaw in inventory panel
			}
		}
	}
}
