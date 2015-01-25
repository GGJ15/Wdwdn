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
    public Transform MedBayTeleportLocation;
	private Transform MainCamera;
	private Player Player;
	private PlayerStateManager PlayerState;
    public GameObject blackCover;

	private CameraEffect currentCameraEffect = CameraEffect.Normal;

    public bool win = false;
    public bool loss = false;
	
	void Start() {
		Player = PlayerGameObject.GetComponent<Player>();
		PlayerState = PlayerStateManager.instance;
		MainCamera = Player.MainCamera;
	}


	void FixedUpdate() {

		if (PlayerState.energyState == PlayerStateManager.EnergyState.OK && currentCameraEffect != CameraEffect.Normal) {
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
		((MonoBehaviour)MainCamera.GetComponent("Vignetting")).enabled = false;
		((MonoBehaviour)MainCamera.GetComponent("Blur")).enabled = false;
		MainCamera.GetComponent<MotionBlur>().enabled = true;
	}

	public void ResetCameraEffects() {
		currentCameraEffect = CameraEffect.Normal;
		((MonoBehaviour)MainCamera.GetComponent("Vignetting")).enabled = false;
		((MonoBehaviour)MainCamera.GetComponent("Blur")).enabled = false;
		MainCamera.GetComponent<MotionBlur>().enabled = false;
	}

	public void DoCameraPassOutEffect() {
		currentCameraEffect = CameraEffect.PassingOut;
		((MonoBehaviour)MainCamera.GetComponent("Vignetting")).enabled = true;
		((MonoBehaviour)MainCamera.GetComponent("Blur")).enabled = true;
		MainCamera.GetComponent<MotionBlur>().enabled = true;
	}

    IEnumerator TeleportedToMedBay() {
        PlayerGameObject.transform.position = MedBayTeleportLocation.transform.position;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(3);
        blackCover.SetActive(false);
        PlayerState.EnergySuperCharge();
        ResetCameraEffects();
        Player.EnableInput();
    }

	public void TeleportToMedicalBay() {
        Player.DisableInput();
        blackCover.SetActive(true);
        StartCoroutine(TeleportedToMedBay());
	}

	public void UnlockBridge(){
        PlayerState.DisableEnableRoom(PlayerStateManager.ShipLocations.Bridge,true);
		PlayerState.hasUnlockedBridge = true;

	}

	public void UnlockMedBay(){
        PlayerState.DisableEnableRoom(PlayerStateManager.ShipLocations.Medical,true);
		PlayerState.hasUnlockedMedBay = true;
	}

	public void Tick() {
		Debug.Log ("Tick!");
		PlayerState.Tick();
	}

	public void Sleep(){
		Player.DisableInput ();
		blackCover.SetActive(true);
		StartCoroutine(Wake());
	}

	IEnumerator Wake(){
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(3);
		blackCover.SetActive(false);
		Player.EnableInput();
	}

    public void PlayDeathScene(){
        loss = true;
    }

    public void PlayWinScene(){
        win = true;
    }

}
