using UnityEngine;
using System.Collections;

public class FireplaceView : MonoBehaviour {

	public GameObject cam1;
	public GameObject cam2;
	private bool audioKitchenCluePlayed = false;
	public AudioSource audioVaseColorClue;
	private bool puzzleSolved;

	void Start(){

		if (GameControl.control.hallwayDoorsUnlockPuzzle.TryGetValue(PuzzleConstants.KITCHEN_DOOR_PUZZLE, out puzzleSolved)) {
			if (puzzleSolved == true) {
				audioKitchenCluePlayed = true;
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
			Debug.Log ("enter fireplace zone");	// log message
			if (audioKitchenCluePlayed == false) {
				audioVaseColorClue.Play ();
				audioKitchenCluePlayed = true;
			}
		}		
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{

			Debug.Log ("exit fireplace zone");	// log message
			cam2.SetActive(false);
			cam1.SetActive(true);
		}
	}
}
