using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

		public Text textToUpdate;
		public Text countDownText;
        public Text warningText;
        public GameObject endpanel;
        public GameObject win;
        public GameObject loss;

		// Update is called once per frame
		void FixedUpdate () {
			textToUpdate.text = PlayerStateManager.instance.playerEnergy + "/" + PlayerStateManager.MAX_ENERGY;
			if (PlayerStateManager.instance.hasUnlockedBridge) {
				countDownText.gameObject.SetActive(true);
				var timeInSeconds = PlayerStateManager.REAL_TIME_COUNTDOWN_IN_SECONDS - PlayerStateManager.instance.realTimeElapsedInSeconds;
				var minutesString = Mathf.FloorToInt((timeInSeconds / 60)).ToString("00");
			var secondsString = Mathf.FloorToInt(timeInSeconds % 60).ToString("00");

            countDownText.text = minutesString + ":" + secondsString;

            if(GameManager.instance.win){
                endpanel.SetActive(true);
                win.SetActive(true);
            }
            if(GameManager.instance.loss){
                endpanel.SetActive(true);
                loss.SetActive(true);
            }
        }
        if (PlayerStateManager.instance.energyState == PlayerStateManager.EnergyState.LOW || PlayerStateManager.instance.energyState == PlayerStateManager.EnergyState.DYING) {
            warningText.gameObject.SetActive (true);
        } else {
            warningText.gameObject.SetActive (false);
        }
    }
}
