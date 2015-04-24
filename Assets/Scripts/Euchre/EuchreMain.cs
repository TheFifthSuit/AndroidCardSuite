using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EuchreMain : MonoBehaviour {
	
    //variables used in functions
	string lastCardPlayedPlayerOne;
	string lastCardPlayedOpponent;
	public Text playerScoreText;
	public Text opponentScoreText;
	public Text trumpText;
	public string lastHandWinner = "";

	int newTrumpHearts= 0;
	int newTrumpDimonds= 0;
	int newTrumpClubs= 0;
	int newTrumpSpades= 0;
	int firstPlay = 0;

	public bool cardIsDiscarded = true;
	public bool cardIsPlayed = true;

	// string of the cards played or dicarded
	public string discard;
	public string playCard;

	bool trumpMade = false;
	bool playerOneTrumpMade = false;
	bool passMade = false;
	
	// will be used to hide and show buttons
	public GameObject trumpButton;
	public GameObject passButton;
	public GameObject discardButton;
	public GameObject heartsButton;
	public GameObject dimondsButton;
	public GameObject clubsButton;
	public GameObject spadesButton;
	public GameObject redealButton;
	public GameObject playCardToHand;
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
    List<GameObject> handCards = new List<GameObject>();
	
	//list for "table" or the people setting to determine the hand
	int tablePosition = 1;
	

	// Use this for initialization
	void Start () 
	{
        fillCardDeckObjects();
        
        //only pops up when trump is made first round
		discardButton.SetActive(false);

		//second round get to choose trump with butons or redeal
		trumpButton.SetActive (false);
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);

		//starts the game
		dealPlayerCards();
		displayAllCard();
		//displayAllWithHand ();
		countTrumpInHand();
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

		updateFeed ("PLAYING");
		if (firstPlay == 0)
		{
			cardIsDiscarded= true;
			cardIsPlayed = false;
            addCardsToHand();
			//clearBoardCardsWithHandToScreen();
			firstPlay++;
		}
	}


	public void trumpHeartsClicked()
	{
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);
		currentTrump = "Hearts";
		trumpText.text = "Trump is: " + currentTrump;
		playGame ();

	}

	public void trumpClubsClicked()
	{
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);
		currentTrump = "Clubs";
		trumpText.text = "Trump is: " + currentTrump;
		playGame ();
		
	}
	public void trumpSpadessClicked()
	{
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);
		currentTrump = "Spades";
		trumpText.text = "Trump is: " + currentTrump;
		playGame ();
		
	}
	public void trumpDimondsClicked()
	{
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);
		currentTrump = "Dimonds";
		trumpText.text = "Trump is: " + currentTrump;
		playGame ();
	}

    public void addCardsToHand()
    {
        handCards.Add(opponentOneCards[0]);
        handCards.Add(playerTwoCards[0]);
        handCards.Add(opponentTwoCards[0]);
    }

    public void removePlayedCards()
    {
        opponentOneCards.Remove(opponentOneCards[0]);
        playerTwoCards.Remove(playerTwoCards[0]);
        opponentTwoCards.Remove(opponentTwoCards[0]);
    }

	public void makeTrumpAgain()
	{
		
		GameObject manager = GameObject.Find("_Manager");
		ChangeScene changescene = manager.GetComponent<ChangeScene>();
		if (trumpMade == false) {
			newTrumpHearts =0;
			newTrumpDimonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;

			for (int i = 0; i != 5; i++) {

				if (opponentOneCards[i].name.Contains ("H") || opponentOneCards [i].name.Contains ("jackD")) {
					newTrumpHearts++;
				}
				if (opponentOneCards [i].name.Contains ("D") || opponentOneCards [i].name.Contains ("jackH")) {
					newTrumpDimonds++;
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
				trumpMade = true;
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
				playGame();
			}
			if (newTrumpDimonds >= 3)
			{
				trumpMade = true;
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
				playGame();
			}
			if (newTrumpClubs >= 3)
			{
				trumpMade = true;
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
				playGame();
			}
			if (newTrumpSpades >= 3)
			{
				trumpMade = true;
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent 1 made trump");
				playGame();
			}
		}
		if (trumpMade == false)
		{
			newTrumpHearts =0;
			newTrumpDimonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;
			updateFeed ("Opponent 1 PASSED!!");

			for (int i = 0; i != 5; i++) {
				if (playerTwoCards [i].name.Contains("H")|| playerTwoCards [i].name.Contains("jackD"))
				{
					newTrumpHearts++;
				}
				if (playerTwoCards [i].name.Contains("D")|| playerTwoCards [i].name.Contains("jackH"))
				{
					newTrumpDimonds++;
				}
				if (playerTwoCards [i].name.Contains("C")|| playerTwoCards [i].name.Contains("jackS"))
				{
					newTrumpClubs++;
				}
				if (playerTwoCards [i].name.Contains("S")|| playerTwoCards [i].name.Contains("jackC"))
				{
					newTrumpSpades++;
				}

			}
			if (newTrumpHearts >= 3)
			{
				trumpMade = true;
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Partner made trump");
				playGame();
			}
			if (newTrumpDimonds >= 3)
			{
				trumpMade = true;
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Partner made trump");
				playGame();
			}
			if (newTrumpClubs >= 3)
			{
				trumpMade = true;
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Partner made trump");
				playGame();
			}
			if (newTrumpSpades >= 3)
			{
				trumpMade = true;
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Partner made trump");
				playGame();
			}
		}


		if (trumpMade == false)
		{
			newTrumpHearts =0;
			newTrumpDimonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;
			updateFeed ("Your Partner PASSED!!");
			
			for (int i = 0; i != 5; i++) {
				if (opponentTwoCards [i].name.Contains("H")|| opponentTwoCards [i].name.Contains("jackD"))
				{
					newTrumpHearts++;
				}
				if (opponentTwoCards [i].name.Contains("D")|| opponentTwoCards [i].name.Contains("jackH"))
				{
					newTrumpDimonds++;
				}
				if (opponentTwoCards [i].name.Contains("C")|| opponentTwoCards [i].name.Contains("jackS"))
				{
					newTrumpClubs++;
				}
				if (opponentTwoCards [i].name.Contains("S")|| opponentTwoCards [i].name.Contains("jackC"))
				{
					newTrumpSpades++;
				}
				
			}
			if (newTrumpHearts >= 3)
			{
				trumpMade = true;
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent Two made trump");
				playGame();
			}
			if (newTrumpDimonds >= 3)
			{
				trumpMade = true;
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent Two made trump");
				playGame();
			}
			if (newTrumpClubs >= 3)
			{
				trumpMade = true;
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent Two made trump");
				playGame();
			}
			if (newTrumpSpades >= 3)
			{
				trumpMade = true;
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				updateFeed ("Opponent Two made trump");
				playGame();
			}
		}

		if (trumpMade == false) {
			newTrumpHearts = 0;
			newTrumpDimonds = 0;
			newTrumpClubs = 0;
			newTrumpSpades = 0;
			updateFeed ("Opponent 2 PASSED!!");
			updateFeed ("Its up to you");
			heartsButton.SetActive(true);
			dimondsButton.SetActive (true);
			clubsButton.SetActive(true);
			spadesButton.SetActive (true);
			redealButton.SetActive (true);
		}
	}
	
	private void checkForWinnerOfHand()
	{
		GameObject manager = GameObject.Find("_Manager");
		ChangeScene changescene = manager.GetComponent<ChangeScene>();
		
		//Check for winner before turn is over
		clearBoardCardsWithHandToScreen ();
		
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
		makeTrumpFromKitty ();
	}

	public void removeToKitty(string CardClicked)
	{	
		if (cardIsDiscarded == false)
		{
		    discardButton.SetActive(true);
		    discard = CardClicked;
			cardIsDiscarded =true;

		}
	}

	public void removeCardToKitty(string CardClicked)
	{
		discard = discard.Replace ("(Clone)", "");
		//playerOneCards.Remove (discard.ToString();
		clearBoardNoReplaceKitty ();
		discardButton.SetActive(false);
		playGame();
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
		
		playEveryoneElsesCards ();
	}

	public void trumpButtonClicked()
	{
		playerOneTrumpMade = true;
		trumpMade = true;
		currentTrump = potentialTrump;
		trumpText.text = "Trump is: " + potentialTrump;
		updateFeed ("YOU HAVE MADE TRUMP");
		cardIsDiscarded = false;
		pickUpKitty();

	}

	public void passButtonClicked()
	{
		passMade = true;
		updateFeed ("YOU HAVE PASSED");
		trumpButton.SetActive (false);
		passButton.SetActive (false);
		clearBoardNoReplaceKitty ();
		makeTrumpAgain ();
	}

	public void playerOneMakeTrump()
	{
		trumpButton.SetActive (true);
		if (playerOneTrumpMade == true) 
		{
			updateFeed ("YOU HAVE MADE TRUMP");

		}
		if (passMade == true) 
		{
			updateFeed ("YOU HAVE PASSED");
		}

	}

	public void opponentOneMakeTrump()
	{
		if (opponentOneNumOfTrump >= 3) {
			updateFeed("Opponent 1 Made Trump as " + potentialTrump);
			trumpMade = true;
			currentTrump = potentialTrump;
			trumpText.text = "Trump is: " + potentialTrump;
			cardIsDiscarded = false;
			pickUpKitty();
		} 
		else 
		{
			updateFeed("Opponent 1 Passed!!");
		}

	}

	public void opponentTwoMakeTrump()
	{
		if (opponentOneNumOfTrump >= 3) {
			updateFeed("Opponent 2 Made Trump as " + potentialTrump);
			trumpMade = true;
			currentTrump = potentialTrump;
			trumpText.text = "Trump is: " + potentialTrump;
			cardIsDiscarded = false;
			pickUpKitty();

		} 
		else 
		{
			updateFeed("Opponent 2 Passed!!");
		}
		
	}

	public void playerTwoMakeTrump()
	{
		if (playerTwoNumOfTrump >= 3) {
			updateFeed("Your Partner Made Trump as " + potentialTrump);
			trumpMade = true;
			currentTrump = potentialTrump;
			trumpText.text = "Trump is: " + potentialTrump;
			cardIsDiscarded = false;
			pickUpKitty();
		} 
		else 
		{
			updateFeed("Your Partner Passed!!");
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
			opponentOneMakeTrump();
			if(trumpMade == false)
			{
				playerTwoMakeTrump();
			}
			if(trumpMade == false)
			{
				opponentTwoMakeTrump();
			}
			if(trumpMade == false)
			{
				playerOneMakeTrump();
			}
		}
		if(trackDealer == 1)
		{
			opponentOneMakeTrump();
			playerTwoMakeTrump();
			opponentTwoMakeTrump();
		}
		if(trackDealer == 2)
		{
			opponentOneMakeTrump();
			playerTwoMakeTrump();
			opponentTwoMakeTrump();
		}
		if(trackDealer == 3)
		{
			opponentOneMakeTrump();
			playerTwoMakeTrump();
			opponentTwoMakeTrump();
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

	public void playEveryoneElsesCards()
	{
        //need to move all cards into position here after player plays there card call this from animate.
        foreach (GameObject ob in handCards)
        {
            string card = ob.name.ToString() + "(Clone)";
            GameObject manager = GameObject.Find(card);
            print(manager);
            Animate ani = manager.GetComponent<Animate>();
            ani.moveCardIntoPosition();
        }
        
        //removes cards from players cards that they have list
        removePlayedCards();
	}

	private int getCardValue(string card)
	{
		int cardValue = 0;

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
                lastCardPlayedPlayerOne = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);				
                lastCardPlayedPlayerOne = "kingD";
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceD";
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceC";
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingS";
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
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
                lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceD";
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceC";
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
                objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, 90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceS";
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
                lastCardPlayedPlayerOne = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingD";
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceD";
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceC";
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "kingS";
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, 0)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedPlayerOne = "aceS";
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
                lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
                objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceD";
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceC";
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);				
                lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
                objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3(xPosition, yPosition, 0), Quaternion.Euler(0, 180, -90)) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceS";
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
                lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceD";
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);				
                lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceC";
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);
                objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale = new Vector3(objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.x * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.y * 0.75f,
                                                                                              objectsOnScreen[objectsOnScreen.Count - 1].transform.localScale.z * 1f);
                lastCardPlayedOpponent = "aceS";
				break;
			}
			#endregion
			

	}
}