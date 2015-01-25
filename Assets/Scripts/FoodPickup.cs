using UnityEngine;
using System.Collections;

public class FoodPickup : MonoBehaviour, IInteractable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnInteract() {
		gameObject.SetActive(false);
		PlayerStateManager.instance.ReplenishSomeEnergy();
	}
}
