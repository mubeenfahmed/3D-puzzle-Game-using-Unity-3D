using UnityEngine;
using System.Collections;

public class OpenWindow : MonoBehaviour {
	
	public GameObject windowSlideOpen;
	public GameObject cam1;
	public GameObject cam2;
	public GameObject numberBoard;
	private bool windowLifted = false;
	public AudioSource audioOpenWindow;
	private bool audioCluePlayed = false;
	public AudioSource audioBathWindowClue;
	private bool _isplayerinzone = false;
	public GameObject windowSlideClose;
	private bool masterBedroomPuzzleSolved;

	void Start ()
	{
		if (GameControl.control.masterBedroomPuzzle.TryGetValue (PuzzleConstants.MASTER_BATHROOM_OPENED, out masterBedroomPuzzleSolved)) { //check if the master bathroom is already opened
			if (masterBedroomPuzzleSolved == true) { //if masterbedroom puzzle is solved
				audioCluePlayed = true; //set audio clue played to true
			}
		}
	}

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		if (other.tag == "Player") { // the tag is reference to the gameobject (Player) 
			_isplayerinzone = true; // player in zone set to true
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true); //area camera focus set to true
			Debug.Log ("enter window zone");	// log message	
		}
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{	
		if (other.tag == "Player") {
			_isplayerinzone = false; //player out of zone 
			Debug.Log ("exit window zone");	// log message
			cam2.SetActive (false); //area camera focus set to false
			cam1.SetActive (true); //main camera focus set to true
			if (windowLifted == true) { //if window pane is lifted
				windowSlideClose.transform.Translate (0, -0.53f, 0); //close the window pane
				windowLifted = false; //windowLifted set to false
				numberBoard.SetActive (false); //numberBoard set Touch false
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		if (_isplayerinzone == true) {
			if (audioCluePlayed == false) { //if audio clue played is false
				audioBathWindowClue.Play (); //play audio clue
				audioCluePlayed = true; //audio clue played set to true
			}
			
			if (Input.GetKey (KeyCode.Q)) { //if Q is pressed
				if (windowLifted == false) { //if window pane not lifted
					windowLifted = true; //windowLifted to true
					Debug.Log ("windowSlideLifted =" + windowLifted); //log message
					windowSlideOpen.transform.Translate (0, 0.53f, 0); //open window pane
					audioOpenWindow.Play (); //play audio of window opening
					numberBoard.SetActive (true); //show the number board gameObject
					cam1.SetActive (false); //main camera focus set to false
					cam2.SetActive (true);  //area camera focus set to true
				}
			}
		}
	}
}
