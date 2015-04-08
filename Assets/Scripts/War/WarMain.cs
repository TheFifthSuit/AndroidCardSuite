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
                Instantiate(deckobjects.twoH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceH";
                break;
            
            //Diamonds
            case "twoD":
                Instantiate(deckobjects.twoD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceD";
                break;

            //Clubs
            case "twoC":
                Instantiate(deckobjects.twoC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceC";
                break;

            //Spades
            case "twoS":
                Instantiate(deckobjects.twoS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
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
                Instantiate(deckobjects.twoH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceH";
                break;

            //Diamonds
            case "twoD":
                Instantiate(deckobjects.twoD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceD";
                break;

            //Clubs
            case "twoC":
                Instantiate(deckobjects.twoC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceC, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceC";
                break;

            //Spades
            case "twoS":
                Instantiate(deckobjects.twoS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
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
        //processturn();
		removePlayButton ();
        dealPlayerCards();
        cardToScreenPlayerOne(0, -300);
	}
}