using UnityEngine;
using System.Collections;

public class ClearHandResults : MonoBehaviour {

    void Start()
    {
        GameObject[] playerOneCard = GameObject.FindGameObjectsWithTag("Card");
		BoxCollider2D playerOneCardBoxCollider = playerOneCard[0].GetComponent<BoxCollider2D>();
		playerOneCardBoxCollider.enabled = false;
    }
    
    void OnMouseDown()
    {
        GameObject manager = GameObject.Find("_Manager");
        ClearBoard clearboard = manager.GetComponent<ClearBoard>();

        clearboard.clearBoard();
    }
}
