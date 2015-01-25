using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

		public Text textToUpdate;

		// Update is called once per frame
		void FixedUpdate ()
		{
				textToUpdate.text = PlayerStateManager.instance.playerEnergy + "/" + PlayerStateManager.MAX_ENERGY;
		}
}
