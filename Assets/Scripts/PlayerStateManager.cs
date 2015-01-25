using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateManager : MonoBehaviour {

	static PlayerStateManager _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public PlayerStateManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(PlayerStateManager)) as PlayerStateManager;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("_playerstatemanager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<PlayerStateManager>();
				}
			}
			return _instance;
		}
	}

	public enum ShipLocations {
		Cryochamber,
		Library,
		Greenhouse,
		Medical,
		Barracks,
		Cafeteria,
		Bridge,
		Corridors,
		Unknown
	}


    public Dictionary<ShipLocations,bool> roomsEnabled = new Dictionary<ShipLocations, bool>(7);

    void Awake() {
        roomsEnabled.Add (ShipLocations.Barracks, true);
        roomsEnabled.Add (ShipLocations.Bridge, false);
        roomsEnabled.Add (ShipLocations.Cafeteria, true);
        roomsEnabled.Add (ShipLocations.Corridors, true);
        roomsEnabled.Add (ShipLocations.Cryochamber, true);
        roomsEnabled.Add (ShipLocations.Greenhouse, true);
        roomsEnabled.Add (ShipLocations.Library, true);
        roomsEnabled.Add (ShipLocations.Medical, false);
    }

    public void DisableEnableRoom(ShipLocations location, bool status){
        roomsEnabled[location] = status;
    }

	public enum EnergyState {
		DYING,
		LOW,
		OK,
        SUPER_CHARGE
	}

	public const int MAX_ENERGY = 100;
	public const int MAX_TIME_ELAPSED = 3000;
	public const int REAL_TIME_COUNTDOWN_IN_SECONDS = 600; // 10 mins
	public const int REPLENISH_TIME_COST = 500; 
	public const int STARTING_ENERGY = 25;
	public int playerEnergy = STARTING_ENERGY;
	public int timeElapsed = 0;
	public ShipLocations currentLocation = ShipLocations.Cryochamber;
	public EnergyState energyState = EnergyState.OK;
	public bool isAdmin = false;
	public bool hasUnlockedBridge = false;
	public bool hasUnlockedMedBay = false;
	public bool hasKeyCard = false;

	public int realTimeElapsedInSeconds = 0;
	public float timeStarted = 0.0f;

	public void Tick () {
		timeElapsed++;
		playerEnergy--;
		if (playerEnergy < 0) {
			playerEnergy = 0;
		}
		CheckState();
	}

	void FixedUpdate() {
		if (hasUnlockedBridge) {
			if (timeStarted == 0.0f) {
				timeStarted = Time.fixedTime;
			}
			realTimeElapsedInSeconds = (int)(Time.fixedTime - timeStarted);
		}
	}

	private void CheckState() {
        if (energyState != EnergyState.SUPER_CHARGE) {
            if (playerEnergy < 10 && playerEnergy > 2) {
                energyState = EnergyState.LOW;
            } else if (playerEnergy <= 2) {
                energyState = EnergyState.DYING;
            } else {
                energyState = EnergyState.OK;
            }

            if (playerEnergy <= 0) {
                GameManager.instance.TeleportToMedicalBay ();
            }
        } else {
            playerEnergy = MAX_ENERGY;
        }

		if (timeElapsed >= MAX_TIME_ELAPSED) {
			//ship has reached final destination?
		}
	}

	public void ReplenishFullEnergy (bool higherCost = false) { //Sleeping
		playerEnergy = MAX_ENERGY;
		var multiplier = 1;
		if (higherCost) {
			multiplier = 2;
		}
		timeElapsed += (REPLENISH_TIME_COST * multiplier);
		CheckState();
	}

    public void EnergySuperCharge() {
        playerEnergy = MAX_ENERGY;
        energyState = EnergyState.SUPER_CHARGE;
    }

	public void ReplenishSomeEnergy () { //Eating
		playerEnergy += MAX_ENERGY / 3;
		if (playerEnergy > MAX_ENERGY) {
			playerEnergy = MAX_ENERGY;
		}
		CheckState();
	}



}
