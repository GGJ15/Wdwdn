using UnityEngine;
using System.Collections;

public class BedSleep : MonoBehaviour, IInteractable {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnInteract() {
		PlayerStateManager.instance.ReplenishFullEnergy();
		GameManager.instance.Sleep();
	}
}
