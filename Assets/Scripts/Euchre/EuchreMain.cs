using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EuchreMain : MonoBehaviour {
<<<<<<< HEAD
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

	
	//tracking total and how many cards are in the hand
	int playerOneTotal = 0;
	int playerOneIndex = 0;
	
	int opponentTotal = 0;
	int opponentIndex = 0;

	public string discard;

	//
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

	public GameObject partnerText;
	public GameObject opponentOneText;
	public GameObject opponentTwoText;



	public List<GameObject> objectsOnScreen = new List<GameObject>();
	//card names within deck should match Prefab names should have seperate prefab for all 24 cards
	string[] cardDeck = {"nineH", "tenH", "jackH", "queenH", "kingH", "aceH", 
		"nineD", "tenD", "jackD", "queenD", "kingD", "aceD", 
		"nineC", "tenC", "jackC", "queenC", "kingC", "aceC",
		"nineS", "tenS", "jackS", "queenS", "kingS", "aceS"};
	
	//Track the Dealer
	int trackDealer = 0;

	//empty string that will be later filled with the first card in the Kitty
	string potentialTrump;
	string currentTrump;

	//list of cards delt
	List<int> dealtCards = new List<int>();

	//number of Trump for each player
	int playerOneNumOfTrump= 0;
	int playerTwoNumOfTrump= 0;
	int opponentOneNumOfTrump= 0;
	int opponentTwoNumOfTrump= 0;

	//players list of current cards
	List<string> playerOneCards = new List<string>();
	List<string> playerTwoCards = new List<string>();
	
	//opponents list of current cards
	List<string> opponentOneCards = new List<string>();
	List<string> opponentTwoCards = new List<string>();
	
	//list for the kitty
	List<string> kittyCards = new List<string>();

	//list for each hand
	List<string> handCards = new List<string>();
	
	//list for "table" or the people setting to determine the hand
	int tablePosition = 1;
	

	// Use this for initialization
	void Start () 
	{
		discardButton.SetActive(false);
		trumpButton.SetActive (false);
		heartsButton.SetActive(false);
		dimondsButton.SetActive (false);
		clubsButton.SetActive(false);
		spadesButton.SetActive (false);
		redealButton.SetActive (false);
		partnerText.SetActive (false);
		opponentOneText.SetActive (false);
		opponentTwoText.SetActive (false);
		dealPlayerCards();      
		displayAllCard();
		countTrumpInHand();	
	}

	// deals 5 random cards to all players left overs go in the Kitty randomized
	private void dealPlayerCards ()
	{	
		for (int i = 0; i <= 4; i++) 
		{
			int drawnCard = Random.Range(0, 24);

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
		
		for (int i = 0; i <= 4; i++) 
		{
			int drawnCard = Random.Range(0, 24);
			
=======


    //variables used in functions
    string lastCardPlayedPlayerOne;
    string lastCardPlayedOpponent;
    public Text playerScoreText;
    public Text opponentScoreText;
    public string lastHandWinner = "";
    
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

>>>>>>> f9ca9b539ed3444388666804bb67b0504376cebc
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
				opponentOneCards.Add(cardDeck[drawnCard]);
				dealtCards.Add(drawnCard);
			}
			else
			{
				i--;
			}
		}

<<<<<<< HEAD
		
		for (int i = 0; i <= 4; i++) 
=======
		for (int i = 0; i <= 25; i++) 
>>>>>>> f9ca9b539ed3444388666804bb67b0504376cebc
		{
			int drawnCard = Random.Range(0, 24);
			
			if(dealtCards.IndexOf(drawnCard) == -1)
			{
				playerTwoCards.Add(cardDeck[drawnCard]);
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
				opponentTwoCards.Add(cardDeck[drawnCard]);
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
				kittyCards.Add(cardDeck[drawnCard]);
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
		partnerText.SetActive (true);
		opponentOneText.SetActive (true);
		opponentTwoText.SetActive (true);

		print ("PLAYING");
		if (firstPlay == 0)
		{
			playOpponentOne();
			playPlayerTwo();
			playOpponentTwo();
			clearBoardCardsWithHandToScreen();
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



	public void playOpponentOne()
	{
		handCards.Insert (0, opponentOneCards [0]);
		opponentOneCards.Remove (opponentOneCards [0]);

	}

	public void playPlayerTwo()
	{
		handCards.Insert (1, playerTwoCards [0]);
		playerTwoCards.Remove (playerTwoCards [0]);

	}

	public void playOpponentTwo()
	{
		handCards.Insert (2, opponentTwoCards [0]);
		opponentTwoCards.Remove (opponentTwoCards [0]);

	}
<<<<<<< HEAD
	
	public void makeTrumpAgain()
	{
		
		GameObject manager = GameObject.Find("_Manager");
		//ChangeScene changescene = manager.GetComponent<ChangeScene>();
		if (trumpMade == false) {
			newTrumpHearts =0;
			newTrumpDimonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;

			for (int i = 0; i != 5; i++) {

				if (opponentOneCards [i].Contains ("H") || opponentOneCards [i].Contains ("jackD")) {
					newTrumpHearts++;
				}
				if (opponentOneCards [i].Contains ("D") || opponentOneCards [i].Contains ("jackH")) {
					newTrumpDimonds++;
				}
				if (opponentOneCards [i].Contains ("C") || opponentOneCards [i].Contains ("jackS")) {
					newTrumpClubs++;
				}
				if (opponentOneCards [i].Contains ("S") || opponentOneCards [i].Contains ("jackC")) {
					newTrumpSpades++;
				}

			}
			if (newTrumpHearts >= 3)
			{
				trumpMade = true;
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpDimonds >= 3)
			{
				trumpMade = true;
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpClubs >= 3)
			{
				trumpMade = true;
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpSpades >= 3)
			{
				trumpMade = true;
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
		}
		if (trumpMade == false)
		{
			newTrumpHearts =0;
			newTrumpDimonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;
			print ("Opponent 1 PASSED!!");

			for (int i = 0; i != 5; i++) {
				if (playerTwoCards [i].Contains("H")|| playerTwoCards [i].Contains("jackD"))
				{
					newTrumpHearts++;
				}
				if (playerTwoCards [i].Contains("D")|| playerTwoCards [i].Contains("jackH"))
				{
					newTrumpDimonds++;
				}
				if (playerTwoCards [i].Contains("C")|| playerTwoCards [i].Contains("jackS"))
				{
					newTrumpClubs++;
				}
				if (playerTwoCards [i].Contains("S")|| playerTwoCards [i].Contains("jackC"))
				{
					newTrumpSpades++;
				}

			}
			if (newTrumpHearts >= 3)
			{
				trumpMade = true;
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpDimonds >= 3)
			{
				trumpMade = true;
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpClubs >= 3)
			{
				trumpMade = true;
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpSpades >= 3)
			{
				trumpMade = true;
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
		}


		if (trumpMade == false)
		{
			newTrumpHearts =0;
			newTrumpDimonds=0;
			newTrumpClubs=0;
			newTrumpSpades=0;
			print ("Your Partner PASSED!!");
			
			for (int i = 0; i != 5; i++) {
				if (opponentTwoCards [i].Contains("H")|| opponentTwoCards [i].Contains("jackD"))
				{
					newTrumpHearts++;
				}
				if (opponentTwoCards [i].Contains("D")|| opponentTwoCards [i].Contains("jackH"))
				{
					newTrumpDimonds++;
				}
				if (opponentTwoCards [i].Contains("C")|| opponentTwoCards [i].Contains("jackS"))
				{
					newTrumpClubs++;
				}
				if (opponentTwoCards [i].Contains("S")|| opponentTwoCards [i].Contains("jackC"))
				{
					newTrumpSpades++;
				}
				
			}
			if (newTrumpHearts >= 3)
			{
				trumpMade = true;
				currentTrump= "Hearts";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpDimonds >= 3)
			{
				trumpMade = true;
				currentTrump= "Dimonds";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpClubs >= 3)
			{
				trumpMade = true;
				currentTrump= "Clubs";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
			if (newTrumpSpades >= 3)
			{
				trumpMade = true;
				currentTrump= "Spades";
				trumpText.text = "Trump is: " + currentTrump;
				playGame();
			}
		}

		if (trumpMade == false) {
			newTrumpHearts = 0;
			newTrumpDimonds = 0;
			newTrumpClubs = 0;
			newTrumpSpades = 0;
			print ("Opponent 2 PASSED!!");
			print ("Its up to you");
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
		
	}

	//Goes to the left of dealer and sees if ever one wants to make trump
	public void countTrumpInHand()
	{
		if (kittyCards [0].Contains ("H")) {
			potentialTrump = "Hearts";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].Contains("H") || playerOneCards [i].Contains("jackD"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].Contains("H") || playerTwoCards [i].Contains("jackD"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].Contains("H")|| opponentOneCards [i].Contains("jackD"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].Contains("H") || opponentTwoCards [i].Contains("jackD"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		if (kittyCards [0].Contains ("C")) {
			potentialTrump = "Clubs";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].Contains("C")|| playerOneCards [i].Contains("jackS"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].Contains("C") || playerTwoCards [i].Contains("jackS"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].Contains("C")|| opponentOneCards [i].Contains("jackS"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].Contains("C") || opponentTwoCards [i].Contains("jackS"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		if (kittyCards [0].Contains ("S")) {
			potentialTrump = "Spades";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].Contains("S")|| playerOneCards [i].Contains("jackC"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].Contains("S")|| playerTwoCards [i].Contains("jackC"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].Contains("S")|| opponentOneCards [i].Contains("jackC"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].Contains("S") || opponentTwoCards [i].Contains("jackC"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		if (kittyCards [0].Contains ("D")){
			potentialTrump = "Dimonds";
			for (int i = 0; i != 5; i++) {
				if(playerOneCards [i].Contains("D")|| playerOneCards [i].Contains("jackH"))
				{
					playerOneNumOfTrump++;
				}
				if(playerTwoCards [i].Contains("D")|| playerTwoCards [i].Contains("jackH"))
				{
					playerTwoNumOfTrump++;
				}
				if(opponentOneCards [i].Contains("D")|| opponentOneCards [i].Contains("jackH"))
				{
					opponentOneNumOfTrump++;
				}
				if(opponentTwoCards [i].Contains("D") || opponentTwoCards [i].Contains("jackH"))
				{
					opponentTwoNumOfTrump++;
				}
			}
		}
		print ("Player 1 Trump = " + playerOneNumOfTrump);
		print ("Opponent 1 Trump = " + opponentOneNumOfTrump);
		print ("Player 2 Trump = " + playerTwoNumOfTrump);
		print ("Opponent 2 Trump = " + opponentTwoNumOfTrump);
		makeTrunpFromKitty ();

	}

	public void removeToKitty(string CardClicked)
	{
		discardButton.SetActive(true);
		discard = CardClicked;
	}

	public void removeCardToKitty(string CardClicked)
	{
		discard = discard.Replace ("(Clone)", "");
		playerOneCards.Remove (discard);
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
		
		displayAllWithHand ();
	}

	public void trumpButtonClicked()
	{
		playerOneTrumpMade = true;
		trumpMade = true;
		currentTrump = potentialTrump;
		trumpText.text = "Trump is: " + potentialTrump;
		print ("YOU HAVE MADE TRUMP");
		pickUpKitty();

	}

	public void passButtonClicked()
	{
		passMade = true;
		print ("YOU HAVE PASSED");
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
			print ("YOU HAVE MADE TRUMP");

		}
		if (passMade == true) 
		{
			print ("YOU HAVE PASSED");
		}

	}

	public void opponentOneMakeTrump()
	{
		if (opponentOneNumOfTrump >= 3) {
			print("Opponent 1 Made Trump as " + potentialTrump);
			trumpMade = true;
			currentTrump = potentialTrump;
			trumpText.text = "Trump is: " + potentialTrump;
			pickUpKitty();
		} 
		else 
		{
			print("Opponent 1 Passed!!");
		}

	}

	public void opponentTwoMakeTrump()
	{
		if (opponentOneNumOfTrump >= 3) {
			print("Opponent 2 Made Trump as " + potentialTrump);
			trumpMade = true;
			currentTrump = potentialTrump;
			trumpText.text = "Trump is: " + potentialTrump;
			pickUpKitty();

		} 
		else 
		{
			print("Opponent 2 Passed!!");
		}
		
	}

	public void playerTwoMakeTrump()
	{
		if (playerTwoNumOfTrump >= 3) {
			print("Your Partner Made Trump as " + potentialTrump);
			trumpMade = true;
			currentTrump = potentialTrump;
			trumpText.text = "Trump is: " + potentialTrump;
			pickUpKitty();
		} 
		else 
		{
			print("Your Partner Passed!!");
		}
		
	}


	public void makeTrunpFromKitty()
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

	public void displayAllWithHand()
	{
		//team one (This is the BOTTOM and TOP of screen)
		cardToScreenPlayerOne(-600, -600);
		cardToScreenPlayerTwo(-600, 600);	
		
		//team two (This is the LEFT and RIGHT of screen)
		cardToScreenOpponent(-1000, 600);
		cardToScreenOpponentTwo(1000, 600);
		cardToScreenHand(-500, 0);
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
			
			xPosition= xPosition+200;
			#region switch statement player one
			switch (playerOneCards [i]) {

				//Hearts
			case "nineH":
				objectsOnScreen.Add(Instantiate(deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "nineH";
				break;
			case "tenH":
				objectsOnScreen.Add(Instantiate(deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "tenH";
				break;
			case "jackH":
				objectsOnScreen.Add(Instantiate(deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "jackH";
				break;
			case "queenH":
				objectsOnScreen.Add(Instantiate(deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "queenH";
				break;
			case "kingH":
				objectsOnScreen.Add(Instantiate(deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "kingH";
				break;
			case "aceH":
				objectsOnScreen.Add(Instantiate(deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				objectsOnScreen.Add(Instantiate(deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "nineD";
				break;
			case "tenD":
				objectsOnScreen.Add(Instantiate(deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "tenD";
				break;
			case "jackD":
				objectsOnScreen.Add(Instantiate(deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "jackD";
				break;
			case "queenD":
				objectsOnScreen.Add(Instantiate(deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "queenD";
				break;
			case "kingD":
				objectsOnScreen.Add(Instantiate(deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "kingD";
				break;
			case "aceD":
				objectsOnScreen.Add(Instantiate(deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "aceD";
				break;
				
				//Clubs
			case "nineC":
				objectsOnScreen.Add(Instantiate(deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "nineC";
				break;
			case "tenC":
				objectsOnScreen.Add(Instantiate(deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "tenC";
				break;
			case "jackC":
				objectsOnScreen.Add(Instantiate(deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "jackC";
				break;
			case "queenC":
				objectsOnScreen.Add(Instantiate(deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "queenC";
				break;
			case "kingC":
				objectsOnScreen.Add(Instantiate(deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "kingC";
				break;
			case "aceC":
				objectsOnScreen.Add(Instantiate(deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "aceC";
				break;
				
				//Spades
			case "nineS":
				objectsOnScreen.Add(Instantiate(deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "nineS";
				break;
			case "tenS":
				objectsOnScreen.Add(Instantiate(deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "tenS";
				break;
			case "jackS":
				objectsOnScreen.Add(Instantiate(deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "jackS";
				break;
			case "queenS":
				objectsOnScreen.Add(Instantiate(deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "queenS";
				break;
			case "kingS":
				objectsOnScreen.Add(Instantiate(deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
				lastCardPlayedPlayerOne = "kingS";
				break;
			case "aceS":
				objectsOnScreen.Add(Instantiate(deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity) as GameObject);;
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
			
			yPosition= yPosition-200;
			#region switch statement opponent
			switch (opponentOneCards [i]) {

				//Hearts
			case "nineH":
				Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "aceD";
				break;
				
				//Clubs
			case "nineC":
				Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "aceC";
				break;
				
				//Spades
			case "nineS":
				Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));

				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
				lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, -90));
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
			
			xPosition= xPosition+200;
			#region switch statement player one
			switch (playerTwoCards [i]) {

				//Hearts
			case "nineH":
				Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "nineH";
				break;
			case "tenH":
				Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "tenH";
				break;
			case "jackH":
				Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "jackH";
				break;
			case "queenH":
				Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "queenH";
				break;
			case "kingH":
				Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "kingH";
				break;
			case "aceH":
				Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "nineD";
				break;
			case "tenD":
				Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "tenD";
				break;
			case "jackD":
				Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "jackD";
				break;
			case "queenD":
				Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "queenD";
				break;
			case "kingD":
				Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "kingD";
				break;
			case "aceD":
				Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "aceD";
				break;
				
				//Clubs
			case "nineC":
				Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "nineC";
				break;
			case "tenC":
				Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "tenC";
				break;
			case "jackC":
				Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "jackC";
				break;
			case "queenC":
				Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "queenC";
				break;
			case "kingC":
				Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "kingC";
				break;
			case "aceC":
				Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "aceC";
				break;
				
				//Spades
			case "nineS":
				Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "nineS";
				break;
			case "tenS":
				Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "tenS";
				break;
			case "jackS":
				Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "jackS";
				break;
			case "queenS":
				Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "queenS";
				break;
			case "kingS":
				Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedPlayerOne = "kingS";
				break;
			case "aceS":
				Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
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
			
			yPosition= yPosition-200;
			#region switch statement opponent
			switch (opponentTwoCards [i]) {
	
				//Hearts
			case "nineH":
				Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "aceD";
				break;
				
				//Clubs
			case "nineC":
				Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "aceC";
				break;
				
				//Spades
			case "nineS":
				Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
				lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.Euler(0, 0, 90));
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

			xPosition= xPosition+200;
			#region switch statement opponent
			switch (kittyCards [0]) {

			//Hearts
			case "nineH":
				Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceH";
				break;
				
				//Diamonds
			case "nineD":
				Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceD";
				break;
				
				//Clubs
			case "nineC":
				Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceC";
				break;
				
				//Spades
			case "nineS":
				Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceS";
				break;
			}
			#endregion
			

	}

	public void cardToScreenHand(float xPosition, float yPosition)
	{
		GameObject manager = GameObject.Find("_Manager");
		DeckObjects deckobjects = manager.GetComponent<DeckObjects>();


		for (int i = 0; i != handCards.Count; i++) {
			xPosition = xPosition + 200;
			#region switch statement opponent
			switch (handCards [i]) {
			
			//Hearts
			case "nineH":
				Instantiate (deckobjects.nineH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineH";
				break;
			case "tenH":
				Instantiate (deckobjects.tenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenH";
				break;
			case "jackH":
				Instantiate (deckobjects.jackH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackH";
				break;
			case "queenH":
				Instantiate (deckobjects.queenH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenH";
				break;
			case "kingH":
				Instantiate (deckobjects.kingH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingH";
				break;
			case "aceH":
				Instantiate (deckobjects.aceH, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceH";
				break;
			
			//Diamonds
			case "nineD":
				Instantiate (deckobjects.nineD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineD";
				break;
			case "tenD":
				Instantiate (deckobjects.tenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenD";
				break;
			case "jackD":
				Instantiate (deckobjects.jackD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackD";
				break;
			case "queenD":
				Instantiate (deckobjects.queenD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenD";
				break;
			case "kingD":
				Instantiate (deckobjects.kingD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingD";
				break;
			case "aceD":
				Instantiate (deckobjects.aceD, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceD";
				break;
			
			//Clubs
			case "nineC":
				Instantiate (deckobjects.nineC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineC";
				break;
			case "tenC":
				Instantiate (deckobjects.tenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenC";
				break;
			case "jackC":
				Instantiate (deckobjects.jackC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackC";
				break;
			case "queenC":
				Instantiate (deckobjects.queenC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenC";
				break;
			case "kingC":
				Instantiate (deckobjects.kingC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingC";
				break;
			case "aceC":
				Instantiate (deckobjects.aceC, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceC";
				break;
			
			//Spades
			case "nineS":
				Instantiate (deckobjects.nineS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "nineS";
				break;
			case "tenS":
				Instantiate (deckobjects.tenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "tenS";
				break;
			case "jackS":
				Instantiate (deckobjects.jackS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "jackS";
				break;
			case "queenS":
				Instantiate (deckobjects.queenS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "queenS";
				break;
			case "kingS":
				Instantiate (deckobjects.kingS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "kingS";
				break;
			case "aceS":
				Instantiate (deckobjects.aceS, new Vector3 (xPosition, yPosition, 0), Quaternion.identity);
				lastCardPlayedOpponent = "aceS";
				break;
			}
			#endregion
		}
		
=======
    
    
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

    public void cardToScreenPlayerOne()
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();

        #region switch statement player one
        switch (playerOneCards[0])
        {
            case "twoH":
                Instantiate(deckobjects.twoH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceH";
                break;
            
            //Diamonds
            case "twoD":
                Instantiate(deckobjects.twoD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceD, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceD";
                break;

            //Clubs
            case "twoC":
                Instantiate(deckobjects.twoC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceC, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceC";
                break;

            //Spades
            case "twoS":
                Instantiate(deckobjects.twoS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceS, new Vector3(0, -2, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceS";
                break;
        }
        #endregion
    }

    public void cardToScreenOpponent()
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();
        
        #region switch statement opponent
        switch (opponentCards[0])
        {
            case "twoH":
                Instantiate(deckobjects.twoH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceH";
                break;

            //Diamonds
            case "twoD":
                Instantiate(deckobjects.twoD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceD, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceD";
                break;

            //Clubs
            case "twoC":
                Instantiate(deckobjects.twoC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceC, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceC";
                break;

            //Spades
            case "twoS":
                Instantiate(deckobjects.twoS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceS, new Vector3(0, 2, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceS";
                break;
        }
        #endregion
    }

    public void updateScore()
    {
        playerScoreText.text = "Player One Score: " + playerOneCards.Count;
        opponentScoreText.text = "Opponents Score: " + opponentCards.Count;
        cardToScreenPlayerOne();
    }


    // Use this for initialization
	void Start () 
	{
        //processturn();
        dealPlayerCards();
        cardToScreenPlayerOne();
>>>>>>> f9ca9b539ed3444388666804bb67b0504376cebc
	}

    void FixedUpdate()
    {

    }
}