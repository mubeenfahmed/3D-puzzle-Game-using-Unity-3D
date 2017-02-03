using UnityEngine;
using System.Collections;

public class ClockPuzzle : MonoBehaviour {

	public GameObject cam1;
	public GameObject cam2;
	public GameObject target;
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	public GameObject bird;
	public GameObject jizsawPiece;
	public AudioSource cocoClock;		// audio source for the drawer
	public AudioSource dialogueClueClock;
	private bool audioCluePlayed = false;
	int y =0;
	public static bool jigsawDropped;

	void Start(){
		
		if (GameControl.control.livingRoomPuzzle.TryGetValue(PuzzleConstants.LIVINGROOM_JIZSAW_DROPPED, out jigsawDropped)) {
			if (jigsawDropped == true) {
				jizsawPiece.SetActive (true);
				audioCluePlayed = true;
			}
		}
	}


	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			cam1.SetActive (false);
			cam2.SetActive (true);
			_isplayerinzone = true;			// if player is inside collider "Light_switch_collider" then this bool is set true
			if (jigsawDropped == false){
				if (audioCluePlayed == false) {
					dialogueClueClock.Play ();
					audioCluePlayed = true;
				}
				Debug.Log ("enter clock zone");	// log message
				Debug.Log ("The clock is mistaken ... its 11:15 now");	// log message
			}
		}	
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit clock zone");	// log message
			cam2.SetActive(false);
			cam1.SetActive(true);
		}
	}

	public void Update()					//function update where it updates with every frame of the play
	{
		int x = 0;
		if(_isplayerinzone == true && jigsawDropped == false)					// checking if the player is inside the collider "Light_switch_collider"
		{
			
			if (Input.GetKeyDown ("q"))		// checking if the user is pressing "e" on the keyboard
			{
				x = x - 30;
				y++;
				target.transform.Rotate(x,360,0);

				if (y == 5) {
					cocoClock.Play ();
					Debug.Log ("WIN");
					bird.SetActive (true);
					jizsawPiece.SetActive (true);
					jigsawDropped = true;
					GameControl.control.livingRoomPuzzle.Add (PuzzleConstants.LIVINGROOM_JIZSAW_DROPPED, true);
					}
				}
			}
		}

}
