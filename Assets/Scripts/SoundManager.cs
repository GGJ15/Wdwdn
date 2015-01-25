using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	static SoundManager _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public SoundManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(SoundManager)) as SoundManager;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("_soundmanager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<SoundManager>();
				}
			}
			return _instance;
		}
	}

	public AudioClip[] clips;
	public AudioSource source1;
	public AudioSource source2;

	public AudioSource footstepSource;
    public AudioSource terminalOn;
    public AudioSource terminalOff;

	
	public PlayerStateManager.ShipLocations lastPlayedBgmForLocation = PlayerStateManager.ShipLocations.Unknown;
	void FixedUpdate() {
		if(PlayerStateManager.instance.currentLocation != lastPlayedBgmForLocation){
			source2.clip = bgmForLocation(PlayerStateManager.instance.currentLocation);
			source2.volume = 0;
			source2.Play();
			source2.loop = true;
			StartCoroutine(Crossfade(source1,source2,2.5f));
			var sourceTemp = source2;
			source2 = source1;
			source1 = sourceTemp;
			lastPlayedBgmForLocation = PlayerStateManager.instance.currentLocation;
		}
	}

	public AudioClip bgmForLocation(PlayerStateManager.ShipLocations location) {
		switch(location){
		case PlayerStateManager.ShipLocations.Cafeteria:
			return clips[0];
		case PlayerStateManager.ShipLocations.Garden:
			return clips[1];
		case PlayerStateManager.ShipLocations.Bridge:
			return clips[2];
		case PlayerStateManager.ShipLocations.Corridors:
			return clips[3];
		case PlayerStateManager.ShipLocations.Library:
			return clips[4];
		case PlayerStateManager.ShipLocations.Medical:
			return clips[5];
		case PlayerStateManager.ShipLocations.Barracks:
			return clips[6];
		case PlayerStateManager.ShipLocations.Cryochamber:
			return clips[7];
		default:
			return null;
		}
	}

	IEnumerator Crossfade ( AudioSource a1, AudioSource a2, float duration ) {
		var startTime = Time.time; var endTime = startTime + duration;
		
		while (Time.time < endTime) {
			
			var i = (Time.time - startTime) / duration;
			
			a1.volume = (1-i) * 0.5f;
			a2.volume = i * 0.5f;
			
			yield return null;
			
		}
	}
	
	public void PlayFootSteps() {
		if (!footstepSource.isPlaying) {
			footstepSource.Play();
		}
	}

	public void StopPlayingFootSteps(){
		footstepSource.Stop();
	}
	
    public void TerminalOn(){
        terminalOn.Play();
    }

    
    public void TerminalOff(){
        terminalOff.Play();
    }
}
