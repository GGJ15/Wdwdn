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


	private enum CameraEffect {
		Normal,
		Dizzy,
		PassingOut
	}

	public GameObject PlayerGameObject;
	private Camera MainCamera;
	private Player Player;
	private PlayerStateManager PlayerState;

	private CameraEffect currentCameraEffect = CameraEffect.Normal;

	
	void Start() {
		Player = PlayerGameObject.GetComponent<Player>();
		PlayerState = PlayerStateManager.instance;
		MainCamera = Player.MainCamera;
	}



	void FixedUpdate() {
		if(PlayerState.energyState == PlayerStateManager.EnergyState.OK && currentCameraEffect != CameraEffect.Normal){
			ResetCameraEffects();
		}
		if (PlayerState.energyState == PlayerStateManager.EnergyState.LOW && currentCameraEffect != CameraEffect.Dizzy) {
			SetCameraDizzyEffect();
		}
		if (PlayerState.energyState == PlayerStateManager.EnergyState.DYING && currentCameraEffect != CameraEffect.PassingOut) {
			DoCameraPassOutEffect();
		}
	}

	public void DisablePlayerInput(){
		Screen.lockCursor = false;
		Player.DisableInput();
	}

	public void EnablePlayerInput(){
		Screen.lockCursor = true;
		Player.EnableInput();
	}

	public void SetCameraDizzyEffect() {
		currentCameraEffect = CameraEffect.Dizzy;
	}

	public void ResetCameraEffects() {
		currentCameraEffect = CameraEffect.Normal;
	}

	public void DoCameraPassOutEffect() {
		currentCameraEffect = CameraEffect.PassingOut;
	}

	public void TeleportToMedicalBay() {
	}


}
