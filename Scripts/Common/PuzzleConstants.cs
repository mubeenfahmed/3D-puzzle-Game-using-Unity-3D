using UnityEngine;
using System.Collections;

public class PuzzleConstants : MonoBehaviour {

	//constant variables used  to keep track of clues picked
	public static string TAP_KNOB_TAKEN = "guestBathTapKnobTaken";
	public static string TAP_KNOB_FIXED = "masterBathTapFixed";
	public static string LIVINGROOM_JIZSAW = "livingroomJigsawTaken";
	public static string LIVINGROOM_JIZSAW_DROPPED = "livingroomJigsawDropped";
	public static string MASTER_BATHROOM_JIZSAW = "masterBathJigsawTaken";
	public static string MASTER_BEDROOM_JIZSAW = "masterBedJigsawTaken";
	public static string MAIN_DOOR_JIZSAW = "mainDoorJigsawsCollected";
	public static string CAR_JIZSAW = "carJigsawsCollected";
	public static string KITCHEN_DOOR_PUZZLE = "kitchenDoorUnlocked";
	public static string KITCHEN_FRIDGE_KNIFE_TAKEN = "kitchenFridgeKnifeTaken";
	public static string KITCHEN_BOX_OPENED = "kitchenBoxOpened";
	public static string MASTER_BATHROOM_KEY_TAKEN = "kitchenBoxKeyTaken";
	public static string NAIL_ON_SECOND_FLOOR = "nailOnSecondFloor";
	public static string NAIL_IN_BASEMENT = "nailInBasement";
	public static string NAIL_ON_FIRST_FLOOR = "nailOnFirstFloor";
	public static string HAMMER_IN_BASEMENT = "hammerInBasement";
	public static string CRIB_FIXED = "cribFixedInNursery";
	public static string MASTER_BEDROOM_KEY = "masterBedroomKey";
	public static string MASTER_BEDROOM_OPENED = "masterBedroomOpened";
	public static string MASTER_BATHROOM_OPENED = "masterBathroomOpened";
	public static string NURSERY_CHEST_CODE = "216";
	public static string PAINTING_SAFE_CODE = "1913";
	public static int MAX_CLUE_NURSERY_SCENE = 4;
	public static int PANEL_CLOCK_JIGSAW = 0;
	public static int PANEL_CAR_JIGSAW = 1;
	public static int PANEL_TAP_JIGSAW = 2;
	public static int PANEL_PAINTING_JIGSAW = 3;
	public static int PANEL_HAMMER = 4;
	public static int PANEL_NAILS = 5;
	public static int PANEL_BEDROOM_KEY = 6;
	public static int PANEL_BATHROOM_KEY = 7;
	public static int PANEL_KNIFE = 8;
	public static int PANEL_TAP_KNOB = 9;
	public static int  TOTAL_JIGSAWS = 4;
	public static bool  NURSERY_AUDIO_CLUE_PLAYED = false;
	public static bool  CAR_AUDIO_CLUE_PLAYED = false;
	public static bool  MAIN_DOOR_CLUE = false;
}
