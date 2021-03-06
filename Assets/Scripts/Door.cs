﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	public Transform target;
	public float smooth = 5.0F;


	public bool isOpen = false;

    public void Switch(){
        var tmp_startMarker = startMarker;
        startMarker = endMarker;
        endMarker = tmp_startMarker;
        Start ();
        isOpen = !isOpen;
        var audio = gameObject.GetComponent<AudioSource>();
        if(audio != null){
            audio.Play();
        }
    }

	void Start(){
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}
	
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
	}
}
