using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour
{

		void Start ()
		{
				Screen.lockCursor = true;
		}

		void DidLockCursor ()
		{
				Debug.Log ("Locking cursor");

		}

		void DidUnlockCursor ()
		{
				Debug.Log ("Unlocking cursor");

		}

		void OnMouseDown ()
		{
				Screen.lockCursor = true;
		}

		private bool wasLocked = false;

		void Update ()
		{
				if (Input.GetKeyDown ("escape"))
						Screen.lockCursor = false;
		
				if (!Screen.lockCursor && wasLocked) {
						wasLocked = false;
						DidUnlockCursor ();
				} else
		if (Screen.lockCursor && !wasLocked) {
						wasLocked = true;
						DidLockCursor ();
				}
		}

		void OnApplicationFocus ()
		{
				if (!Screen.lockCursor) {
						Screen.lockCursor = true;
				}
		}
}
