using UnityEngine;
using System.Collections;

public class NextPlay : MonoBehaviour {


    void OnMouseDown()
    {
        GameObject manager = GameObject.Find("_Manager");
        EuchreMain emain = manager.GetComponent<EuchreMain>();
        ScoreTracking score = manager.GetComponent<ScoreTracking>();

        if (score.playFinished == true)
        {
            score.playFinished = false;
            print("NEXT DEALER TIME");
            emain.clearBoardNoReplaceKitty();
            emain.nextDeal();
        }
        else
        {
            emain.clearBoardNoReplaceKitty();
        }
    }

}
