using UnityEngine;
using System.Collections;

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
		Corridors
	}

	public enum EnergyState {
		DYING,
		LOW,
		OK
	}

	const int MAX_ENERGY = 200;
	const int MAX_TIME_ELAPSED = 3000;
	const int REPLENISH_TIME_COST = 500; 

	private int playerEnergy = MAX_ENERGY;
	private int timeElapsed = 0;
	public ShipLocations currentLocation = ShipLocations.Cryochamber;
	public EnergyState energyState = EnergyState.OK;
	public bool isAdmin = false;

	public void Tick () {
		timeElapsed++;
		playerEnergy--;
		CheckState();
	}

	private void CheckState() {
		if (playerEnergy < 25 && playerEnergy > 0) {
			energyState = EnergyState.LOW;
		} else if (playerEnergy <= 0) {
			energyState = EnergyState.DYING;
		} else {
			energyState = EnergyState.OK;
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

	public void ReplenishSomeEnergy () { //Eating
		playerEnergy += MAX_ENERGY / 3;
		if (playerEnergy > MAX_ENERGY) {
			playerEnergy = MAX_ENERGY;
		}
		CheckState();
	}



}
