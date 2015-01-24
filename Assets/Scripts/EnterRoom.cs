using UnityEngine;
using System.Collections;

public class EnterRoom : MonoBehaviour {

	public PlayerStateManager.ShipLocations location;

	void OnTriggerEnter() {
		PlayerStateManager.instance.currentLocation = location;
	}
}
