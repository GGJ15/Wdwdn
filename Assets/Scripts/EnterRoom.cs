using UnityEngine;
using System.Collections;

public class EnterRoom : MonoBehaviour {

	// Use this for initialization
	void OnTriggerExit() {
		if (PlayerStateManager.instance.currentLocation == PlayerStateManager.ShipLocations.Cryochamber) {
			PlayerStateManager.instance.currentLocation = PlayerStateManager.ShipLocations.Corridors;
		} else {
			PlayerStateManager.instance.currentLocation = PlayerStateManager.ShipLocations.Cryochamber;
		}
	}
}
