using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this script lifts the tank cover and destroys tap(collects)
public class TapCover : MonoBehaviour {
	public GameObject tapCover;
	public GameObject tapKnob;
	public GameObject tapSymbol;
	public static bool tapCoverLifted = false;
	public bool tapKnobTaken = false;
	private bool isEPressed = false;
	public AudioSource audio_findHammer;

	// Update is called once per frame
	void Update () {
		
		if (TapScript._isplayerinzone == true) { //if player in zone
			if (Input.GetKey (KeyCode.Q)) { //if Q is pressed
				isEPressed = true;
				if (tapCoverLifted == false) { //if toilet cover is not lifted
					tapCoverLifted = true;// set cover lifted to true
					Debug.Log ("tapCoverLifted =" + tapCoverLifted);//log message
					tapCover.transform.Translate (0, 0, 0.20f);//lift the cover
				}
			}
			if (isEPressed == true) {//if E is pressed
				
				if(tapKnobTaken == false && Input.GetKeyDown(KeyCode.E) && TapScript.tapKnobPicked == false){//if tap knob not taken and E is pressed and tap cover lifeted
					tapKnobTaken = true;//set tap knob taken to true
					audio_findHammer.Play ();//play clue picked audio
					Destroy(tapKnob);//destroy tap knob
					GameControl.control.guestBathroomPuzzle.Add (PuzzleConstants.TAP_KNOB_TAKEN, true);// add the clue picked to the guestBathroomPuzzle dictionary
					GameControl.control.i = Instantiate (GameControl.control.inventoryIcons [PuzzleConstants.PANEL_TAP_KNOB]); //instantiate the tap knob icon to be displayed in inventory
					GameControl.control.i.transform.SetParent (GameControl.control.inventoryPanel.transform);// display the tap knob in inventory panel
				}
			}
		}

	}
}
