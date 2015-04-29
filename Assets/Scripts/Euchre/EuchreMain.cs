using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EuchreMain : MonoBehaviour {

    //variables used in functions
	public Text trumpText;
	public string lastHandWinner = "";

	int newTrumpHearts= 0;
	int newTrumpDiamonds= 0;
	int newTrumpClubs= 0;
	int newTrumpSpades= 0;

	public bool cardIsDiscarded = true;
	public bool cardIsPlayed = true;

	// string of the cards played or dicarded
	public string discard;
	public string playCard;

	// Booleans to set the buttons Active or not
	bool trumpMade = false;
	bool passMade = false;
    public string whoMadeTrump;

	bool playerOneTrumpMade = false;

	
	// will be used to hide and show buttons
	public GameObject trumpButton;
	public GameObject passButton;
	public GameObject heartsButton;
	public GameObject dimondsButton;
	public GameObject clubsButton;
	public GameObject spadesButton;
	public GameObject redealButton;
	public GameObject playCardToHand;
    public GameObject playerOneWinMessage;
    public GameObject playerTwoWinMessage;
    public GameObject opponentOneWinMessage;
    public GameObject opponentTwoWinMessage;
    public Text feed;
    private string line1 = "";
    private string line2 = "";
    private string line3 = "";
    public GameObject selectedCard;
    


	//card names within deck should match Prefab names should have seperate prefab for all 24 cards
	string[] cardDeck = new string[] {"nineH", "tenH", "jackH", "queenH", "kingH", "aceH", 
		                              "nineD", "tenD", "jackD", "queenD", "kingD", "aceD", 
		                              "nineC", "tenC", "jackC", "queenC", "kingC", "aceC",
		                              "nineS", "tenS", "jackS", "queenS", "kingS", "aceS"};

    //this is the same as cardDeck just GameObjects instead of strings
	public List<GameObject> cardDeckObjects = new List<GameObject>();

	//Track the Dealer
	int trackDealer = 0;

	//Track the Leader
	public int trackLeader = 1;

	//empty string that will be later filled with the first card in the Kitty
	string potentialTrump;
	string currentTrump;

	//number of Trump for each player
	int playerOneNumOfTrump= 0;
	int playerTwoNumOfTrump= 0;
	int opponentOneNumOfTrump= 0;
	int opponentTwoNumOfTrump= 0;

	//players list of current cards
    public List<GameObject> playerOneCards = new List<GameObject>();
    public List<GameObject> playerTwoCards = new List<GameObject>();
    public List<GameObject> opponentOneCards = new List<GameObject>();
    public List<GameObject> opponentTwoCards = new List<GameObject>();
    public List<GameObject> objectsOnScreen = new List<GameObject>();
    public List<GameObject> kittyCards = new List<GameObject>();
    public List<GameObject> handCards = new List<GameObject>();
	
    //bools to track who has played in the hand.
    public bool playerOnePlayed;
    public bool playerTwoPlayed;
    public bool opponentOnePlayed;
    public bool opponentTwoPlayed;
    public bool trumpSelectionFinished;

	//bool to track whos turn to select turmp
	public bool playerOneTrumpTurn;
	public bool playerTwoTrumpTurn;
	public bool opponentOneTrumpTurn;
	public bool opponentTwoTrumpTurn;

	//bool to track SECOND round to make trump
	public bool playerOneTrumpSecondTurn;
	public bool playerTwoTrumpSecondTurn;
	public bool opponentOneTrumpSecondTurn;
	public bool opponentTwoTrumpSecondTurn;

	// Use this for initialization
	void Start() 
	{
        fillCardDeckObjects();

		//second round get to choose trump with butons or redeal
		trumpButton.SetActive (false);
		passButton.SetActive (false);
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);

		//starts the game
        playGame();
	}

    //fills deck of objects with method called in deckObjects class
    private void fillCardDeckObjects()
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
        
        foreach (string st in cardDeck)
        {
            deckobjects.createListOfSpecificCards(st);
        }

        foreach (GameObject go in deckobjects.selectCards)
        {
            cardDeckObjects.Add(go);
        }
    }

	// deals 5 random cards to all players left overs go in the Kitty randomized
	private void dealPlayerCards ()
	{
        //list of cards delt
        List<int> dealtCards = new List<int>();
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
        
        for (int i = 0; i <= 4; i++) 
		{
			int drawnCard = Random.Range(0, 24);
            
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
                playerOneCards.Add(cardDeckObjects[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}
		
		for (int i = 0; i <= 4; i++) 
		{
			int drawnCard = Random.Range(0, 24);
			
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
                opponentOneCards.Add(cardDeckObjects[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}

		
		for (int i = 0; i <= 4; i++) 
		{
			int drawnCard = Random.Range(0, 24);
			
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
                playerTwoCards.Add(cardDeckObjects[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}

		for (int i = 0; i <= 4; i++) 
		{
			int drawnCard = Random.Range(0, 24);
			
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
                opponentTwoCards.Add(cardDeckObjects[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}

		for (int i = 0; i <= 3; i++) 
		{
			int drawnCard = Random.Range(0, 24);
			
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
                kittyCards.Add(cardDeckObjects[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}
		
	}

	public void playGame()
	{
        trackDealer = 0;

        dealPlayerCards();
        displayAllCard();
        countTrumpInHand();
        makeTrumpFromKitty();

	}

    void Update()
    {
        if (playerOnePlayed == true)
        {
            playOpponentOne();
            playerOnePlayed = false;
        }
        if (playerTwoPlayed == true)
        {
            playOpponentTwo();
            playerTwoPlayed = false;
        }
        if (opponentOnePlayed == true)
        {
            playPlayerTwo();
            opponentOnePlayed = false;
        }
        if (opponentTwoPlayed == true)
        {
            playPlayerOne();
            opponentTwoPlayed = false;
        }
        if (trumpSelectionFinished == true)
        {

			if(trackDealer==0)
			{
				if(cardIsDiscarded==true)
				{
					// FOR SOME REASON THEY DO NOT GET SET TO FALSE ON CLICK
					trumpButton.SetActive (false);
					passButton.SetActive (false);
					heartsButton.SetActive(false);
					dimondsButton.SetActive (false);
					clubsButton.SetActive(false);
					spadesButton.SetActive (false);
					redealButton.SetActive (false);
					addCardsToHand();
				}
			}
            
            //checkForWinnerOfHand();
            trumpSelectionFinished = false;
        }
		if (playerOneTrumpTurn == true || playerOneTrumpSecondTurn == true) 
        {
            playerOneMakeTrump();
		}
		if (opponentOneTrumpTurn == true || opponentOneTrumpSecondTurn == true )
        {
            opponentOneMakeTrump();
        }
		if (playerTwoTrumpTurn == true || playerTwoTrumpSecondTurn == true )
        {
            playerTwoMakeTrump();
        }
		if (opponentTwoTrumpTurn == true || opponentTwoTrumpSecondTurn == true)
        {
            opponentTwoMakeTrump();
        }
        if(handCards.Count == 4)
        {
            checkForWinnerOfHand();
            handCards.Clear();
            //clearBoardNoReplaceKitty();
        }
		if (trumpMade == true)
		{
			trumpButton.SetActive(false);
			passButton.SetActive(false);
			heartsButton.SetActive (false);
			dimondsButton.SetActive (false);
			clubsButton.SetActive(false);
			spadesButton.SetActive (false);
			redealButton.SetActive (false);
		}
		if (passMade == true)
		{
			trumpButton.SetActive(false);
			passButton.SetActive(false);
		}
    }

	public void trumpHeartsClicked()
	{
		trumpMade = true;
		currentTrump = "Hearts";
		trumpText.text = "Trump is: " + currentTrump;
		cardIsDiscarded = true;
		trumpSelectionFinished = true;
		playerOneTrumpSecondTurn = false;
	}

	public void trumpClubsClicked()
	{
		trumpMade = true;
		currentTrump = "Clubs";
		trumpText.text = "Trump is: " + currentTrump;
		cardIsDiscarded = true;
		trumpSelectionFinished = true;
		playerOneTrumpSecondTurn = false;
	}
	public void trumpSpadessClicked()
	{
		trumpMade = true;
		currentTrump = "Spades";
		trumpText.text = "Trump is: " + currentTrump;
		cardIsDiscarded = true;
		trumpSelectionFinished = true;
		playerOneTrumpSecondTurn = false;
	}
	public void trumpDiamondsClicked()
	{
		trumpMade = true;
		currentTrump = "Dimonds";
		trumpText.text = "Trump is: " + currentTrump;
		cardIsDiscarded = true;
		trumpSelectionFinished = true;
		playerOneTrumpSecondTurn = false;
	}

    public void addCardsToHand()
    {
    	if (trackLeader == 1)
    	{
        	playOpponentOne();
    	}
    	if (trackLeader == 2)
    	{
        	playPlayerTwo();
    	}
    	if (trackLeader == 3)
    	{
        	playOpponentTwo();
    	}
    	if (trackLeader == 4)
    	{
        	playPlayerOne();
    	}
    }

	public void playOpponentOne()
	{
		// Check all cards in hand for HIGHEST and LOWEST and HIGHEST NOT TRUMP
		int maxCard = 0;
		int indexMaxCard = 0;
		
		int minCard = 0;
		int indexMinCard = 1000;
		
		
		for (int i = 0; i != opponentOneCards.Count; i++) 
		{
			string getCard = opponentOneCards[i].name.ToString();
			if (getCardValue (getCard) > maxCard) 
			{
				maxCard = getCardValue(getCard);
				indexMaxCard = i;	
			}
			
			if (getCardValue (getCard) < minCard) 
			{
				minCard = getCardValue(getCard);
				indexMinCard = i;	
			}
			
		}
		handCards.Add(opponentOneCards[indexMaxCard]);
        playSpecificCardInHand(handCards.Count - 1);
        removeFromList(indexMaxCard, "opponentOne");
        opponentOnePlayed = true;
	}

	public void playPlayerTwo()
	{
		// Check all cards in hand for HIGHEST and LOWEST and HIGHEST NOT TRUMP
		int maxCard = 0;
		int indexMaxCard = 0;

		for (int i = 0; i != playerTwoCards.Count; i++) 
		{
			string getCard = playerTwoCards[i].name.ToString();
			if (getCardValue (getCard) > maxCard) 
			{
				maxCard = getCardValue (getCard);
				indexMaxCard = i;	
			}
		}

		handCards.Add(playerTwoCards[indexMaxCard]);
        playSpecificCardInHand(handCards.Count - 1);
        removeFromList(indexMaxCard, "playerTwo");
        playerTwoPlayed = true;
	}

	public void playOpponentTwo()
	{		
		// Check all cards in hand for HIGHEST and LOWEST and HIGHEST NOT TRUMP
		int maxCard = 0;
		int indexMaxCard = 0;

		for (int i = 0; i != opponentTwoCards.Count; i++) 
		{
			string getCard = opponentTwoCards[i].name.ToString();
			if (getCardValue (getCard) >= maxCard) 
			{
				maxCard = getCardValue (getCard);
				indexMaxCard = i;	
			}
		}

		handCards.Add(opponentTwoCards[indexMaxCard]);
        playSpecificCardInHand(handCards.Count - 1);
        removeFromList(indexMaxCard, "opponentTwo");
        opponentTwoPlayed = true;
	}

	public void playPlayerOne()
	{
		if(cardIsPlayed== false)
		{
			//add card to hand


		}
		else
		{
			//try to make an infinate loop so it will wait for input
			cardIsPlayed = true;
		}

	}

    private void removePlayedCards()
    {
        //remove played cards from the players logical hands list
        foreach (GameObject ob in handCards)
        {
            for (int i = 0; i <= playerTwoCards.Count - 1; i++)
            {
                if (ob.name.ToString() == playerTwoCards[i].name.ToString())
                    removeFromList(i, "playerTwo");
            }
            for (int i = 0; i <= opponentOneCards.Count - 1; i++)
            {
                if (ob.name.ToString() == opponentOneCards[i].name.ToString())
                    removeFromList(i, "opponentOne");
            }
            for (int i = 0; i <= opponentTwoCards.Count - 1; i++)
            {
                if (ob.name.ToString() == opponentTwoCards[i].name.ToString())
                    removeFromList(i, "opponentTwo");
            }
        }
    }

    public void removeFromList(int index, string playerIdentifyer)
    {
        if (playerIdentifyer == "playerOne")
        {
            playerOneCards.RemoveAt(index);
        }
        if (playerIdentifyer == "playerTwo")
        {
            playerTwoCards.RemoveAt(index);
        }
        else if (playerIdentifyer == "opponentOne")
        {
            opponentOneCards.RemoveAt(index);
        }
        else if (playerIdentifyer == "opponentTwo")
        {
            opponentTwoCards.RemoveAt(index);
        }
    }

	private void checkForWinnerOfHand()
	{
        GameObject manager = GameObject.Find("_Manager");
        ScoreTracking score = manager.GetComponent<ScoreTracking>();

		int indexWinner = 0;
        int highestScore = Mathf.Max(getCardValue(handCards[0].name.ToString()), getCardValue(handCards[1].name.ToString()), getCardValue(handCards[2].name.ToString()), getCardValue(handCards[3].name.ToString()));

        for (int i = 0; i < handCards.Count - 1; i++)
        {
            if (getCardValue(handCards[i].name.ToString()) == highestScore)
            {
                indexWinner = i;
            }
        }
        
		if (indexWinner == 1) 
		{
            GameObject ob = Instantiate(opponentOneWinMessage, new Vector3(-400, 400, 0), Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.Find("Canvas").transform);
            ob.animation.Play();
            score.opponentOneWinsBeforeRedeal += 1;
		}
		if (indexWinner == 2) 
		{
            GameObject ob = Instantiate(playerTwoWinMessage, new Vector3(-400, 400, 0), Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.Find("Canvas").transform);
            ob.animation.Play();
            score.playerTwoWinsBeforeRedeal += 1;
		}
		if (indexWinner == 3) 
		{
            GameObject ob = Instantiate(opponentTwoWinMessage, new Vector3(-400, 400, 0), Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.Find("Canvas").transform);
            ob.animation.Play();
            score.playerTwoWinsBeforeRedeal += 1;
		}
		if (indexWinner == 0) 
		{
            GameObject ob = Instantiate(playerOneWinMessage, new Vector3(-400, 400, 0), Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.Find("Canvas").transform);
            ob.animation.Play();
            score.playerOneWinsBeforeRedeal += 1;
		}

	}

	//Goes to the left of dealer and sees if ever one wants to make trump
	public void countTrumpInHand()
	{
		if (kittyCards [0].name.Contains ("H")) {
			potentialTrump = "Hearts";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].name.Contains("H") || playerOneCards [i].name.Contains("jackD"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].name.Contains("H") || playerTwoCards [i].name.Contains("jackD"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].name.Contains("H")|| opponentOneCards [i].name.Contains("jackD"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].name.Contains("H") || opponentTwoCards [i].name.Contains("jackD"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		if (kittyCards [0].name.Contains ("C")) {
			potentialTrump = "Clubs";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].name.Contains("C")|| playerOneCards [i].name.Contains("jackS"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].name.Contains("C") || playerTwoCards [i].name.Contains("jackS"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].name.Contains("C")|| opponentOneCards [i].name.Contains("jackS"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].name.Contains("C") || opponentTwoCards [i].name.Contains("jackS"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		if (kittyCards [0].name.Contains ("S")) {
			potentialTrump = "Spades";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].name.Contains("S")|| playerOneCards [i].name.Contains("jackC"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].name.Contains("S")|| playerTwoCards [i].name.Contains("jackC"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].name.Contains("S")|| opponentOneCards [i].name.Contains("jackC"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].name.Contains("S") || opponentTwoCards [i].name.Contains("jackC"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		if (kittyCards [0].name.Contains ("D")){
			potentialTrump = "Dimonds";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].name.Contains("D")|| playerOneCards [i].name.Contains("jackH"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].name.Contains("D")|| playerTwoCards [i].name.Contains("jackH"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].name.Contains("D")|| opponentOneCards [i].name.Contains("jackH"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].name.Contains("D") || opponentTwoCards [i].name.Contains("jackH"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
	}


	public void pickUpKitty()
	{
		if(trackDealer == 0)
		{
			trumpButton.SetActive (false);
			passButton.SetActive (false);
			playerOneCards.Add(kittyCards[0]);
			kittyCards.Remove(kittyCards[0]);
			clearBoardNoReplaceKitty();

		}
        else if (trackDealer == 1)
        {
            opponentOneCards.Add(kittyCards[0]);
            opponentOneCards = computerDiscardCardAfterTakingKitty(opponentOneCards);
        }
        else if (trackDealer == 2)
        {
            playerTwoCards.Add(kittyCards[0]);
            playerTwoCards = computerDiscardCardAfterTakingKitty(playerTwoCards);
        }
        else if (trackDealer == 3)
        {
            opponentTwoCards.Add(kittyCards[0]);
            opponentTwoCards = computerDiscardCardAfterTakingKitty(opponentTwoCards);
        }
	}

    private List<GameObject> computerDiscardCardAfterTakingKitty(List<GameObject> cardList)
    {
        int worstCardIndex = 0;
        
        //we are assuming since they just took the kitty card they will have 6 cards.
        int lowestScore = Mathf.Min(getCardValue(cardList[0].name.ToString()),
                                     getCardValue(cardList[1].name.ToString()),
                                     getCardValue(cardList[2].name.ToString()),
                                     getCardValue(cardList[3].name.ToString()),
                                     getCardValue(cardList[4].name.ToString()),
                                     getCardValue(cardList[5].name.ToString()));


        for (int i = 0; i <= cardList.Count - 1; i++)
        {
			if (getCardValue(cardList[i].name.ToString()) == lowestScore)
            {
                worstCardIndex = i;
            }
        }

        cardList.RemoveAt(worstCardIndex);
        return cardList;
    }

	public void clearBoardNoReplaceKitty()
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
		
		displayAllNoKitty();
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
		
		displayAllCard();
	}

	public void clearBoardCardsWithHandToScreen()
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
	}

	public void trumpButtonClicked()
	{
		trumpMade = true;
		playerOneTrumpMade = true;
		currentTrump = potentialTrump;
		trumpText.text = "Trump is: " + potentialTrump;
		pickUpKitty();
	}

	public void passButtonClicked()
	{
		passMade = true;
		clearBoardNoReplaceKitty ();
        opponentOneTrumpSecondTurn = true;
		playerOneTrumpTurn = false;
	}

	public void playerOneMakeTrump()
	{

		if(playerOneTrumpTurn == true)
		{

			trumpButton.SetActive (true);
			passButton.SetActive (true);
			if (playerOneTrumpMade == true) 
			{
				playerOneTrumpMade = false;
				trumpSelectionFinished = true;
                whoMadeTrump = "playerOne";

				updateFeed ("PLEASE DISCARD");
				updateFeed ("YOU HAVE MADE TRUMP");
				print("YOU HAVE MADE TRUMP");

				clearBoardNoReplaceKitty();

			}
			if (passMade == true) 
			{
				updateFeed ("YOU HAVE PASSED");
				print("YOU HAVE PASSED");
				//If playerOne is dealer when he passes it will be the second time to be trump
				if(trackDealer == 0)
				{
					opponentOneTrumpSecondTurn = true;
					playerOneTrumpTurn = false;
				}

				// If playerOne is NOT dealer it may or may not be second round to make trump
				if(trackDealer != 0)
				{
					// figure out how many times the button has been clicked of flag or some thing
					//NEED LOGIC!!!!!!!!!!!
				}
			}
		}
		if (playerOneTrumpSecondTurn == true) 
		{
			trumpButton.SetActive (false);
			passButton.SetActive (false);
			heartsButton.SetActive(true);
			dimondsButton.SetActive (true);
			clubsButton.SetActive (true);
			spadesButton.SetActive (true);
			redealButton.SetActive (true);

		}


	}

	public void opponentOneMakeTrump()
	{
		if (opponentOneTrumpTurn == true) 
		{
	

			if(opponentOneNumOfTrump>=3)
			{
				updateFeed("Opponent 1 Made Trump as " + potentialTrump);
                whoMadeTrump = "opponentOne";
				currentTrump = potentialTrump;
				trumpText.text = "Trump is: " + potentialTrump;
				pickUpKitty();
				opponentOneTrumpTurn = false;
				trumpSelectionFinished = true;
			}
			else
			{
				updateFeed("Opponent 1 passed!");
				print("Opponent 1 passed!");
				playerTwoTrumpTurn = true;
				opponentOneTrumpTurn = false;

			}
		}
        
		if (opponentOneTrumpSecondTurn == true)
        {
			newTrumpHearts =0;
			newTrumpDiamonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;

			for (int i = 0; i != opponentOneCards.Count - 1; i++) {
				
				if (opponentOneCards[i].name.Contains ("H") || opponentOneCards [i].name.Contains ("jackD")) {
					newTrumpHearts++;
				}
				if (opponentOneCards [i].name.Contains ("D") || opponentOneCards [i].name.Contains ("jackH")) {
					newTrumpDiamonds++;
				}
				if (opponentOneCards [i].name.Contains ("C") || opponentOneCards [i].name.Contains ("jackS")) {
					newTrumpClubs++;
				}
				if (opponentOneCards [i].name.Contains ("S") || opponentOneCards [i].name.Contains ("jackC")) {
					newTrumpSpades++;
				}	
			}
			if (newTrumpHearts >= 3)
			{
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
                whoMadeTrump = "opponentOne";

				print ("Opponent 1 made Hearts");
				opponentOneTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;;
			}
			if (newTrumpDiamonds >= 3)
			{
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
                whoMadeTrump = "opponentOne";
				print ("Opponent 1 made Dimonds");
				opponentOneTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpClubs >= 3)
			{
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
                whoMadeTrump = "opponentOne";
				print ("Opponent 1 made Clubs");
				opponentOneTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpSpades >= 3)
			{
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
                whoMadeTrump = "opponentOne";
				print ("Opponent 1 made Spades");
				opponentOneTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (trumpSelectionFinished == false && cardIsDiscarded == false)
			{
				updateFeed("Opponent 1 passed AGAIN!");
				print("Opponent 1 passed AGAIN!");
				playerTwoTrumpSecondTurn = true;
				opponentOneTrumpSecondTurn = false;
			}
        }


	}

	public void opponentTwoMakeTrump()
	{
		if (opponentTwoTrumpTurn == true) 
		{
			if(opponentTwoNumOfTrump>=3)
			{
				updateFeed("Opponent 2 Made Trump as " + potentialTrump);
				currentTrump = potentialTrump;
                whoMadeTrump = "opponentTwo";
				trumpText.text = "Trump is: " + potentialTrump;
				pickUpKitty();
				opponentTwoTrumpTurn = false;
				trumpSelectionFinished = true;
			}
			else
			{
				updateFeed("Opponent 2 passed!");
				print("Opponent 2 passed!");
				playerOneTrumpTurn = true;
				opponentTwoTrumpTurn = false;	
			}
		}
		
		if (opponentTwoTrumpSecondTurn == true)
		{
			newTrumpHearts =0;
			newTrumpDiamonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;

            for (int i = 0; i != opponentTwoCards.Count - 1; i++)
            {
				
				if (opponentTwoCards[i].name.Contains ("H") || opponentTwoCards [i].name.Contains ("jackD")) {
					newTrumpHearts++;
				}
				if (opponentTwoCards [i].name.Contains ("D") || opponentTwoCards [i].name.Contains ("jackH")) {
					newTrumpDiamonds++;
				}
				if (opponentTwoCards [i].name.Contains ("C") || opponentTwoCards [i].name.Contains ("jackS")) {
					newTrumpClubs++;
				}
				if (opponentTwoCards [i].name.Contains ("S") || opponentTwoCards [i].name.Contains ("jackC")) {
					newTrumpSpades++;
				}	
			}
			if (newTrumpHearts >= 3)
			{
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "opponentTwo";
				updateFeed ("Opponent 2 made trump");
				print("Opponent 2 made Hearts");
				opponentTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpDiamonds >= 3)
			{
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "opponentTwo";
				updateFeed ("Opponent 2 made trump");
				print("Opponent 2 made Dimonds");
				opponentTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpClubs >= 3)
			{
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "opponentTwo";
				updateFeed ("Opponent 2 made trump");
				print("Opponent 2 made Clubs");
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpSpades >= 3)
			{
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "opponentTwo";
				updateFeed ("Opponent 2 made trump");
				print("Opponent 2 made Spades");
				opponentTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (trumpSelectionFinished == false && cardIsDiscarded == false)
			{
				updateFeed ("Opponent 2 passed AGAIN!");
				print("Opponent 2 passed AGAIN!");
				playerOneTrumpSecondTurn = true;
				opponentTwoTrumpSecondTurn = false;
			}
		}

	}

	public void playerTwoMakeTrump()
	{
		if (playerTwoTrumpTurn == true) 
		{
			if(playerTwoNumOfTrump>=3)
			{
				updateFeed("Partner Made Trump as " + potentialTrump);
                whoMadeTrump = "playerTwo";
				currentTrump = potentialTrump;
				trumpText.text = "Trump is: " + potentialTrump;
				pickUpKitty();
				playerTwoTrumpTurn = false;
				trumpSelectionFinished = true;
			}
			else
			{
				updateFeed("Your partner passed!");
				print("Player 2 passed!");
				opponentTwoTrumpTurn = true;
				playerTwoTrumpTurn = false;	
			}
		}
		
		if (playerTwoTrumpSecondTurn == true)
		{
			newTrumpHearts =0;
			newTrumpDiamonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;
			
			for (int i = 0; i != playerTwoCards.Count - 1; i++) {
				
				if (playerTwoCards[i].name.Contains ("H") || playerTwoCards [i].name.Contains ("jackD")) {
					newTrumpHearts++;
				}
				if (playerTwoCards [i].name.Contains ("D") || playerTwoCards [i].name.Contains ("jackH")) {
					newTrumpDiamonds++;
				}
				if (playerTwoCards [i].name.Contains ("C") || playerTwoCards [i].name.Contains ("jackS")) {
					newTrumpClubs++;
				}
				if (playerTwoCards [i].name.Contains ("S") || playerTwoCards [i].name.Contains ("jackC")) {
					newTrumpSpades++;
				}	
			}
			if (newTrumpHearts >= 3)
			{
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "playerTwo";
				updateFeed ("Partner made trump");
				print("Your Partner made Hearts");
				playerTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpDiamonds >= 3)
			{
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "playerTwo";
				updateFeed ("Partner made trump");
				print("Your Partner made Dimonds");
				playerTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpClubs >= 3)
			{
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "playerTwo";
				updateFeed ("Partner made trump");
				print("Your Partner made Clubs");
				playerTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (newTrumpSpades >= 3)
			{
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
                whoMadeTrump = "playerTwo";
				updateFeed ("Partner made trump");
				print("Your Partner made Spades");
				playerTwoTrumpSecondTurn = false;
				cardIsDiscarded = true;
				trumpSelectionFinished = true;
			}
			if (trumpSelectionFinished == false)
			{
				updateFeed("Your Partner passed AGAIN!");
				print("Your Partner passed AGAIN!");

				opponentTwoTrumpSecondTurn = true;
				playerTwoTrumpSecondTurn = false;
			}
		}
	}


    public void updateFeed(string input)
    {

        if (line1 == "" && line2 == "" && line3 == "")
        {
            line1 = input;
        }
        else if(line1 != "" && line2 == "" && line3 == "")
        {
            line2 = input;
        }
        else if (line1 != "" && line2 != "" && line3 == "")
        {
            line3 = input;
        }
        else if (line1 != "" && line2 != "" && line3 != "")
        {
            line2 = line1;
            line3 = line2;
            line1 = input;
        }

        feed.text = line1 + "\n" + line2 + "\n" + line3 + "\n";

    }

	public void makeTrumpFromKitty()
	{
		if(trackDealer == 0)
		{
			opponentOneTrumpTurn = true;
		}
		if(trackDealer == 1)
		{
			playerTwoTrumpTurn = true;
		}
		if(trackDealer == 2)
		{
			opponentTwoTrumpTurn = true;
		}
		if(trackDealer == 3)
		{
			playerOneTrumpTurn = true;
		}
	}


	public void displayAllCard()
	{
		//team one (This is the BOTTOM and TOP of screen)
		cardToScreenPlayerOne(-600, -600);
		cardToScreenPlayerTwo(-600, 600);


		//team two (This is the LEFT and RIGHT of screen)
		cardToScreenOpponent(-1000, 600);
		cardToScreenOpponentTwo(1000, 600);

		//Kitty in the middle
		cardToScreenKitty(-600, 0);
	}

	public void displayAllNoKitty()
	{
		//team one (This is the BOTTOM and TOP of screen)
		cardToScreenPlayerOne(-600, -600);
		cardToScreenPlayerTwo(-600, 600);	
		
		//team two (This is the LEFT and RIGHT of screen)
		cardToScreenOpponent(-1000, 600);
		cardToScreenOpponentTwo(1000, 600);
	}

    public void playSpecificCardInHand(int indexOfCard)
    {
        //print("playspecificcardinhand playing");
        //print(handCards.Count + " is total number of cards in hand");
        //print(indexOfCard.ToString() + " is index used");
        string card = handCards[indexOfCard].name.ToString() + "(Clone)";
        GameObject manager = GameObject.Find(card);
        //print(manager);
        Animate ani = manager.GetComponent<Animate>();
        ani.moveCardIntoPosition();
    }

	private int getCardValue(string card)
	{
		int cardValue = 0;
		char letter = currentTrump[0];
		string firstLetter = letter.ToString ();
		
		if(card.Contains("nine"))
		{
			cardValue = 9;
			if (card.Contains(firstLetter))
			{
				cardValue= cardValue +10;
			}
		}
		if(card.Contains("ten"))
		{
			cardValue = 10;
			if (card.Contains(firstLetter))
			{
				cardValue= cardValue +10;
			}
		}
		if (card.Contains("jack"))
		{
			cardValue = 11;
			if (firstLetter == "H")
			{
				if (card.Contains ("H"))
				{
					cardValue= cardValue +20;
				}
				if (card.Contains ("D"))
				{
					cardValue= cardValue +15;
				}
			}
			if (firstLetter == "D")
			{
				if (card.Contains ("D"))
				{
					cardValue= cardValue +20;
				}
				if (card.Contains ("H"))
				{
					cardValue= cardValue +15;
				}
			}
			if (firstLetter == "C")
			{
				if (card.Contains ("C"))
				{
					cardValue= cardValue +20;
				}
				if (card.Contains ("S"))
				{
					cardValue= cardValue +15;
				}
			}
			if (firstLetter == "S")
			{
				if (card.Contains ("S"))
				{
					cardValue= cardValue +20;
				}
				if (card.Contains ("C"))
				{
					cardValue= cardValue +15;
				}
			}
			
		}
		if(card.Contains("queen"))
		{
			cardValue = 12;
			if (card.Contains(firstLetter))
			{
				cardValue= cardValue +10;
			}
		}
		if(card.Contains("king"))
		{
			cardValue = 13;			
			if (card.Contains(firstLetter))
			{
				cardValue= cardValue +10;
			}
		}
		if(card.Contains("ace"))
		{
			cardValue = 14;
			if (card.Contains(firstLetter))
			{
				cardValue= cardValue +10;
			}
		}
		
		return cardValue;
	}
	

	public void cardToScreenPlayerOne(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
		
		for (int i = 0; i != playerOneCards.Count; i++) {

            xPosition = xPosition + 160;
			#region switch statement player one
			switch (playerOneCards [i].name.ToString()) {

				//Hearts
			case "nineH":
				objectsOnScreen.Add(Instantiate(deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f, 
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f, 
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);				
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);;
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			}
			#endregion
		}
		
	}
	
	public void cardToScreenOpponent(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
		
		for (int i = 0; i != opponentOneCards.Count; i++) {

            yPosition = yPosition - 160;
			#region switch statement opponent
			switch (opponentOneCards [i].name.ToString()) {

				//Hearts
			case "nineH":
                objectsOnScreen.Add(Instantiate(deckobjects.nineH, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceS":
                objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			}
			#endregion
			
		}
	}

	public void cardToScreenPlayerTwo(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
		
		for (int i = 0; i != playerTwoCards.Count; i++) {

            xPosition = xPosition + 160;
			#region switch statement player one
			switch (playerTwoCards [i].name.ToString()) {

				//Hearts
			case "nineH":
				objectsOnScreen.Add(Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			}
			#endregion
		}
		
	}
	
	public void cardToScreenOpponentTwo(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
		
		for (int i = 0; i != opponentTwoCards.Count; i++) {

            yPosition = yPosition - 160;
			#region switch statement opponent
			switch (opponentTwoCards [i].name.ToString()) {
	
				//Hearts
			case "nineH":
				objectsOnScreen.Add(Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceD":
                objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);				
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceS":
                objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			}
			#endregion
			
		}
	}

	public void cardToScreenKitty(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();

        xPosition = xPosition + 160;
			#region switch statement opponent
			switch (kittyCards [0].name.ToString()) {

			//Hearts
			case "nineH":
				objectsOnScreen.Add(Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);				
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				break;
			}
			#endregion
	}
}