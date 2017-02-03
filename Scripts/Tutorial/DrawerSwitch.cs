using UnityEngine;
using System.Collections;

public class DrawerSwitch : MonoBehaviour
{

	public static bool Doorkey = false;	// static var for door key
	public GameObject key_canvas;
	public GameObject press_e;
	public GameObject press_q;	
	public GameObject key;
	public AudioSource audio_DrawerOpen;	// audio source for the drawer
	public AudioSource audio_KeyFound;	// audio source for finding the key
	public GameObject cam1;
	public GameObject cam2;
	public GameObject target;
	private bool drawerOpen = false;	// bool in this script to control the light switch, if its on or off
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone

	void OnTriggerEnter (Collider other) 	// function of when the player enters the collider zone
	{	
		
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player) 
			_isplayerinzone = true;			// if player is inside collider, then this bool is set true
			Debug.Log ("enter drawer zone");	// log message
			cam1.SetActive (false);				// main camera turned off
			cam2.SetActive (true);				// second camera turned on
			press_q.SetActive (true);			// the word " press q" is turned on
		}				

	}

	void OnTriggerExit (Collider other)		// function of when the player exits the collider zone
	{										
		if (other.tag == "Player") {			// the tag is reference to the gameobject (Player)
			_isplayerinzone = false;		// if player is outside collider , then this bool is set false
			Debug.Log ("exit drawer zone");	// log message
			cam2.SetActive (false);
			cam1.SetActive (true);
			press_q.SetActive (false);
			press_e.SetActive (false);
			if (drawerOpen == true) {
				target.transform.Translate ((float)-0.18, 0, 0);
				drawerOpen = false;
			}
		}
	}

	public void Update ()					//function update where it updates with every frame of the play
	{
		if (_isplayerinzone == true) {					// checking if the player is inside the collider
			//press_q.SetActive (true);
			if (Input.GetKeyDown ("q")) {		// checking if the user is pressing "q" on the keyboard
				press_q.SetActive (false); 		// the word " press q" will disappear
				if (drawerOpen == false) {		// checking if this bool is (not true) it means the drawer it off
					target.transform.Translate ((float)0.18, 0, 0); // opening the drawer
					audio_DrawerOpen.Play ();
					drawerOpen = true;
					if (Doorkey == false) { 
						press_e.SetActive (true);
					}
					
				}
			}
			if (drawerOpen == true && Input.GetKeyDown (KeyCode.E) && Doorkey == false) { // if player presses "e" and the key is not taken yet
				Destroy (key);				// remove the key from the drawer
				audio_KeyFound.Play ();		// play the sound of the light switch
				key_canvas.SetActive (true);	// canvas picture of key is showing in the top of screen
				Doorkey = true;		
				press_e.SetActive (false); // remove the "press e" word
				Debug.Log ("key found");// log message

			}
		}

	}
}
