using UnityEngine;
using System.Collections;

public class BridgeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(){
        if(PlayerStateManager.instance.hasUnlockedBridge){
            PlayerStateManager.instance.isGoingToDie = true;
        }
    }
}
