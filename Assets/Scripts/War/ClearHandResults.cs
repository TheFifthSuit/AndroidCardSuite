using UnityEngine;
using System.Collections;

public class ClearHandResults : MonoBehaviour {

    void OnMouseDown()
    {
        GameObject manager = GameObject.Find("_Manager");
        ClearBoard clearboard = manager.GetComponent<ClearBoard>();

        clearboard.clearBoard();
    }
}
