using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InputPasscode : MonoBehaviour {

	public static bool passcodeCorrect; 
	public InputField inputField;

	void Start(){
		EventSystem.current.SetSelectedGameObject(inputField.gameObject,null);//setting input field to null
		inputField.OnPointerClick (new PointerEventData (EventSystem.current));//focus cursor in input field
	}

	public void getPasscode(string passcode){
		if (passcode == PuzzleConstants.PAINTING_SAFE_CODE) { //check if the passcode entered is correct
			Debug.Log ("Passcode right"); //log message
			passcodeCorrect = true; //set passcdeCorrect to true
		}
	}
}
