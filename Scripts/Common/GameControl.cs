using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public static GameControl control; // object of GameControl class
	public Dictionary<string,bool> guestBathroomPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> livingRoomPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> masterBathroomPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> masterBedroomPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> mainDoorPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> mainDoorPuzzleFixed = new Dictionary<string,bool>();
	public Dictionary<string,bool> hallwayDoorsUnlockPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> kitchenPuzzle = new Dictionary<string,bool>();
	public Dictionary<string,bool> nurseryPuzzle = new Dictionary<string,bool>();
	public GameObject canvasResumeMenu;
	public GameObject canvasPickUpItems;
	public GameObject inventoryPanel;
	public GameObject[] inventoryIcons;
	public GameObject i;
	public AudioSource backgroundMusic;

	void Awake(){ //setting the object before scene loading

		if (control == null) { //if object is null
			DontDestroyOnLoad (gameObject); //dont destroy the gameobject
			control = this; //control is current object
			DontDestroyOnLoad (canvasPickUpItems); //dont destroy inventory canvas
			DontDestroyOnLoad (canvasResumeMenu);//dont destroy Resume menu
		}
		else if (control != this){ //if control is not current object
			Destroy (gameObject); //destroy the gameobject
		}
	}

	void Start(){
		backgroundMusic.Play ();//play background music
	}

}
