using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WarMain : MonoBehaviour {


    //variables used in functions
    string lastCardPlayedPlayerOne;
    string lastCardPlayedOpponent;
    public Text playerScoreText;
    public Text opponentScoreText;
    public string lastHandWinner = "";
	public Button playButton;
	public Text playButtonText;
    public GameObject objectToAnimate;
    public List<GameObject> objectsOnScreen = new List<GameObject>();


    
    //card names within deck should match Prefab names should have seperate prefab for all 52 cards
	string[] cardDeck = new string[] {"twoH", "threeH", "fourH", "fiveH", "sixH", "sevenH", "eightH", "nineH", "tenH", "jackH", "queenH", "kingH", "aceH", 
									  "twoD", "threeD", "fourD", "fiveD", "sixD", "sevenD", "eightD", "nineD", "tenD", "jackD", "queenD", "kingD", "aceD", 
									  "twoC", "threeC", "fourC", "fiveC", "sixC", "sevenC", "eightC", "nineC", "tenC", "jackC", "queenC", "kingC", "aceC",
									  "twoS", "threeS", "fourS", "fiveS", "sixS", "sevenS", "eightS", "nineS", "tenS", "jackS", "queenS", "kingS", "aceS"};

	//list of each players 26 cards they randomly got to use for this game
	List<string> playerOneCards = new List<string>();
	List<string> opponentCards = new List<string>();



    
    private void dealPlayerCards ()
	{
		List<int> dealtCards = new List<int>();

		for (int i = 0; i <= 25; i++) 
		{
			int drawnCard = Random.Range(0, 52);

			if(dealtCards.IndexOf(drawnCard) == -1)
			{
				playerOneCards.Add(cardDeck[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}

		for (int i = 0; i <= 25; i++) 
		{
			int drawnCard = Random.Range(0, 52);
			
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
				opponentCards.Add(cardDeck[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}
	}
    
    
    public void processturn()
    {
		GameObject manager = GameObject.Find("_Manager");
		ChangeScene changescene = manager.GetComponent<ChangeScene>();

		//player one wins turn over opponent
        //must add opponents card to player ones deck. Also must take players ones card they just played and add it to back of deck
        if(getCardValue(playerOneCards[0]) > getCardValue(opponentCards[0]))
        {
            playerOneWin();
			checkForWinner();
			return;
		}
        //opponent wins turn
        else if (getCardValue(playerOneCards[0]) < getCardValue(opponentCards[0]))
        {
            opponentWin();
			checkForWinner();
			return;
        }

        //WARRRRRRR!!!!
        //laying one more card face down then playing next card after that
        if (getCardValue(playerOneCards[0]) == getCardValue(opponentCards[0]))
        {
            int i = 0;
            string winner = "none";

            while (winner == "none")
            {
                //war occuring going to need to visually show this

                i = i + 2;

                if (playerOneCards[i] == null && playerOneCards.Count < opponentCards.Count)
				{
					changescene.changeToScene("menu");
				}

				if (opponentCards[i] == null && playerOneCards.Count > opponentCards.Count)
				{
					changescene.changeToScene("menu");
				}
				         
                if (getCardValue(playerOneCards[i]) > getCardValue(opponentCards[i]))
                {
                	winner = "playerone";
            	}
            	if (getCardValue(playerOneCards[i]) < getCardValue(opponentCards[i]))
            	{
                	winner = "opponent";
            	}
       		}

            if (winner == "playerone")
            {
                for (int x = 0; x <= i; x++)
                {
                    playerOneWin();
                }
            }

            if (winner == "opponent")
            {
                for (int x = 0; x <= i; x++)
                {
                    opponentWin();
                }
            }
        }
    }

	private void checkForWinner()
	{
		GameObject manager = GameObject.Find("_Manager");
		ChangeScene changescene = manager.GetComponent<ChangeScene>();

		//Check for winner before turn is over
		if (playerOneCards.Count == 0)
		{
			changescene.changeToScene("menu");
		}
		
		if (opponentCards.Count == 0)
		{
			changescene.changeToScene("menu");
		}
	}

    private void playerOneWin()
    {
        string tempCardHolder;
        lastHandWinner = "PlayerOne";

        //adding opponents card that was won to back of player ones deck.
        playerOneCards.Add(opponentCards[0]);
        opponentCards.RemoveAt(0);

        //moving played card to back of deck
        tempCardHolder = playerOneCards[0];
        playerOneCards.RemoveAt(0);
        playerOneCards.Add(tempCardHolder);
    }

    private void opponentWin()
    {
        string tempCardHolder;
        lastHandWinner = "Opponent";

        //adding player ones card that was won to back of opponents deck.
        opponentCards.Add(playerOneCards[0]);
        playerOneCards.RemoveAt(0);

        //moving played card to back of deck
        tempCardHolder = opponentCards[0];
        opponentCards.RemoveAt(0);
        opponentCards.Add(tempCardHolder);
    }


    private int getCardValue(string card)
    {
        int cardValue = 0;
        
        if (card.Contains("two"))
        {
            cardValue = 2;
        }
        if(card.Contains("three"))
        {
            cardValue = 3;
        }
        if(card.Contains("four"))
        {
            cardValue = 4;
        }
        if(card.Contains("five"))
        {
            cardValue = 5;
        }
        if(card.Contains("six"))
        {
            cardValue = 6;
        }
        if(card.Contains("seven"))
        {
            cardValue = 7;
        }
        if(card.Contains("eight"))
        {
            cardValue = 8;
        }
        if(card.Contains("nine"))
        {
            cardValue = 9;
        }
        if(card.Contains("ten"))
        {
            cardValue = 10;
        }
        if (card.Contains("jack"))
        {
            cardValue = 11;
        }
        if(card.Contains("queen"))
        {
            cardValue = 12;
        }
        if(card.Contains("king"))
        {
            cardValue = 13;
        }
        if(card.Contains("ace"))
        {
            cardValue = 14;
        }

        return cardValue;
    }

    public void cardToScreenPlayerOne(float xPosition, float yPosition)
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();

        #region switch statement player one
        switch (playerOneCards[0])
        {
            case "twoH":
                Instantiate(deckobjects.twoH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "aceH";
                break;
            
            //Diamonds
            case "twoD":
                Instantiate(deckobjects.twoD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "aceD";
                break;

            //Clubs
            case "twoC":
                Instantiate(deckobjects.twoC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "aceC";
                break;

            //Spades
            case "twoS":
                Instantiate(deckobjects.twoS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation);
                lastCardPlayedPlayerOne = "aceS";
                break;
        }
        #endregion
    }

    public void cardToScreenOpponent(float xPosition, float yPosition)
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();

        #region switch statement opponent
        switch (opponentCards[0])
        {
            case "twoH":
                objectToAnimate = Instantiate(deckobjects.twoH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject;
                lastCardPlayedOpponent = "twoH";
                break;
            case "threeH":
                objectToAnimate = Instantiate(deckobjects.threeH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "threeH";
                break;
            case "fourH":
                objectToAnimate = Instantiate(deckobjects.fourH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fourH";
                break;
            case "fiveH":
                objectToAnimate = Instantiate(deckobjects.fiveH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fiveH";
                break;
            case "sixH":
                objectToAnimate = Instantiate(deckobjects.sixH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation)as GameObject;
                lastCardPlayedOpponent = "sixH";
                break;
            case "sevenH":
                objectToAnimate = Instantiate(deckobjects.sevenH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sevenH";
                break;
            case "eightH":
                objectToAnimate = Instantiate(deckobjects.eightH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "eightH";
                break;
            case "nineH":
                objectToAnimate = Instantiate(deckobjects.nineH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "nineH";
                break;
            case "tenH":
                objectToAnimate = Instantiate(deckobjects.tenH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "tenH";
                break;
            case "jackH":
                objectToAnimate = Instantiate(deckobjects.jackH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "jackH";
                break;
            case "queenH":
                objectToAnimate = Instantiate(deckobjects.queenH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "queenH";
                break;
            case "kingH":
                objectToAnimate = Instantiate(deckobjects.kingH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "kingH";
                break;
            case "aceH":
                objectToAnimate = Instantiate(deckobjects.aceH, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "aceH";
                break;

            //Diamonds
            case "twoD":
                objectToAnimate = Instantiate(deckobjects.twoD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "twoD";
                break;
            case "threeD":
                objectToAnimate = Instantiate(deckobjects.threeD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "threeD";
                break;
            case "fourD":
                objectToAnimate = Instantiate(deckobjects.fourD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fourD";
                break;
            case "fiveD":
                objectToAnimate = Instantiate(deckobjects.fiveD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fiveD";
                break;
            case "sixD":
                objectToAnimate = Instantiate(deckobjects.sixD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sixD";
                break;
            case "sevenD":
                objectToAnimate = Instantiate(deckobjects.sevenD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sevenD";
                break;
            case "eightD":
                objectToAnimate = Instantiate(deckobjects.eightD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "eightD";
                break;
            case "nineD":
                objectToAnimate = Instantiate(deckobjects.nineD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "nineD";
                break;
            case "tenD":
                objectToAnimate = Instantiate(deckobjects.tenD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "tenD";
                break;
            case "jackD":
                objectToAnimate = Instantiate(deckobjects.jackD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "jackD";
                break;
            case "queenD":
                objectToAnimate = Instantiate(deckobjects.queenD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "queenD";
                break;
            case "kingD":
                objectToAnimate = Instantiate(deckobjects.kingD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "kingD";
                break;
            case "aceD":
                objectToAnimate = Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "aceD";
                break;

            //Clubs
            case "twoC":
                objectToAnimate = Instantiate(deckobjects.twoC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "twoC";
                break;
            case "threeC":
                objectToAnimate = Instantiate(deckobjects.threeC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "threeC";
                break;
            case "fourC":
                objectToAnimate = Instantiate(deckobjects.fourC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fourC";
                break;
            case "fiveC":
                objectToAnimate = Instantiate(deckobjects.fiveC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fiveC";
                break;
            case "sixC":
                objectToAnimate = Instantiate(deckobjects.sixC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sixC";
                break;
            case "sevenC":
                objectToAnimate = Instantiate(deckobjects.sevenC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sevenC";
                break;
            case "eightC":
                objectToAnimate = Instantiate(deckobjects.eightC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "eightC";
                break;
            case "nineC":
                objectToAnimate = Instantiate(deckobjects.nineC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "nineC";
                break;
            case "tenC":
                objectToAnimate = Instantiate(deckobjects.tenC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "tenC";
                break;
            case "jackC":
                objectToAnimate = Instantiate(deckobjects.jackC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "jackC";
                break;
            case "queenC":
                objectToAnimate = Instantiate(deckobjects.queenC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "queenC";
                break;
            case "kingC":
                objectToAnimate = Instantiate(deckobjects.kingC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "kingC";
                break;
            case "aceC":
                objectToAnimate = Instantiate(deckobjects.aceC, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "aceC";
                break;

            //Spades
            case "twoS":
                objectToAnimate = Instantiate(deckobjects.twoS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "twoS";
                break;
            case "threeS":
                objectToAnimate = Instantiate(deckobjects.threeS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "threeS";
                break;
            case "fourS":
                objectToAnimate = Instantiate(deckobjects.fourS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fourS";
                break;
            case "fiveS":
                objectToAnimate = Instantiate(deckobjects.fiveS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "fiveS";
                break;
            case "sixS":
                objectToAnimate = Instantiate(deckobjects.sixS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sixS";
                break;
            case "sevenS":
                objectToAnimate = Instantiate(deckobjects.sevenS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "sevenS";
                break;
            case "eightS":
                objectToAnimate = Instantiate(deckobjects.eightS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "eightS";
                break;
            case "nineS":
                objectToAnimate = Instantiate(deckobjects.nineS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "nineS";
                break;
            case "tenS":
                objectToAnimate = Instantiate(deckobjects.tenS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "tenS";
                break;
            case "jackS":
                objectToAnimate = Instantiate(deckobjects.jackS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "jackS";
                break;
            case "queenS":
                objectToAnimate = Instantiate(deckobjects.queenS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "queenS";
                break;
            case "kingS":
                objectToAnimate = Instantiate(deckobjects.kingS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "kingS";
                break;
            case "aceS":
                objectToAnimate = Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), deckobjects.twoH.transform.rotation) as GameObject;
                lastCardPlayedOpponent = "aceS";
                break;
        }
        #endregion
    }

    public void updateScore()
    {
        playerScoreText.text = "Your Score: " + playerOneCards.Count;
        opponentScoreText.text = "Opponent Score: " + opponentCards.Count;
        cardToScreenPlayerOne(0, -300);
    }


	public void removePlayButton()
	{
		playButton.gameObject.SetActive (false);
	}

	public void setPlayButton(string cardClicked)
	{
		cardClicked = cardClicked.Replace ("(Clone)", "");
		string cardSuit = cardClicked.Substring (cardClicked.Length - 1, 1);
		string cardValue = cardClicked.Substring (0, cardClicked.Length - 1);

		if (cardSuit == "H") 
		{
			cardSuit = "Hearts";
		} 
		else if (cardSuit == "D") 
		{
			cardSuit = "Diamonds";
		}
		else if (cardSuit == "C") 
		{
			cardSuit = "Clubs";
		}
		else if (cardSuit == "S") 
		{
			cardSuit = "Spades";
		}



		playButtonText.text = "Play " + cardValue + " of " + cardSuit;
		playButton.gameObject.SetActive (true);

	}

    // Use this for initialization
	void Start () 
	{
		removePlayButton ();
        dealPlayerCards();
        cardToScreenPlayerOne(0, -300);
	}
}