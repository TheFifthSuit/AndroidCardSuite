using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BlackjackMain : MonoBehaviour {


    //variables used in functions
    string lastCardPlayedPlayerOne;
    string lastCardPlayedOpponent;
    public Text playerScoreText;
    public Text opponentScoreText;
    public string lastHandWinner = "";
	public bool stand = false;

	//tracking total and how many cards are in the hand
	int playerOneTotal = 0;
	int playerOneIndex = 0;

	int opponentTotal = 0;
	int opponentIndex = 0;

	// will be used to hide and show buttons
	GameObject restartButton;
	GameObject hitButton;
	GameObject standButton;
	GameObject mainButton;
	
    //card names within deck should match Prefab names should have seperate prefab for all 52 cards
	string[] cardDeck = new string[] {"twoH", "threeH", "fourH", "fiveH", "sixH", "sevenH", "eightH", "nineH", "tenH", "jackH", "queenH", "kingH", "aceH", 
									  "twoD", "threeD", "fourD", "fiveD", "sixD", "sevenD", "eightD", "nineD", "tenD", "jackD", "queenD", "kingD", "aceD", 
									  "twoC", "threeC", "fourC", "fiveC", "sixC", "sevenC", "eightC", "nineC", "tenC", "jackC", "queenC", "kingC", "aceC",
									  "twoS", "threeS", "fourS", "fiveS", "sixS", "sevenS", "eightS", "nineS", "tenS", "jackS", "queenS", "kingS", "aceS"};

	//list of each players hands and the cards delt
	List<string> playerOneCards = new List<string>();
	List<string> opponentCards = new List<string>();
	List<int> dealtCards = new List<int>();



	// deals 2 random cards to player and opponent adds the cards to delt deck
	private void dealPlayerCards ()
	{
		
		for (int i = 0; i <= 1; i++) 
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
		
		for (int i = 0; i <= 1; i++) 
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
        //ChangeScene changescene = manager.GetComponent<ChangeScene>();

		//checking the each players array and for each card add value to total
		for(int i = 0; i != playerOneCards.Count; i++)
		{
			playerOneTotal = playerOneTotal + getCardValue(playerOneCards[i]);
			playerOneIndex++;
		}

		for(int i = 0; i != opponentCards.Count; i++)
		{
			opponentTotal = opponentTotal + getCardValue(opponentCards[i]);
			opponentIndex++;
			if (opponentTotal < 15 && opponentTotal < playerOneTotal)
			{
				for (int x = 0; x <= 0; x++) 
				{
					int drawnCard = Random.Range(0, 52);
					
					if(dealtCards.IndexOf(drawnCard) == -1)
					{
						opponentCards.Add(cardDeck[drawnCard]);
						dealtCards.Add(drawnCard);
					}
					else
					{
						x--;
					}
				}
			}
		}


		updateScore(); 

    }

	private void checkForWinner()
	{
		GameObject manager = GameObject.Find("_Manager");
    	ChangeScene changescene = manager.GetComponent<ChangeScene>();

		//Check for winner before turn is over

		if (playerOneTotal > 21)
		{
			print("You Bust");

			restartButton.SetActive(true);
			mainButton.SetActive(true);

			hitButton.SetActive (false);
			standButton.SetActive (false);
		}

		if (playerOneTotal == 21)
		{
			print("You Win");
			
			restartButton.SetActive(true);
			mainButton.SetActive(true);

			hitButton.SetActive (false);
			standButton.SetActive (false);

		}
		if (stand == true) 
		{

			if (playerOneTotal < 21)
			{
				if (opponentTotal > 21)
				{
					print("You beat the AI");
					
					restartButton.SetActive(true);
					mainButton.SetActive(true);
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}
				else {
				if (playerOneTotal > opponentTotal)
				{
					print("you beat the AI");
					
					restartButton.SetActive(true);
					mainButton.SetActive(true);
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}

				if (playerOneTotal < opponentTotal)
				{
					print("the AI beat you");
					
					restartButton.SetActive(true);
					mainButton.SetActive(true);
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}

				if (playerOneTotal == opponentTotal)
				{
					print("You Have the same score");
					
					restartButton.SetActive(true);
					mainButton.SetActive(true);
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}
				}
				
			}

		}



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
            cardValue = 10;
        }
        if(card.Contains("queen"))
        {
            cardValue = 10;
        }
        if(card.Contains("king"))
        {
            cardValue = 10;
        }
        if(card.Contains("ace"))
        {
            cardValue = 11;
        }

        return cardValue;
    }

    public void cardToScreenPlayerOne()
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();


		for(int i = 0; i != playerOneCards.Count; i++)
		{
        #region switch statement player one
        switch (playerOneCards[i])
        {

            case "twoH":
				Instantiate(deckobjects.twoH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoH";
                break;
            case "threeH":
				Instantiate(deckobjects.threeH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeH";
                break;
            case "fourH":
				Instantiate(deckobjects.fourH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourH";
                break;
            case "fiveH":
				Instantiate(deckobjects.fiveH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveH";
                break;
            case "sixH":
				Instantiate(deckobjects.sixH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixH";
                break;
            case "sevenH":
				Instantiate(deckobjects.sevenH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenH";
                break;
            case "eightH":
				Instantiate(deckobjects.eightH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightH";
                break;
            case "nineH":
				Instantiate(deckobjects.nineH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineH";
                break;
            case "tenH":
				Instantiate(deckobjects.tenH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenH";
                break;
            case "jackH":
				Instantiate(deckobjects.jackH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackH";
                break;
            case "queenH":
				Instantiate(deckobjects.queenH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenH";
                break;
            case "kingH":
				Instantiate(deckobjects.kingH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingH";
                break;
            case "aceH":
				Instantiate(deckobjects.aceH, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceH";
                break;
            
            //Diamonds
            case "twoD":
				Instantiate(deckobjects.twoD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoD";
                break;
            case "threeD":
				Instantiate(deckobjects.threeD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeD";
                break;
            case "fourD":
				Instantiate(deckobjects.fourD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourD";
                break;
            case "fiveD":
				Instantiate(deckobjects.fiveD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveD";
                break;
            case "sixD":
				Instantiate(deckobjects.sixD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixD";
                break;
            case "sevenD":
				Instantiate(deckobjects.sevenD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenD";
                break;
            case "eightD":
				Instantiate(deckobjects.eightD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightD";
                break;
            case "nineD":
				Instantiate(deckobjects.nineD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineD";
                break;
            case "tenD":
				Instantiate(deckobjects.tenD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenD";
                break;
            case "jackD":
				Instantiate(deckobjects.jackD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackD";
                break;
            case "queenD":
				Instantiate(deckobjects.queenD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenD";
                break;
            case "kingD":
				Instantiate(deckobjects.kingD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingD";
                break;
            case "aceD":
				Instantiate(deckobjects.aceD, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceD";
                break;

            //Clubs
            case "twoC":
				Instantiate(deckobjects.twoC, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoC";
                break;
            case "threeC":
				Instantiate(deckobjects.threeC, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeC";
                break;
            case "fourC":
				Instantiate(deckobjects.fourC, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourC";
                break;
            case "fiveC":
				Instantiate(deckobjects.fiveC, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveC";
                break;
            case "sixC":
				Instantiate(deckobjects.sixC, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixC";
                break;
            case "sevenC":
				Instantiate(deckobjects.sevenC, new Vector3(playerOneIndex*100, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenC";
                break;
            case "eightC":
				Instantiate(deckobjects.eightC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightC";
                break;
            case "nineC":
				Instantiate(deckobjects.nineC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineC";
                break;
            case "tenC":
				Instantiate(deckobjects.tenC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenC";
                break;
            case "jackC":
				Instantiate(deckobjects.jackC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackC";
                break;
            case "queenC":
				Instantiate(deckobjects.queenC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenC";
                break;
            case "kingC":
				Instantiate(deckobjects.kingC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingC";
                break;
            case "aceC":
				Instantiate(deckobjects.aceC, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceC";
                break;

            //Spades
            case "twoS":
				Instantiate(deckobjects.twoS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoS";
                break;
            case "threeS":
				Instantiate(deckobjects.threeS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeS";
                break;
            case "fourS":
				Instantiate(deckobjects.fourS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourS";
                break;
            case "fiveS":
				Instantiate(deckobjects.fiveS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveS";
                break;
            case "sixS":
				Instantiate(deckobjects.sixS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixS";
                break;
            case "sevenS":
				Instantiate(deckobjects.sevenS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenS";
                break;
            case "eightS":
				Instantiate(deckobjects.eightS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightS";
                break;
            case "nineS":
				Instantiate(deckobjects.nineS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineS";
                break;
            case "tenS":
				Instantiate(deckobjects.tenS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenS";
                break;
            case "jackS":
				Instantiate(deckobjects.jackS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackS";
                break;
            case "queenS":
				Instantiate(deckobjects.queenS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenS";
                break;
            case "kingS":
				Instantiate(deckobjects.kingS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingS";
                break;
            case "aceS":
				Instantiate(deckobjects.aceS, new Vector3(playerOneIndex*150, -400, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceS";
                break;
			}}
        #endregion
    }

    public void cardToScreenOpponent()
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
        

		for (int i = 0; i != opponentCards.Count; i++) {
			#region switch statement opponent
			switch (opponentCards [i]) {
			case "twoH":
				Instantiate (deckobjects.twoH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "twoH";
				break;
			case "threeH":
				Instantiate (deckobjects.threeH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "threeH";
				break;
			case "fourH":
				Instantiate (deckobjects.fourH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fourH";
				break;
			case "fiveH":
				Instantiate (deckobjects.fiveH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fiveH";
				break;
			case "sixH":
				Instantiate (deckobjects.sixH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sixH";
				break;
			case "sevenH":
				Instantiate (deckobjects.sevenH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sevenH";
				break;
			case "eightH":
				Instantiate (deckobjects.eightH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "eightH";
				break;
			case "nineH":
				Instantiate (deckobjects.nineH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				Instantiate (deckobjects.tenH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				Instantiate (deckobjects.jackH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				Instantiate (deckobjects.queenH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				Instantiate (deckobjects.kingH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				Instantiate (deckobjects.aceH, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceH";
				break;

			//Diamonds
			case "twoD":
				Instantiate (deckobjects.twoD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "twoD";
				break;
			case "threeD":
				Instantiate (deckobjects.threeD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "threeD";
				break;
			case "fourD":
				Instantiate (deckobjects.fourD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fourD";
				break;
			case "fiveD":
				Instantiate (deckobjects.fiveD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fiveD";
				break;
			case "sixD":
				Instantiate (deckobjects.sixD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sixD";
				break;
			case "sevenD":
				Instantiate (deckobjects.sevenD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sevenD";
				break;
			case "eightD":
				Instantiate (deckobjects.eightD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "eightD";
				break;
			case "nineD":
				Instantiate (deckobjects.nineD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				Instantiate (deckobjects.tenD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				Instantiate (deckobjects.jackD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				Instantiate (deckobjects.queenD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				Instantiate (deckobjects.kingD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				Instantiate (deckobjects.aceD, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceD";
				break;

			//Clubs
			case "twoC":
				Instantiate (deckobjects.twoC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "twoC";
				break;
			case "threeC":
				Instantiate (deckobjects.threeC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "threeC";
				break;
			case "fourC":
				Instantiate (deckobjects.fourC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fourC";
				break;
			case "fiveC":
				Instantiate (deckobjects.fiveC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fiveC";
				break;
			case "sixC":
				Instantiate (deckobjects.sixC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sixC";
				break;
			case "sevenC":
				Instantiate (deckobjects.sevenC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sevenC";
				break;
			case "eightC":
				Instantiate (deckobjects.eightC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "eightC";
				break;
			case "nineC":
				Instantiate (deckobjects.nineC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				Instantiate (deckobjects.tenC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				Instantiate (deckobjects.jackC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				Instantiate (deckobjects.queenC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				Instantiate (deckobjects.kingC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				Instantiate (deckobjects.aceC, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceC";
				break;

			//Spades
			case "twoS":
				Instantiate (deckobjects.twoS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "twoS";
				break;
			case "threeS":
				Instantiate (deckobjects.threeS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "threeS";
				break;
			case "fourS":
				Instantiate (deckobjects.fourS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fourS";
				break;
			case "fiveS":
				Instantiate (deckobjects.fiveS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "fiveS";
				break;
			case "sixS":
				Instantiate (deckobjects.sixS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sixS";
				break;
			case "sevenS":
				Instantiate (deckobjects.sevenS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "sevenS";
				break;
			case "eightS":
				Instantiate (deckobjects.eightS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "eightS";
				break;
			case "nineS":
				Instantiate (deckobjects.nineS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				Instantiate (deckobjects.tenS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				Instantiate (deckobjects.jackS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				Instantiate (deckobjects.queenS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				Instantiate (deckobjects.kingS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				Instantiate (deckobjects.aceS, new Vector3 (0, 400, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceS";
				break;
			}
			#endregion
		}
    }

    public void updateScore()
    {
		playerScoreText.text = "Player One Score: " + playerOneTotal + " dealt Count " + dealtCards.Count;
		opponentScoreText.text = "Opponents Score: " + opponentTotal;
        cardToScreenPlayerOne();
		cardToScreenOpponent();

    }

	public void Stand()
	{
		GameObject[] cardclones = GameObject.FindGameObjectsWithTag("Card");
		foreach (GameObject clone in cardclones)
		{
			Destroy(clone);
		}

		stand = true;

		updateScore ();
		checkForWinner ();

	}

	public void Hit()
	{
		
		for (int i = 0; i <= 0; i++) 
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

		
		playerOneTotal = playerOneTotal + getCardValue(playerOneCards[playerOneIndex]);
		playerOneIndex++;

		//destroy all the cards on the table
		GameObject[] cardclones = GameObject.FindGameObjectsWithTag("Card");
		foreach (GameObject clone in cardclones)
		{
			Destroy(clone);
		}
		updateScore ();
		checkForWinner ();
	}



    // Use this for initialization
	void Start () 
	{

		dealPlayerCards();      
		processturn();

	}

    void FixedUpdate()
    {

    }
}