using UnityEngine;
using System.Collections;

public class DoorAdrresses : MonoBehaviour {

	public GameObject player;
	private Vector3 nurseryHallwayPosition = new Vector3(4.79f,6.97f,-15.53f); //player nursery position
	private Vector3 kitchenHallwayPosition = new Vector3(4.95f,4.35f,-0.4f); //player kitchen position
	private Vector3 guestBathHallwayPosition = new Vector3(5.12f,4.3f,-13.02f); //player guest bathroom position
	private Vector3 livingRoomHallwayPosition = new Vector3(5.16f,4.28f,-17.18f); //player living room position
	private Vector3 masterBathHallwayPosition = new Vector3(5.32f,6.89f,-0.97f); //player master bathroom position
	private Vector3 masterBedrooomPosition = new Vector3(5.16f,6.81f,-11.55f); //player master bedroom position

	void Start () {
		if (LeaveNursery.leaveNursery == true) { //if player left the nursery
			player.transform.localPosition = nurseryHallwayPosition; //set the position of the player
			LeaveNursery.leaveNursery = false; //set leave nursery to false
		}
		else if (LeaveGuestBathroom.leaveGuestBathroom == true) {  //if player left the guest bathroom
			player.transform.localPosition = guestBathHallwayPosition; //set the position of the player
			LeaveGuestBathroom.leaveGuestBathroom = false; //set leave guest bathroom to false
		}
		else if (LeaveLivingRoom.leaveLivingroom == true) {  //if player left the living room
			player.transform.localPosition = livingRoomHallwayPosition; //set the position of the player
			LeaveLivingRoom.leaveLivingroom = false; //set leave living room to false
		}
		else if (LeaveMasterBathroom.leaveMasterBathroom == true) {  //if player left the master bedroom
			player.transform.localPosition = masterBathHallwayPosition; //set the position of the player
			LeaveMasterBathroom.leaveMasterBathroom = false; //set leave master bedroom to false
		}
		else if (LeaveKitchen.leaveKitchen == true) {  //if player left the kitchen
			player.transform.localPosition = kitchenHallwayPosition; //set the position of the player
			LeaveKitchen.leaveKitchen = false; //set leave kitchen to false
		}
		else if(LeaveBedroom.leaveMasterBedroom == true) {  //if player left the master bedroom
			player.transform.localPosition = masterBedrooomPosition; //set the position of the player
			LeaveBedroom.leaveMasterBedroom = false; //set leave master bedroom to false
		}
	}
}
