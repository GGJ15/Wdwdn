using UnityEngine;
using System.Collections;

public class HelloTerminal : MonoBehaviour, ITerminal {

	void Update() {
		if (Input.GetKeyDown ("escape")) {
			gameObject.SetActive(false);
		}
	}

	public string InitialPromptText() {
		return "Message of the day!"; 
	}

	public string ProcessCommand(string input){
		return "Hello!";
	}
}
