using UnityEngine;
using System.Collections;

public class ClearBoard : MonoBehaviour {

    public void clearBoard()
    {
        GameObject[] cardclones = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject clone in cardclones)
        {
            Destroy(clone);
        }

        GameObject[] tempobjectclones = GameObject.FindGameObjectsWithTag("tempobject");

        foreach (GameObject clone in tempobjectclones)
        {
            Destroy(clone);
        }

        GameObject manager = GameObject.Find("_Manager");
        WarMain warmain = manager.GetComponent<WarMain>();

        if (Application.loadedLevelName == "War")
        {
            warmain.updateScore();
        }
        else if (Application.loadedLevelName == "Blackjack")
        {
            GameObject manager1 = GameObject.Find("_Manager");
            BlackjackMain bjmain = manager1.GetComponent<BlackjackMain>();
            bjmain.nextHand();
        }
    }
}
