using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	static GameManager _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("_gamemanager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<GameManager>();
				}
			}
			return _instance;
		}
	}

	public GameObject PlayerObject;

	public void DisablePlayerInput(){
		Screen.lockCursor = false;
		PlayerObject.GetComponent<Player>().DisableInput();
	}
	public void EnablePlayerInput(){
		Screen.lockCursor = true;
		PlayerObject.GetComponent<Player>().EnableInput();
	}
}
