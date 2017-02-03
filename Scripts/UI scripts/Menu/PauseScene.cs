using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseScene : MonoBehaviour {

	public Transform canvas;
	public GameObject controlTextExplanation;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {//if escape is pressed
			Pause ();//pause the game
		}
	}

	public void Pause(){
		
		if (canvas.gameObject.activeInHierarchy == false) {// if canvas is not active
			canvas.gameObject.SetActive (true);//set canvas active
			Time.timeScale = 0;//pause the game
		} else {
			canvas.gameObject.SetActive (false);//set canvas to false
			Time.timeScale = 1;//play the game
		}
	}

	public void Quit(){

		Application.Quit (); //close the application
	}

	public void Controls(){

		if (controlTextExplanation.gameObject.activeInHierarchy == false) { // if control text is false
			controlTextExplanation.SetActive (true); //display control text
		} else {
			controlTextExplanation.SetActive (false);//hide control text
		}
	}
}
