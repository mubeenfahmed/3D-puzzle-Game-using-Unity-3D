using UnityEngine;
using System.Collections;

public class Lockeddoor : MonoBehaviour {

	public AudioSource door_sound;			// audio source for the door
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone

	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
		
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// if player is inside collider, then this bool is set true
			Debug.Log ("enter door zone");	// log message

		}				

	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider , then this bool is set false
			Debug.Log ("exit door zone");	// log message
		}
	}

	void Update ()							//function update where it updates with every frame of the play
	{
		if (_isplayerinzone) 				// checking if the player is inside the collider
		{
			if (Input.GetKeyDown (KeyCode.Q)) 	// checking if the user is pressing "q" on the keyboard
			{
				Debug.Log ("door locked");// log message
				door_sound.Play ();		// play sound of locked door 
			}
		}
	}
}
