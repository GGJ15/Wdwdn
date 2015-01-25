using UnityEngine;
using System.Collections;

public class BridgeDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
        if (PlayerStateManager.instance.hasUnlockedBridge && !gameObject.GetComponent<Door>().isOpen) {
            gameObject.GetComponent<Door>().Switch();
        }
	}
}
