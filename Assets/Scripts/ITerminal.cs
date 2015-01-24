using UnityEngine;
using System.Collections;

public interface ITerminal  {
	string InitialPromptText();
	string OnOpenPromptText();
	string ProcessCommand(string input);
}
