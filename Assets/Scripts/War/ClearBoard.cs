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

        warmain.updateScore();
    }
}
