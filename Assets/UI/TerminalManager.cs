using UnityEngine;
using System.Collections;

public class TerminalManager : MonoBehaviour {


	public GameObject terminalInput; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show_() {
		gameObject.SetActive(true);
		terminalInput.GetComponent<TerminalInput>().On_Show();
	}

	public void Hide_() {
		gameObject.SetActive(false);
		terminalInput.GetComponent<TerminalInput>().On_Hide();
	}
}
