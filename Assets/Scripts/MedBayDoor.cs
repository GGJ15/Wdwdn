using UnityEngine;
using System.Collections;

public class MedBayDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
        if (PlayerStateManager.instance.hasUnlockedMedBay && !gameObject.GetComponent<Door>().isOpen) {
            gameObject.GetComponent<Door>().Switch();
        }
	}
}
