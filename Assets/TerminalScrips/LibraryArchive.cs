using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LibraryArchive : MonoBehaviour {

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
		@"!!! NOT ENOUGH POWER TO DISPLAY FULL INTERFACE !!!
Archive raw data dump loading...100%

Dr. Douglas Finch, Ph.D. - father of Deterministic AI
2015/04/11 - 2082/09/01

Prof. Alicia Wright, M.Sc. Ph.D, etc. - leading researcher in Machine Ethics
b. 2027/11/11 - 2090/05/31

Takeshi Hanafumi - architect, HMS King Solomon
b. 2041/12/25

HMS King Solomon list of passengers:

Yuk Elsey, b. 2016-01-23
Armida Eddy, b. 2017-12-04
Dion Browne, b. 2022-01-27
Taryn Kushner, b. 2024-11-06
Damien Wessels, b. 2024-12-05
Hilary Bains, b. 2024-12-22
Bonnie Elwood, b. 2025-07-19
Jordan Murawski, b. 2026-09-06
Armanda Westerberg, b. 2032-09-24
Jeannetta Chilcote, b. 2033-05-24
Alejandra Blasi, b. 2033-07-14
Maragret Hout, b. 2035-05-12
Refugia Gaston, b. 2044-09-15
Bella Dowe, b. 2045-11-10
Charis Mclelland, b. 2047-02-07
Janine Ekhoff, b. 2049-06-01
Lezlie Pridgen, b. 2052-12-24
Jannie Mathieson, b. 2053-11-20
Travis Gambrell, b. 2056-09-09
Laverna Hyche, b. 2059-05-06
Sean Levis, b. 2063-11-28
Carson Elem, b. 2068-02-10
Tynisha Jepson, b. 2069-02-04
Sherrill Mcphillips, b. 2073-03-21
Laquanda Moncrief, b. 2075-09-18
Deloise Daubert, b. 2057-11-15
Cornell Arizmendi, b. 2058-07-05
Rosella Anglin, b. 2059-10-24
Lecia Graney, b. 2060-04-18
Julie Carneal, b. 2062-12-15
",@"In the year 2048, human technology has advanced to the point where silicon-based chips have finally reached their fundamental physical limits. Due to the relative inaccessibility of quantum and molecular computing thanks to declining confidence in technology companies. Technological advancement and ingenuity begins to stagnate.",@"In the year 2051, large-scale riots across the globe cause governments to fear for the end of human civilization.   

In the year 2055, the first Deterministic Pseudo-Sentient Artificial Intelligence was developed at the Human-Computer Interaction Institute in the United States. This was a stopgap measure in case the Luddite Riots would overthrow governmental order. By programming an AI to enforce a set of deterministic rules set by an administrator, natural order could theoretically be preserved in the event of loss of government.",@"In the year 2071, on October 8, NATO funds Project Hammurabi, a system by which micro-computers injected into a person's bloodstream would be able to dictate and govern their bodily functions as directed by a DPSAI. Dr. Douglas Finch, former director of the HCI Institute, is designated project lead, with leading Machine Ethicist, Professor Alicia Wright, as consultant.",@"[CLASSIFIED] Administrator access required.",@"The Hammurabi prototype is cleared for field testing aboard the luxury starship, the HMS King Solomon, with its maiden voyage in 15 years.

In the year 2086, March 1, the HMS King Solomon departs on its maiden voyage as humanity's first space tourism vessel."
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
