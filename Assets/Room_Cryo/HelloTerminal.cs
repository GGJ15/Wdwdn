using UnityEngine;
using System.Collections;

public class HelloTerminal : MonoBehaviour, ITerminal {

	void Update() {
		if (Input.GetKeyDown ("escape")) {
			gameObject.GetComponent<TerminalManager>().Hide();
			GameManager.instance.EnablePlayerInput();
		}
	}

	void OnEnable(){
		gameObject.GetComponent<TerminalManager>().Show();
		GameManager.instance.DisablePlayerInput();
	}

	public string InitialPromptText() {
		return "Message of the day!"; 
	}

	public string ProcessCommand(string input){
		return "Hello!";
	}
}
