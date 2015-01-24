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

	public void Show() {
		gameObject.SetActive(true);
		terminalInput.GetComponent<TerminalInput>().OnShow();
	}

	public void Hide() {
		gameObject.SetActive(false);
		terminalInput.GetComponent<TerminalInput>().OnHide();
	}
}
