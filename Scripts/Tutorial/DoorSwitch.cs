using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorSwitch : MonoBehaviour
{

	public AudioSource door_sound;			// audio source for the door
	public AudioSource locked_door;
	public GameObject canvas;
	private bool _doorOn = false;			// bool in this script to control the light switch, if its on or off
	private bool _isplayerinzone = false;	// bool in this script to check if the player is in the collider zone
	void OnTriggerEnter(Collider other) 	// function of when the player enters the collider zone
	{	
											// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player) 
		{	
			_isplayerinzone = true;			// if player is inside collider "door_collider" then this bool is set true
			Debug.Log ("enter door zone");	// log message
		}	
	}

	void OnTriggerExit(Collider other)		// function of when the player exits the collider zone
	{										// Collider = class , other = object inside this class
		if (other.tag == "Player")			// the tag is reference to the gameobject (Player)
		{
			_isplayerinzone = false;		// if player is outside collider "door_collider" then this bool is set false
			Debug.Log ("exit door zone");	// log message
		}
	}


	void Update ()							//function update where it updates with every frame of the play
	{
		if (_isplayerinzone) 				// checking if the player is inside the collider "door_collider"
		{
			if (Input.GetKeyDown ("q")) 	// checking if the user is pressing "e" on the keyboard
			{
				if( DrawerSwitch.Doorkey == true) // checking if the player has 1 or more door key
				{
					Destroy (canvas);		// remove the key picture from canvas
					_doorOn = true;			// make the door bool = true, it means open the door
					Debug.Log ("door open");// log message
					door_sound.Play ();		// play sound of door opening 
					DrawerSwitch.Doorkey = false;// reset the counter for keys now after using the one we collected from drawer
					SceneManager.LoadScene ("Tutorial2", LoadSceneMode.Single); //load next scene

				} 
				else 
				{
					locked_door.Play (); // if the player didnt find the key, then play this sound 
					Debug.Log (" key is not there, so door is locked"); // log message of the player didnt collect any key
				}
			}
		}
	}
}