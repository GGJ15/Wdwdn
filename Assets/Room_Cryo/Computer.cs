using UnityEngine;
using System.Collections;

public class Computer : MonoBehaviour, IInteractable {

	public GameObject terminalObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnInteract() {
		terminalObject.SetActive(true);
	}
}
