﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Transform MainCamera;


	public float hitRadius = 1f;
	public float hitDistace = 0.6f;



    private bool disableInput = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Action") && !disableInput) {
			PerformAction();
		}
	}

	private int framesSinceLastTick = 0;

	void FixedUpdate() {
		var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if (directionVector != Vector3.zero) {
			var directionLength = directionVector.magnitude;
			if (directionLength> 0.5f) {
				SoundManager.instance.PlayFootSteps();
			} else {
				SoundManager.instance.StopPlayingFootSteps();
			}
			if(directionLength == 1){
				framesSinceLastTick++;
				if(framesSinceLastTick > 150){
					framesSinceLastTick = 0;
					GameManager.instance.Tick();
				}
			}
		} else {
			SoundManager.instance.StopPlayingFootSteps();
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
			GameManager.instance.Tick();
		}
	}

	public void DisableInput() {
        disableInput = true;
		((MonoBehaviour)GetComponent("CharacterMotor")).enabled = false;
		GetComponent<SmoothMouseLook>().enabled = false;
		MainCamera.GetComponent<SmoothMouseLook>().enabled = false;
	}

	public void EnableInput() {
        disableInput = false;
		((MonoBehaviour)GetComponent("CharacterMotor")).enabled = true;
		GetComponent<SmoothMouseLook>().enabled = true;
		MainCamera.GetComponent<SmoothMouseLook>().enabled = true;
	}
}
