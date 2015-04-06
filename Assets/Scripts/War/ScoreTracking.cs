using UnityEngine;
using System.Collections;

public class ScoreTracking : MonoBehaviour {

    public GUIText playerScoreText;
    public GUIText opponentScoreText;

    

    private void updateScore()
    {
        GameObject manager = GameObject.Find("_Manager");
        WarMain warmain = manager.GetComponent<WarMain>();
    }

}
