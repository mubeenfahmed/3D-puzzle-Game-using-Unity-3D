using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {  //default class name 

	public Light Light; 					// var for the Basement_Spotlight
	public GameObject text;				// var for the text "Press E"
	public GameObject arrow;				// var for the arrow_right
	public AudioSource sound;				// var for Audio_switch
	private bool _switchOn;					// bool in this script to control the light switch, if its on or off
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
											// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// if player is inside collider "Light_switch_collider" then this bool is set true
			Debug.Log ("enter switch zone");	// log message

		}				

	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "Light_switch_collider" then this bool is set false
			Debug.Log ("exit switch zone");	// log message
		}
	}

	void Update()							//function update where it updates with every frame of the play
	{
		if(_isplayerinzone)					// checking if the player is inside the collider "Light_switch_collider"
		{				
			if (Input.GetKeyDown ("e")) 	// checking if the user is pressing "e" on the keyboard
			{
				if (!_switchOn) 			// checking if this bool is (not true) it means the light switch it off
				{
					_switchOn = true;		// make the light switch bool = true
					sound.Play ();			// play the sound of the light switch
					Light.enabled = true;	// turn Basement_Spotlight on
					Destroy (text);		// remove the "press e" text
					Destroy (arrow);		// remove the arrow_right
				}

			}
		}
	}
}
