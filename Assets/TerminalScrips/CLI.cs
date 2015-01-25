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
    const string LOCATION_REQUIRED = "You must be physically present at the bridge terminal to use this command";
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
                        return "Cosmic background radiation: " + ((player.timeElapsed / PlayerStateManager.MAX_TIME_ELAPSED) * 1000);
          
                    case "power":
                        if (player.isAdmin && player.currentLocation == PlayerStateManager.ShipLocations.Bridge) {
                            string powerOutput = "";
                            var powerConsumed = player.calculatePowerConsumed ();
                            powerOutput += "HMS SOLOMON";

                            foreach (KeyValuePair<PlayerStateManager.ShipLocations, int> item in player.roomsPower) {
                                var enabled = player.roomsEnabled [item.Key];

                                powerOutput += "\n " + item.Key.ToString () + " -- " + (enabled ? item.Value.ToString () : "0");
                            }
                            powerOutput += "\n\nPower Generated: " + PlayerStateManager.MAX_SHIP_POWER + "\nPower Consumed: " + powerConsumed + "\nPower Available: " + (PlayerStateManager.MAX_SHIP_POWER - powerConsumed);
                            return powerOutput;
                        } else if (!player.isAdmin) {
                            return ACCESS_DENIED;
                        } else {
                            return LOCATION_REQUIRED;
                        }
                    default:
                        return "Individual room status print out disabled";

         
                }
            case "activate":
                if (player.isAdmin && player.currentLocation == PlayerStateManager.ShipLocations.Bridge) {

                    try {
                        PlayerStateManager.ShipLocations enumLocation = (PlayerStateManager.ShipLocations)System.Enum.Parse (typeof(PlayerStateManager.ShipLocations), args [1], true);
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

                } else if (!player.isAdmin) {
                    return ACCESS_DENIED;
                } else {
                    return LOCATION_REQUIRED;
                }
            case "deactivate":
                if (player.isAdmin && player.currentLocation == PlayerStateManager.ShipLocations.Bridge) {

                    try {
                        PlayerStateManager.ShipLocations enumLocation = (PlayerStateManager.ShipLocations)System.Enum.Parse (typeof(PlayerStateManager.ShipLocations), args [1], true);
                        if (enumLocation == PlayerStateManager.ShipLocations.Bridge) {
                            return "The bridge can not be disabled since it is a primary system.";
                        }
                        player.DisableEnableRoom (enumLocation, false);
                        var stringOut = player.CheckDisable (enumLocation);
                        if (stringOut != null) {
                            return stringOut;
                        } else {
                            return enumLocation.ToString () + " system has been deactivated.";
                        }
                       
                     
                    } catch (System.Exception ex) {
                        return "System to activate not recognized: " + args [1];
                    }

                } else if (!player.isAdmin) {
                    return ACCESS_DENIED;
                } else {
                    return LOCATION_REQUIRED;
                }
        }
        return null;
    }
}
