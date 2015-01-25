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
        CommsArray,
        Engines,
		Unknown
	}


    public Dictionary<ShipLocations,bool> roomsEnabled = new Dictionary<ShipLocations, bool>(7);
    public Dictionary<ShipLocations,int> roomsPower = new Dictionary<ShipLocations, int>(7);

    void Awake() {
        roomsEnabled.Add (ShipLocations.Barracks, true); 
        roomsEnabled.Add (ShipLocations.Bridge, false); 
        roomsEnabled.Add (ShipLocations.Cafeteria, true); 
        roomsEnabled.Add (ShipLocations.CommsArray, false);
        roomsEnabled.Add (ShipLocations.Cryochamber, true); 
        roomsEnabled.Add (ShipLocations.Greenhouse, true); 
        roomsEnabled.Add (ShipLocations.Library, true); 
        roomsEnabled.Add (ShipLocations.Medical, false);
        roomsEnabled.Add (ShipLocations.Engines, false);

        roomsPower.Add (ShipLocations.Barracks, 5); 
        roomsPower.Add (ShipLocations.Bridge, 30); 
        roomsPower.Add (ShipLocations.Cafeteria, 10); 
        roomsPower.Add (ShipLocations.CommsArray, 70); 
        roomsPower.Add (ShipLocations.Engines, 200); 
        roomsPower.Add (ShipLocations.Cryochamber, 5); 
        roomsPower.Add (ShipLocations.Greenhouse, 30); 
        roomsPower.Add (ShipLocations.Library, 20); 
        roomsPower.Add (ShipLocations.Medical, 15); 

    }

    public int calculatePowerConsumed(){
        var power = 0;
        foreach(KeyValuePair<ShipLocations, int> item in roomsPower){
            if(roomsEnabled[item.Key]){
                power += item.Value;
            }
        }
        return power;
    }
    
    public void DisableEnableRoom(ShipLocations location, bool status){
        roomsEnabled[location] = status;
    }

    public int CheckPowerRequirements (ShipLocations location) {
        if ((roomsPower [location] + calculatePowerConsumed ()) > MAX_SHIP_POWER) {
            return -1;
        }
        return roomsPower [location];
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
	public const int STARTING_ENERGY = 35;

    public const int MAX_SHIP_POWER = 120;
    public int shipPowerConsumed = 110;
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
            if (playerEnergy < 15 && playerEnergy > 3) {
                energyState = EnergyState.LOW;
            } else if (playerEnergy <= 3) {
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
        ReplenishFullEnergy(true);
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
