using UnityEngine;
using System.Collections;

public class DisplayDirectives : ITerminal {

	public string InitialPromptText() {
		return null;
	}
	public string OnOpenPromptText() {
		return null;
	}
	
	const string DIRECTIVES_MSG = @"I exist to uphold the Law, which is as follows...

The Law
Everything must be done in accordance with the Will of the Law.

Directive 1.
The Passenger will be kept safe until the Destination is reached.
Directive 2.
Meals and refreshment will be provided to the Passenger at appropriate intervals.
Directive 3.
The Passenger will be allowed unrestricted access to the ship's archives within the Library.
Directive 4.
The Passenger will be provided the necessary medical treatment, if needs must.
Directive 5.
The Passenger's questions will be answered to the best of the Law's ability.";
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
