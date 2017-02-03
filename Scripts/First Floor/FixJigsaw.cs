using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FixJigsaw : MonoBehaviour {

	private bool _isplayerinzone = false;
	public GameObject yellowJigsaw;	// var for the text "Found key"
	public GameObject redJigsaw;
	public GameObject blueJigsaw;
	public GameObject greenJigsaw;
	public AudioSource audioDoorOpen;	// audio source for the drawer
	public AudioSource audioJigsawFixed;	// audio source for finding the key
	public GameObject cam1;
	public GameObject cam2;
	private bool carJigsaw;
	private bool tapJigsaw;
	private bool paintingJigsaw;
	private bool clockJigsaw;
	public GameObject doorClose;
	public GameObject doorOpen;
	public AudioSource audioMainDoorClue1;
	public AudioSource audioMainDoorClosed;
	private bool cribFixed;
	public static bool playAudioClue;

	void Start(){
		
		if (GameControl.control.nurseryPuzzle.TryGetValue (PuzzleConstants.CRIB_FIXED, out cribFixed)) { // check if crib is fixed
			if (cribFixed == true) { //if true
				if (PuzzleConstants.MAIN_DOOR_CLUE == true) { //if main door clue is true
					playAudioClue = true; //play clue audio
				}
			}
		}
		if (GameControl.control.mainDoorPuzzleFixed.TryGetValue(PuzzleConstants.CAR_JIZSAW, out carJigsaw)) { //if car jigsaw is fixed
			if (carJigsaw == true) { //if true
				greenJigsaw.SetActive (true); //set jigsaw active
			}
		}
		if (GameControl.control.mainDoorPuzzleFixed.TryGetValue(PuzzleConstants.LIVINGROOM_JIZSAW, out clockJigsaw)) {//if clock jigsaw is fixed
			if (clockJigsaw == true) {//if true
				yellowJigsaw.SetActive (true);//set jigsaw active
			}
		}
		if (GameControl.control.mainDoorPuzzleFixed.TryGetValue(PuzzleConstants.MASTER_BATHROOM_JIZSAW, out tapJigsaw)) { //if tap jigsaw is fixed
			if (tapJigsaw == true) {//if true
				blueJigsaw.SetActive (true);//set jigsaw active
			}
		}
		if (GameControl.control.mainDoorPuzzleFixed.TryGetValue(PuzzleConstants.MASTER_BEDROOM_JIZSAW, out paintingJigsaw)) {//if painting jigsaw is fixed
			if (paintingJigsaw == true) {//if true
				redJigsaw.SetActive (true);//set jigsaw active
			}
		}
	} 

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			_isplayerinzone = true;		// set player in zone to true
			Debug.Log ("enter drawer zone");	// log message
			cam1.SetActive (false); //main camera focus set to false
			cam2.SetActive (true);//area camera focus set to true
		}		
	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// set player in zone to false
			Debug.Log ("exit drawer zone");	// log message
			cam2.SetActive (false);//area camera focus set to false
			cam1.SetActive (true);//main camera focus set to true
		}
	}

	public void Update (){
		
		if (_isplayerinzone == true) { //if player in zone
			if (Input.GetKey (KeyCode.Q)) { //if Q is pressed
				audioMainDoorClosed.Play (); //play door closed audio
				if (PuzzleConstants.MAIN_DOOR_CLUE == false && cribFixed == true) { //if main door clue is false and crib is fixed
					PuzzleConstants.MAIN_DOOR_CLUE = true; //set main door clue to true
					audioMainDoorClue1.Play (); //play clue 1 audio
					playAudioClue = true;//set clue audio played to true
				}
			}
		}
		if (Input.GetKey (KeyCode.E) && _isplayerinzone == true) { //if E is pressed
			if (carJigsaw == false) { //if car jigsaw fixed to puzzle is false
				if (GameControl.control.mainDoorPuzzle.TryGetValue (PuzzleConstants.CAR_JIZSAW, out carJigsaw)) {//if car jgsaw is found
					if (carJigsaw == true) {
						greenJigsaw.SetActive (true); //set active jigsaw
						audioJigsawFixed.Play (); //play set puzzle audio
						Debug.Log ("Found green");//log message
						GameControl.control.mainDoorPuzzleFixed.Add (PuzzleConstants.CAR_JIZSAW, true); // add the clue picked to the mainDoorPuzzleFixed dictionary
						foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through inventory icons
							if (child.gameObject.tag == "GreenJigsaw") { //if green jigsaw found
								Destroy (child.gameObject);//destroy green jigsaw
								Debug.Log ("Destroy green");//log message
							}
						}
					}
				}
			}
			if (clockJigsaw == false && _isplayerinzone == true) {//if clock jigsaw fixed to puzzle is false
				if (GameControl.control.mainDoorPuzzle.TryGetValue (PuzzleConstants.LIVINGROOM_JIZSAW, out clockJigsaw)) {//if clock jgsaw is found
					if (clockJigsaw == true) {
						yellowJigsaw.SetActive (true);//set active jigsaw
						audioJigsawFixed.Play ();//play set puzzle audio
						GameControl.control.mainDoorPuzzleFixed.Add (PuzzleConstants.LIVINGROOM_JIZSAW, true);// add the clue picked to the mainDoorPuzzleFixed dictionary
						Debug.Log ("Found yellow");//log message
						foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through inventory icons
							if (child.gameObject.tag == "YellowJigsaw") {//if yellow jigsaw found
								Destroy (child.gameObject);//destroy yellow jigsaw
								Debug.Log ("Destroy yellow");//log message
							}
						}
					}
				}
			}
			Debug.Log ("clockJigsaw-" + clockJigsaw);
			if (tapJigsaw == false && _isplayerinzone == true) {//if tap jigsaw fixed to puzzle is false
				if (GameControl.control.mainDoorPuzzle.TryGetValue (PuzzleConstants.MASTER_BATHROOM_JIZSAW, out tapJigsaw)) {//if tap jgsaw is found
					if (tapJigsaw == true) {
						blueJigsaw.SetActive (true);//set active jigsaw
						audioJigsawFixed.Play ();//play set puzzle audio
						Debug.Log ("Found blue");//log message
						GameControl.control.mainDoorPuzzleFixed.Add (PuzzleConstants.MASTER_BATHROOM_JIZSAW, true);// add the clue picked to the mainDoorPuzzleFixed dictionary
						foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through inventory icons
							if (child.gameObject.tag == "BlueJigsaw") {//if blue jigsaw found
								Destroy (child.gameObject);//destroy blue jigsaw
								Debug.Log ("Destroy blue");//log message
							}
						}
					}
				}
			}
			if (paintingJigsaw == false && _isplayerinzone == true) { //if painting jigsaw fixed to puzzle is false
				if (GameControl.control.mainDoorPuzzle.TryGetValue (PuzzleConstants.MASTER_BEDROOM_JIZSAW, out paintingJigsaw)) {//if painting jgsaw is found
					if (paintingJigsaw == true) {
						redJigsaw.SetActive (true);//set active jigsaw
						audioJigsawFixed.Play ();//play set puzzle audio
						Debug.Log ("Found red");//log message
						GameControl.control.mainDoorPuzzleFixed.Add (PuzzleConstants.MASTER_BEDROOM_JIZSAW, true);// add the clue picked to the mainDoorPuzzleFixed dictionary
						foreach (Transform child in GameControl.control.inventoryPanel.transform) { //loop through inventory icons
							if (child.gameObject.tag == "RedJigsaw") {//if red jigsaw found
								Destroy (child.gameObject);//destroy red jigsaw
								Debug.Log ("Destroy red");//log message
							}
						}
					}
				}
			}
		}
		if (GameControl.control.mainDoorPuzzleFixed.Count == PuzzleConstants.TOTAL_JIGSAWS) { //if fixed jigsaws equal total jigsaw
			doorClose.SetActive (false); //set door closed to false
			doorOpen.SetActive (true);//set door open to active
			audioDoorOpen.Play ();//play door open audio
			GameControl.control.canvasPickUpItems.SetActive(false);
			SceneManager.LoadScene ("ending", LoadSceneMode.Single);//load main menu scene
		}
	}
}
