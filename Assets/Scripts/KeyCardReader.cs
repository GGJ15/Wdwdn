using UnityEngine;
using System.Collections;

public class KeyCardReader : MonoBehaviour, IInteractable {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void OnInteract () {
        if (PlayerStateManager.instance.hasKeyCard && PlayerStateManager.instance.isAdmin) {
            GameManager.instance.UnlockBridge ();
        }
        else{

        }
    }
}