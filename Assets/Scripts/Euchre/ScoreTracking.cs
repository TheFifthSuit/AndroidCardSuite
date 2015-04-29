using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScoreTracking : MonoBehaviour {

    public Text playerScoreText;
    public Text opponentScoreText;
    
    //score that will be added after each person plays a card and checkwinner is called
    public int playerOneWinsBeforeRedeal;
    public int playerTwoWinsBeforeRedeal;
    public int opponentOneWinsBeforeRedeal;
    public int opponentTwoWinsBeforeRedeal;

    //Will be added to total team score
    public int playerTeamScore;
    public int opponentTeamScore;

    public bool playFinished;

    // Use this for initialization
	void Start () 
    {
        playerOneWinsBeforeRedeal = 0;
        playerTwoWinsBeforeRedeal = 0;
        opponentOneWinsBeforeRedeal = 0;
        opponentTwoWinsBeforeRedeal = 0;
        playerTeamScore = 0;
        opponentTeamScore = 0;
        playFinished = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        GameObject manager = GameObject.Find("_Manager");
        EuchreMain emain = manager.GetComponent<EuchreMain>();
        //time to see who won
        if(playFinished == true)
        {
            if ((playerOneWinsBeforeRedeal + playerTwoWinsBeforeRedeal + opponentOneWinsBeforeRedeal + opponentTwoWinsBeforeRedeal) != 0)
            {
                //check for Euchred first
                if (emain.whoMadeTrump == "playerOne" || emain.whoMadeTrump == "playerTwo")
                {
                    if ((playerOneWinsBeforeRedeal + playerTwoWinsBeforeRedeal) < 3)
                    {
                        opponentTeamScore += 2;
                        resetTempScores();
                    }
                }
                else if (emain.whoMadeTrump == "opponentOne" || emain.whoMadeTrump == "opponentTwo")
                {
                    if ((opponentOneWinsBeforeRedeal + opponentTwoWinsBeforeRedeal) < 3)
                    {
                        playerTeamScore += 2;
                        resetTempScores();
                    }
                }
                //5 tricks made 2 points
                if ((playerOneWinsBeforeRedeal + playerTwoWinsBeforeRedeal) == 5)
                {
                    playerTeamScore += 2;
                    resetTempScores();
                }
                else if ((opponentOneWinsBeforeRedeal + opponentTwoWinsBeforeRedeal) == 5)
                {
                    opponentTeamScore += 2;
                    resetTempScores();
                }
                //3 or 4 tricks made 1 point
                else if ((playerOneWinsBeforeRedeal + playerTwoWinsBeforeRedeal) == 3 || (playerOneWinsBeforeRedeal + playerTwoWinsBeforeRedeal) == 4)
                {
                    playerTeamScore += 1;
                    resetTempScores();
                }
                else if ((opponentOneWinsBeforeRedeal + opponentTwoWinsBeforeRedeal) == 3 || (opponentOneWinsBeforeRedeal + opponentTwoWinsBeforeRedeal) == 4)
                {
                    opponentTeamScore += 1;
                    resetTempScores();
                }
            }
        }
	}

    private void resetTempScores()
    {
        GameObject manager = GameObject.Find("_Manager");
        EuchreMain emain = manager.GetComponent<EuchreMain>();
        
        playerOneWinsBeforeRedeal = 0;
        playerTwoWinsBeforeRedeal = 0;
        opponentOneWinsBeforeRedeal = 0;
        opponentTwoWinsBeforeRedeal = 0;
        emain.whoMadeTrump = "";
        playFinished = false;
    }
}
