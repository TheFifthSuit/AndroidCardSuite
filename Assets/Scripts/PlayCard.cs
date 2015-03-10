using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {

    public GameObject winMessage;
    public GameObject loseMessage;
    
    void OnMouseDown()
    {
        GameObject manager = GameObject.Find("_Manager");
        WarMain warmain = manager.GetComponent<WarMain>();

        warmain.cardToScreenOpponent();
        warmain.processturn();

        if(warmain.lastHandWinner == "PlayerOne")
            Instantiate(winMessage, new Vector3(0, 0, 0), Quaternion.identity);
        if (warmain.lastHandWinner == "Opponent")
            Instantiate(loseMessage, new Vector3(0, 0, 0), Quaternion.identity);

        GameObject.Destroy(this.gameObject);
    }
}
