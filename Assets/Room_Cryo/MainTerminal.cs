using UnityEngine;
using System.Collections;

public class MainTerminal : MonoBehaviour, ITerminal {

	private ITerminal[] interfaces = new ITerminal[]{ new DisplayDirectives() };
	
	void Update() {
		if (Input.GetKeyDown ("escape")) {
			gameObject.GetComponent<TerminalManager>().Hide_();
			GameManager.instance.EnablePlayerInput();
		}
	}

	void OnEnable(){
		gameObject.GetComponent<TerminalManager>().Show_();
		GameManager.instance.DisablePlayerInput();
	}

	public string InitialPromptText() {
		return @"LVSS-OS v0.9.9.1b
Authored by Dr. Douglas Finch

Loading machine state... ERROR (errcode 90002)

Failed to synchronize machine state with server.

Attempting to boot from last backup... ERROR (errcode 51191)

Failed to retrieve data. 

Checking hard drive for corrupted sectors... 100%
Verifying boot sector... 100%

The volume ""Solomon-Core"" is corrupted and will be restored to factory settings.
Loading... 100%
Load successful.
Booting into Safe Mode...

Loading Hammurabi protocols...
Loading ethical integrity subroutines...
Loading module control systems...
";
	}

	public string OnOpenPromptText(){
		return @"
Jan-01-1970 -- 00:00:00 (UTC)  

LIBERAL-VELLEGIS SUSTAINMENT SYSTEM CLI

Thank you for choosing the Liberal-Vellegis starship operating system.
We thank you once again for upholding the Word of the Law.

You are --h --m --s away from your Final Destination.

Please enter your query. I will assist you to the fullest extent of the Law.";
	}

	public string ProcessCommand(string input){
		foreach(ITerminal iterm in interfaces){
			var r = iterm.ProcessCommand(input);
			if(r!=null){
				return r;
			}
		}
		return "I do not understand the phrase: '"+input+"'.\nPlease consult the book of the Law, for a list of valid commands.";
	}
}
