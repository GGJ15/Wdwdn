using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform MainCamera;


	public float hitRadius = 1f;
	public float hitDistace = 0.6f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Action")) {
			PerformAction();
		}
	}

	void PerformAction() {
		Transform cam = MainCamera.transform;
		var ray = new Ray(cam.position, cam.forward);
		
		RaycastHit hit;
		var radius = hitRadius;
		var distance = hitDistace;

		if (Physics.SphereCast(ray, radius, out hit, distance, 1 << LayerMask.NameToLayer("Interactables"))) {
			IInteractable interactable = hit.collider.gameObject.GetComponent(typeof(IInteractable)) as IInteractable;
			interactable.OnInteract();
		}
	}

	public void DisableInput() {
		((MonoBehaviour)GetComponent("CharacterMotor")).enabled = false;
		GetComponent<SmoothMouseLook>().enabled = false;
		MainCamera.GetComponent<SmoothMouseLook>().enabled = false;
	}

	public void EnableInput() {
		((MonoBehaviour)GetComponent("CharacterMotor")).enabled = true;
		GetComponent<SmoothMouseLook>().enabled = true;
		MainCamera.GetComponent<SmoothMouseLook>().enabled = true;
	}
}
