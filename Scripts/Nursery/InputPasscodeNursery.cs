using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InputPasscodeNursery : MonoBehaviour {

	public static bool passcodeCorrect;
	public InputField inputField;

	void Start(){
		EventSystem.current.SetSelectedGameObject(inputField.gameObject,null);//setting input field to null
		inputField.OnPointerClick (new PointerEventData (EventSystem.current));//focus cursor in input field
	}

	public void getPasscode(string passcode){

		if (passcode == PuzzleConstants.NURSERY_CHEST_CODE) {//if passcode is correct
			Debug.Log ("Passcode right");//log  message
			passcodeCorrect = true;//set passcode correct to true
		}
	}
}
