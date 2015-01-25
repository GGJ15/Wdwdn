using UnityEngine;
using System.Collections;

public class DisplayDirectives : ITerminal {

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
        string[] args = input.Split (" ");
        switch (args [0]) {
        case "su":
            if (args [1] != ADMIN_PASSWORD) {
                return "Incorrect password. Password hint: birthday";
            } else {
                PlayerStateManager.instance.isAdmin = true;
                return "Administrative access granted.";
            }
            break;
        case "status":
            switch (args [1]) {
            case "ship":

                break;
            case "sensors":
                return "Cosmic background radiation: ";
                    break;

            default:
                return "Individual room status print out disabled";

                break;
            }
            break;
        }

    }
}
