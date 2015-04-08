using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {

    public GameObject winMessage;
    public GameObject loseMessage;
    
    public void playcard()
    {
        GameObject manager = GameObject.Find("_Manager");
        WarMain warmain = manager.GetComponent<WarMain>();

        warmain.cardToScreenOpponent(0, 300);
        warmain.processturn();

        if(warmain.lastHandWinner == "PlayerOne")
            Instantiate(winMessage, new Vector3(0, 0, 0), Quaternion.identity);
        if (warmain.lastHandWinner == "Opponent")
            Instantiate(loseMessage, new Vector3(0, 0, 0), Quaternion.identity);

		warmain.removePlayButton ();
    }
}
