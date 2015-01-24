using UnityEngine;
using System.Collections;

public interface ITerminal  {
	string InitialPromptText();
	string ProcessCommand(string input);
}
