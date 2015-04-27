﻿using UnityEngine;
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

    int totalScoreOpponent = 0;
    int totalScorePlayer = 0;

	int count =0;

	// will be used to hide and show buttons and messages
	public GameObject bustMessage;
    public GameObject winMessage;
    public GameObject loseMessage;
	public GameObject hitButton;
	public GameObject standButton;
	public GameObject mainButton;
    public GameObject pushMessage;
    public List<GameObject> objectsOnScreen = new List<GameObject>();
	
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
		//player ones hand
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

		//players 2 hand
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
    
    
    public void processturnPlayer()
    {
		//updating player ones score
        for(int i = 0; i != playerOneCards.Count; i++)
		{
			playerOneTotal = playerOneTotal + getCardValue(playerOneCards[i]);
			playerOneIndex++;
		}
		updateScore(); 
    }

    public void processTurnOpponent()
    {
        //dealer will now hit or stand when this is called
        for (int i = 0; i != opponentCards.Count; i++)
        {
            opponentTotal = opponentTotal + getCardValue(opponentCards[i]);
            opponentIndex++;
            if (opponentTotal > 21)
            {
                for (int z = 0; z != opponentCards.Count; z++)
                {
                    int CardTempValue = getCardValue(opponentCards[z]);
                    if (CardTempValue == 11)
                    {
                        opponentTotal = opponentTotal - 10;
                    }
                }
            }
            if (opponentTotal < 11)
            {
                for (int x = 0; x <= 0; x++)
                {
                    int drawnCard = Random.Range(0, 52);

                    if (dealtCards.IndexOf(drawnCard) == -1)
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

    public void playAnimation(GameObject objectToFlip)
    {
        objectToFlip.animation.Play();
    }

    public void flipAllCards(List<GameObject> objectList)
    {
        foreach (GameObject obj in objectList)
        {
            if(obj.transform.localRotation.y == 1)
            {
                obj.animation.Play();
            }
        }
    }

	private void checkForWinner()
	{
		GameObject manager = GameObject.Find("_Manager");
    	ChangeScene changescene = manager.GetComponent<ChangeScene>();
        GameObject bust;
        GameObject win;
        GameObject lose;
        GameObject push;

		//Check for winner before turn is over
		if (playerOneTotal > 21)
		{
            bust = Instantiate(bustMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            bust.transform.SetParent(GameObject.Find("Canvas").transform);
            flipAllCards(objectsOnScreen);
            bust.animation.Play();
            totalScoreOpponent += 1;

			hitButton.SetActive (false);
			standButton.SetActive (false);
		}

		if (playerOneTotal == 21)
		{
            win = Instantiate(winMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            win.transform.SetParent(GameObject.Find("Canvas").transform);
            flipAllCards(objectsOnScreen);
            win.animation.Play();
            totalScorePlayer += 1;

			hitButton.SetActive (false);
			standButton.SetActive (false);

		}
		if (stand == true) 
		{

			if (playerOneTotal < 21)
			{
				if (opponentTotal > 21)
				{

                    win = Instantiate(winMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    win.transform.SetParent(GameObject.Find("Canvas").transform);
                    flipAllCards(objectsOnScreen);
                    win.animation.Play();

                    totalScorePlayer += 1;
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}
				else {
				if (playerOneTotal > opponentTotal)
				{

                    win = Instantiate(winMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    win.transform.SetParent(GameObject.Find("Canvas").transform);
                    flipAllCards(objectsOnScreen);
                    win.animation.Play();

                    totalScorePlayer += 1;
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}

				if (playerOneTotal < opponentTotal)
				{
                    lose = Instantiate(loseMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    lose.transform.SetParent(GameObject.Find("Canvas").transform);
                    flipAllCards(objectsOnScreen);
                    lose.animation.Play();
                    totalScoreOpponent += 1;
					
					hitButton.SetActive (false);
					standButton.SetActive (false);
				}

				if (playerOneTotal == opponentTotal)
				{
                    push = Instantiate(pushMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    push.transform.SetParent(GameObject.Find("Canvas").transform);
                    flipAllCards(objectsOnScreen);
                    push.animation.Play();
                    totalScoreOpponent += 1;

					hitButton.SetActive (false);
					standButton.SetActive (false);
				}
				}
				
			}

		}
	}




    public void updateScore()
    {
		playerScoreText.text = "Your Score: " + playerOneTotal + " Overall: " + totalScorePlayer;
		opponentScoreText.text = "Opponent Score: " + opponentTotal + " Overall: " + totalScoreOpponent;
        clearBoardCardsToScreen();
    }

	public void Stand()
	{
		stand = true;
        processTurnOpponent();
		updateScore();
		checkForWinner();
		playAnimation(objectsOnScreen[2]);
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

		if (playerOneTotal > 21 && count == 0) 
		{
			for(int y = 0; y < playerOneCards.Count; y++)
			{
				int CardTempValue = getCardValue(playerOneCards[y]);
				if (CardTempValue == 11)
				{
					playerOneTotal = playerOneTotal - 10;
					count++;
				}
			}
		}

		print (dealtCards.Count);
		stand = false;
		playerOneIndex++;
        updateScore();
		checkForWinner();
	}



    // Use this for initialization
	void Start () 
	{
		hitButton.SetActive (true);
		standButton.SetActive (true);
		dealPlayerCards();
        processturnPlayer();
	}

    public void reset()
    {
        //reset all variables that only last for current hand
		dealtCards.Clear ();
        playerOneCards.Clear();
        opponentCards.Clear();
        playerOneTotal = 0;
        opponentTotal = 0;
        playerOneIndex = 0;
        opponentIndex = 0;
		count = 0;
        objectsOnScreen.Clear();
    }

    public void nextHand()
    {
        reset();
        hitButton.SetActive(true);
        standButton.SetActive(true);
        dealPlayerCards();
        processturnPlayer();
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

    public void clearBoardCardsToScreen()
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
        
        objectsOnScreen.Clear();
        cardToScreenPlayerOne(0, -400);
        cardToScreenOpponent(0, 400);
    }

    public void cardToScreenPlayerOne(float xPosition, float yPosition)
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();

        for (int i = 0; i <= playerOneCards.Count - 1; i++)
        {
            xPosition = i * 200;

            #region switch statement player one
            switch (playerOneCards[i])
            {
                case "twoH":
                    objectsOnScreen.Add(Instantiate(deckobjects.twoH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "twoH";
                    break;
                case "threeH":
                    objectsOnScreen.Add(Instantiate(deckobjects.threeH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "threeH";
                    break;
                case "fourH":
                    objectsOnScreen.Add(Instantiate(deckobjects.fourH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fourH";
                    break;
                case "fiveH":
                    objectsOnScreen.Add(Instantiate(deckobjects.fiveH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fiveH";
                    break;
                case "sixH":
                    objectsOnScreen.Add(Instantiate(deckobjects.sixH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sixH";
                    break;
                case "sevenH":
                    objectsOnScreen.Add(Instantiate(deckobjects.sevenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sevenH";
                    break;
                case "eightH":
                    objectsOnScreen.Add(Instantiate(deckobjects.eightH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "eightH";
                    break;
                case "nineH":
                    objectsOnScreen.Add(Instantiate(deckobjects.nineH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "nineH";
                    break;
                case "tenH":
                    objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "tenH";
                    break;
                case "jackH":
                    objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "jackH";
                    break;
                case "queenH":
                    objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "queenH";
                    break;
                case "kingH":
                    objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "kingH";
                    break;
                case "aceH":
                    objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "aceH";
                    break;

                //Diamonds
                case "twoD":
                    objectsOnScreen.Add(Instantiate(deckobjects.twoD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "twoD";
                    break;
                case "threeD":
                    objectsOnScreen.Add(Instantiate(deckobjects.threeD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "threeD";
                    break;
                case "fourD":
                    objectsOnScreen.Add(Instantiate(deckobjects.fourD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fourD";
                    break;
                case "fiveD":
                    objectsOnScreen.Add(Instantiate(deckobjects.fiveD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fiveD";
                    break;
                case "sixD":
                    objectsOnScreen.Add(Instantiate(deckobjects.sixD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sixD";
                    break;
                case "sevenD":
                    objectsOnScreen.Add(Instantiate(deckobjects.sevenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sevenD";
                    break;
                case "eightD":
                    objectsOnScreen.Add(Instantiate(deckobjects.eightD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "eightD";
                    break;
                case "nineD":
                    objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "nineD";
                    break;
                case "tenD":
                    objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "tenD";
                    break;
                case "jackD":
                    objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "jackD";
                    break;
                case "queenD":
                    objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "queenD";
                    break;
                case "kingD":
                    objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "kingD";
                    break;
                case "aceD":
                    objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "aceD";
                    break;

                //Clubs
                case "twoC":
                    objectsOnScreen.Add(Instantiate(deckobjects.twoC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "twoC";
                    break;
                case "threeC":
                    objectsOnScreen.Add(Instantiate(deckobjects.threeC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "threeC";
                    break;
                case "fourC":
                    objectsOnScreen.Add(Instantiate(deckobjects.fourC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fourC";
                    break;
                case "fiveC":
                    objectsOnScreen.Add(Instantiate(deckobjects.fiveC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fiveC";
                    break;
                case "sixC":
                    objectsOnScreen.Add(Instantiate(deckobjects.sixC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sixC";
                    break;
                case "sevenC":
                    objectsOnScreen.Add(Instantiate(deckobjects.sevenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sevenC";
                    break;
                case "eightC":
                    objectsOnScreen.Add(Instantiate(deckobjects.eightC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "eightC";
                    break;
                case "nineC":
                    objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "nineC";
                    break;
                case "tenC":
                    objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "tenC";
                    break;
                case "jackC":
                    objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "jackC";
                    break;
                case "queenC":
                    objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "queenC";
                    break;
                case "kingC":
                    objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "kingC";
                    break;
                case "aceC":
                    objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "aceC";
                    break;

                //Spades
                case "twoS":
                    objectsOnScreen.Add(Instantiate(deckobjects.twoS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "twoS";
                    break;
                case "threeS":
                    objectsOnScreen.Add(Instantiate(deckobjects.threeS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "threeS";
                    break;
                case "fourS":
                    objectsOnScreen.Add(Instantiate(deckobjects.fourS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fourS";
                    break;
                case "fiveS":
                    objectsOnScreen.Add(Instantiate(deckobjects.fiveS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "fiveS";
                    break;
                case "sixS":
                    objectsOnScreen.Add(Instantiate(deckobjects.sixS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sixS";
                    break;
                case "sevenS":
                    objectsOnScreen.Add(Instantiate(deckobjects.sevenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "sevenS";
                    break;
                case "eightS":
                    objectsOnScreen.Add(Instantiate(deckobjects.eightS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "eightS";
                    break;
                case "nineS":
                    objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "nineS";
                    break;
                case "tenS":
                    objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "tenS";
                    break;
                case "jackS":
                    objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "jackS";
                    break;
                case "queenS":
                    objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "queenS";
                    break;
                case "kingS":
                    objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "kingS";
                    break;
                case "aceS":
                    objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                    lastCardPlayedPlayerOne = "aceS";
                    break;
            }
            #endregion
        }
    }
	
	public void cardToScreenOpponent(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
        Quaternion rotation;

		for (int i = 0; i <= opponentCards.Count - 1; i++) 
        {

			xPosition= i*200;

            //this makes sure first card of dealers hand is facedown
            if (i == 0)
                rotation = deckobjects.twoH.transform.rotation;
            else
                rotation = Quaternion.identity;

			#region switch statement opponent
			switch (opponentCards[i]){
			case "twoH":
				objectsOnScreen.Add(Instantiate(deckobjects.twoH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "twoH";
				break;
			case "threeH":
				objectsOnScreen.Add(Instantiate(deckobjects.threeH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "threeH";
				break;
			case "fourH":
				objectsOnScreen.Add(Instantiate(deckobjects.fourH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fourH";
				break;
			case "fiveH":
				objectsOnScreen.Add(Instantiate(deckobjects.fiveH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fiveH";
				break;
			case "sixH":
				objectsOnScreen.Add(Instantiate(deckobjects.sixH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sixH";
				break;
			case "sevenH":
				objectsOnScreen.Add(Instantiate(deckobjects.sevenH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sevenH";
				break;
			case "eightH":
				objectsOnScreen.Add(Instantiate(deckobjects.eightH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "eightH";
				break;
			case "nineH":
				objectsOnScreen.Add(Instantiate(deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "aceH";
				break;
			
			//Diamonds
			case "twoD":
				objectsOnScreen.Add(Instantiate(deckobjects.twoD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "twoD";
				break;
			case "threeD":
				objectsOnScreen.Add(Instantiate(deckobjects.threeD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "threeD";
				break;
			case "fourD":
				objectsOnScreen.Add(Instantiate(deckobjects.fourD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fourD";
				break;
			case "fiveD":
				objectsOnScreen.Add(Instantiate(deckobjects.fiveD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fiveD";
				break;
			case "sixD":
				objectsOnScreen.Add(Instantiate(deckobjects.sixD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sixD";
				break;
			case "sevenD":
				objectsOnScreen.Add(Instantiate(deckobjects.sevenD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sevenD";
				break;
			case "eightD":
				objectsOnScreen.Add(Instantiate(deckobjects.eightD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "eightD";
				break;
			case "nineD":
				objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "aceD";
				break;
			
			//Clubs
			case "twoC":
				objectsOnScreen.Add(Instantiate(deckobjects.twoC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "twoC";
				break;
			case "threeC":
				objectsOnScreen.Add(Instantiate(deckobjects.threeC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "threeC";
				break;
			case "fourC":
				objectsOnScreen.Add(Instantiate(deckobjects.fourC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fourC";
				break;
			case "fiveC":
				objectsOnScreen.Add(Instantiate(deckobjects.fiveC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fiveC";
				break;
			case "sixC":
				objectsOnScreen.Add(Instantiate(deckobjects.sixC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sixC";
				break;
			case "sevenC":
				objectsOnScreen.Add(Instantiate(deckobjects.sevenC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sevenC";
				break;
			case "eightC":
				objectsOnScreen.Add(Instantiate(deckobjects.eightC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "eightC";
				break;
			case "nineC":
				objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "aceC";
				break;
			
			//Spades
			case "twoS":
				objectsOnScreen.Add(Instantiate(deckobjects.twoS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "twoS";
				break;
			case "threeS":
				objectsOnScreen.Add(Instantiate(deckobjects.threeS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "threeS";
				break;
			case "fourS":
				objectsOnScreen.Add(Instantiate(deckobjects.fourS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fourS";
				break;
			case "fiveS":
				objectsOnScreen.Add(Instantiate(deckobjects.fiveS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "fiveS";
				break;
			case "sixS":
				objectsOnScreen.Add(Instantiate(deckobjects.sixS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sixS";
				break;
			case "sevenS":
				objectsOnScreen.Add(Instantiate(deckobjects.sevenS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "sevenS";
				break;
			case "eightS":
				objectsOnScreen.Add(Instantiate(deckobjects.eightS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "eightS";
				break;
			case "nineS":
				objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), rotation) as GameObject);
				lastCardPlayedOpponent = "aceS";
				break;
			}
			#endregion

		}
	}
}