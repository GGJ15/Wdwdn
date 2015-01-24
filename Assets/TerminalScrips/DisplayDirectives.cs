using UnityEngine;
using System.Collections;

public class DisplayDirectives : ITerminal {

	public string InitialPromptText() {
		return null;
	}
	public string OnOpenPromptText() {
		return null;
	}
	
	const string DIRECTIVES_MSG = @"I exist to uphold the Law, which is as follows...";
	private string[] low_keywords = new string[] {"what","do","now"," i ","we","are","you", "who"};
	private string[] hot_keywords = new string[] {"directive","law","help"};
	public string ProcessCommand(string input){
		var low_score = 0;
		var hot_score = 0;
		foreach(string str in hot_keywords){
			if(input.ToLower().Contains(str)){
				hot_score++;
			}
		}
		foreach(string str in low_keywords){
			if(input.ToLower().Contains(str)){
				low_score++;
			}
		}
		if(hot_score>0){
			return DIRECTIVES_MSG; 
		} else if(low_score>2){
			return DIRECTIVES_MSG;
		} else {
			return null;
		}

	}
}
