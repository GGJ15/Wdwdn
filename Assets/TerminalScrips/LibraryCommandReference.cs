using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LibraryCommandReference : MonoBehaviour {

	void FixedUpdate() {
		if (Input.GetKeyDown ("escape")) {
			gameObject.SetActive(false);
			GameManager.instance.EnablePlayerInput();
		}
	}
	
	void OnEnable(){
		GameManager.instance.DisablePlayerInput();
	}

	private string[] pages = new string[]{
		@"* LIST OF CLI COMMANDS *

!su (password) -- This grants administrative access.
!status (ship/sensors/room name)
 -- !status ship shows list of rooms, activation states for each.
 -- !status sensors shows cosmic background radiation status.
 -- !status (room name) shows the activation state for the room, plus a description.

 -> ex

> !status cryogenics

CRYOGENICS WARD
Status: Online

(etc)

* MORE COMMANDS AND THEIR USAGE ON THE NEXT PAGE *
",@"* BRIDGE-ONLY COMMANDS *

!status power -- as in !status ship, except shows you how much power is available, how much power is being consumed

HMS SOLOMON
Power Generated: 120 Units
Power Consumed: 110 Units
Power Available: 10 Units

Cryogenics -- 10 Units
…
etc.

!activate / !deactivate (room) -- activates/deactivates room. Deactivating takes up time. Activating does not.

!deactivate Cryogenics

Deactivating… 100%

Cryogenics Ward has been deactivated, and access to it has been restricted.
This procedure has freed the use of 10 Power Units.

!activate Cryogenics

Activating… 100%

Power has been allotted

!activate / !deactivate comms -- activates/deactivates communications array

!sos (message) -- only works if the comms are on, sends an emergency broadcast to anyone who can hear it
",
	};

	// Use this for initialization
	void Start () {
		PaginateForward();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Text outputText;
	private int currentPage = -1;

	public void PaginateForward() {
		currentPage++;
		if (currentPage > (pages.Length-1)) {
			currentPage = pages.Length -1;
			return;
		} else {
			outputText.text = pages[currentPage];
		}
		GameManager.instance.Tick();
	}

	public void PaginateBackward() {
		currentPage--;
		if (currentPage < 0) {
			currentPage = 0;
			return;
		} else {
			outputText.text = pages[currentPage];
		}
		GameManager.instance.Tick();
	}
}
