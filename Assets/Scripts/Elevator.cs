using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	public Transform target;
	public float smooth = 5.0F;
	
	
	private bool startedSwitch = false;
	
	IEnumerator Switch() {
		yield return new WaitForSeconds(2);
		var tmp_startMarker = startMarker;
		startMarker = endMarker;
		endMarker = tmp_startMarker;
		Start ();
		startedSwitch = false;
	}
	
	void Start(){
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}
	
	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
		if(fracJourney > 1.0 && !startedSwitch){
			StartCoroutine(Switch());
			startedSwitch = true;
		}
	}
}
