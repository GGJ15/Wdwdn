using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLI : ITerminal {

    public string InitialPromptText () {
        return null;
    }

    public string OnOpenPromptText () {
        return null;
    }
    
    const string ACCESS_DENIED = @"Access denied. You must be a privilidged user to use this command.";
    const string ADMIN_PASSWORD = "20711008";

    public string ProcessCommand (string input) {
        PlayerStateManager player = PlayerStateManager.instance;
        if (!input.StartsWith ("!")) {
            return null;
        }
        input = input.Remove (0, 1).ToLower ();
        string[] args = input.Split (' ');
        switch (args [0]) {
            case "su":
                if (args [1] != ADMIN_PASSWORD) {
                    return "Incorrect password. Password hint: birthday";
                } else {
                    PlayerStateManager.instance.isAdmin = true;
                    return "Administrative access granted.";
                }
      
            case "status":
                switch (args [1]) {
                    case "ship":
                        string shipOutput = "HMS SOLOMON";
                        foreach (KeyValuePair<PlayerStateManager.ShipLocations, int> item in player.roomsPower) {
                            var enabled = player.roomsEnabled [item.Key];
                            shipOutput += "\n " + item.Key.ToString () + " -- " + (enabled ? "ON" : "OFF");
                        }
                        return shipOutput;
                    case "sensors":
                        return "Cosmic background radiation: " + ((player.timeElapsed / PlayerStateManager.MAX_TIME_ELAPSED)*1000);
          
                    case "power":
                        if (player.isAdmin) {
                            string powerOutput = "";
                            var powerConsumed = player.calculatePowerConsumed ();
                            powerOutput += "HMS SOLOMON";

                            foreach (KeyValuePair<PlayerStateManager.ShipLocations, int> item in player.roomsPower) {
                                var enabled = player.roomsEnabled [item.Key];

                                powerOutput += "\n " + item.Key.ToString () + " -- " + (enabled ? item.Value.ToString () : "0");
                            }
                            powerOutput += "\n\nPower Generated: " + PlayerStateManager.MAX_SHIP_POWER + "\nPower Consumed: " + powerConsumed + "\nPower Available: " + (PlayerStateManager.MAX_SHIP_POWER - powerConsumed);
                            return powerOutput;
                        } else {
                            return ACCESS_DENIED;
                        }
                    default:
                        return "Individual room status print out disabled";

         
                }
            case "activate":
                switch (args [1]) {
                    case "comms":
                        return null;
                    default:
                        try {
                            PlayerStateManager.ShipLocations enumLocation = (PlayerStateManager.ShipLocations) System.Enum.Parse (typeof(PlayerStateManager.ShipLocations), args [1],true);
                            var check = player.CheckPowerRequirements (enumLocation);
                            if (check != -1) {
                                player.DisableEnableRoom (enumLocation, true);
                            } else {
                                return "Not enough power to activate this system!";
                            }
                            return check + " units of power allocated for " + enumLocation.ToString ();
                        } catch (System.Exception ex) {
                            return "System to activate not recognized: " + args [1];
                        }
                }
        }
        return null;
    }
}
