using UnityEngine;
using System.Collections;

public class NextPlay : MonoBehaviour {


    void OnMouseDown()
    {
        GameObject manager = GameObject.Find("_Manager");
        EuchreMain emain = manager.GetComponent<EuchreMain>();


        emain.clearBoardNoReplaceKitty();
    }

}
