using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TerminalInput : MonoBehaviour {

	public GameObject terminalObject;

	public Text outputText;
	public InputField inputField;

	private ITerminal terminalInterface;

	void Start() {
		terminalInterface = (terminalObject.GetComponent(typeof(ITerminal)) as ITerminal);
		outputText.text += terminalInterface.InitialPromptText();
	}

	public void EditText() {
		string input = inputField.text;
		outputText.text += "\n$ " + input;
		string commandOutput = terminalInterface.ProcessCommand(input);
		outputText.text += "\n" + commandOutput;
		inputField.text = "";

		inputField.ActivateInputField();
	}
}
