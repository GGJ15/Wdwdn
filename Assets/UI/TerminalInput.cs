using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerminalInput : MonoBehaviour {

	public GameObject terminalObject;

	public Text outputText;
	public InputField inputField;

	private ITerminal terminalInterface;

	void Start() {
		terminalInterface = (terminalObject.GetComponent(typeof(ITerminal)) as ITerminal);
		outputText.text += terminalInterface.InitialPromptText();
		inputField.Select();
		inputField.ActivateInputField();
	}
	IEnumerator OnShow_(){
		yield return new WaitForFixedUpdate();
		outputText.text += terminalInterface.OnOpenPromptText();
	}
	IEnumerator ForceActivate_(){
		yield return new WaitForFixedUpdate();
		EventSystem.current.SetSelectedGameObject(inputField.gameObject);
		inputField.Select();
		inputField.ActivateInputField();
		inputField.OnPointerClick(new PointerEventData(EventSystem.current));

		
	}
	public void On_Show () {

		StartCoroutine(OnShow_());
		ForceActivate();
	}

	public void ForceActivate () {
		
		StartCoroutine(ForceActivate_());
	}

	public void On_Hide () {
		inputField.DeactivateInputField();
	}
	
	public void EditText() {
		string input = inputField.text;
		if(input.Length == 0) { return; }
		outputText.text += "\n$ " + input;
		string commandOutput = terminalInterface.ProcessCommand(input);
		outputText.text += "\n" + commandOutput;
		inputField.text = "";
		inputField.Select();
		inputField.ActivateInputField();
	}
}
