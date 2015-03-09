using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarMain : MonoBehaviour {

    //script objects
    public CardValue cardvalue;



    //variables used in functions
    string lastCardPlayedPlayerOne;
    string lastCardPlayedOpponent;

	
    
    //card names within deck should match Prefab names should have seperate prefab for all 52 cards
	string[] cardDeck = new string[] {"oneH", "twoH", "threeH", "fourH", "fiveH", "sixH", "sevenH", "eightH", "nineH", "tenH", "jackH", "queenH", "kingH", "aceH", 
									  "oneD", "twoD", "threeD", "fourD", "fiveD", "sixD", "sevenD", "eightD", "nineD", "tenD", "jackD", "queenD", "kingD", "aceD", 
									  "oneC", "twoC", "threeC", "fourC", "fiveC", "sixC", "sevenC", "eightC", "nineC", "tenC", "jackC", "queenC", "kingC", "aceC",
									  "oneS", "twoS", "threeS", "fourS", "fiveS", "sixS", "sevenS", "eightS", "nineS", "tenS", "jackS", "queenS", "kingS", "aceS"};

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


    private void processturn()
    {
        GameObject playercard;
        GameObject opponentcard;

        playercard = GameObject.FindGameObjectWithTag("Finish");

		playercard.gameObject.GetComponent<CardValue>().getCardValue();

    }

    private void cardToScreen()
    {
        GameObject manager = GameObject.Find("_Manager");
        DeckObjects deckobjects = manager.GetComponent<DeckObjects>();

        #region switch statement player one
        switch (playerOneCards[0])
        {
            case "oneH":
                Instantiate(deckobjects.oneH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "oneH";
                break;
            case "twoH":
                Instantiate(deckobjects.twoH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceH";
                break;
            
            //Diamonds
            case "oneD":
                Instantiate(deckobjects.oneH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "oneD";
                break;
            case "twoD":
                Instantiate(deckobjects.twoH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceD";
                break;

            //Clubs
            case "oneC":
                Instantiate(deckobjects.oneH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "oneC";
                break;
            case "twoC":
                Instantiate(deckobjects.twoH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceC";
                break;

            //Spades
            case "oneS":
                Instantiate(deckobjects.oneH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "oneS";
                break;
            case "twoS":
                Instantiate(deckobjects.twoH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceH, new Vector3(0, 0, 0), Quaternion.identity);
                lastCardPlayedPlayerOne = "aceS";
                break;

        }
        #endregion

        #region switch statement opponent
        switch (opponentCards[0])
        {
            case "oneH":
                Instantiate(deckobjects.oneH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "oneH";
                break;
            case "twoH":
                Instantiate(deckobjects.twoH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoH";
                break;
            case "threeH":
                Instantiate(deckobjects.threeH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeH";
                break;
            case "fourH":
                Instantiate(deckobjects.fourH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourH";
                break;
            case "fiveH":
                Instantiate(deckobjects.fiveH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveH";
                break;
            case "sixH":
                Instantiate(deckobjects.sixH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixH";
                break;
            case "sevenH":
                Instantiate(deckobjects.sevenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenH";
                break;
            case "eightH":
                Instantiate(deckobjects.eightH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightH";
                break;
            case "nineH":
                Instantiate(deckobjects.nineH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineH";
                break;
            case "tenH":
                Instantiate(deckobjects.tenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenH";
                break;
            case "jackH":
                Instantiate(deckobjects.jackH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackH";
                break;
            case "queenH":
                Instantiate(deckobjects.queenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenH";
                break;
            case "kingH":
                Instantiate(deckobjects.kingH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingH";
                break;
            case "aceH":
                Instantiate(deckobjects.aceH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceH";
                break;

            //Diamonds
            case "oneD":
                Instantiate(deckobjects.oneH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "oneD";
                break;
            case "twoD":
                Instantiate(deckobjects.twoH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoD";
                break;
            case "threeD":
                Instantiate(deckobjects.threeH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeD";
                break;
            case "fourD":
                Instantiate(deckobjects.fourH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourD";
                break;
            case "fiveD":
                Instantiate(deckobjects.fiveH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveD";
                break;
            case "sixD":
                Instantiate(deckobjects.sixH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixD";
                break;
            case "sevenD":
                Instantiate(deckobjects.sevenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenD";
                break;
            case "eightD":
                Instantiate(deckobjects.eightH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightD";
                break;
            case "nineD":
                Instantiate(deckobjects.nineH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineD";
                break;
            case "tenD":
                Instantiate(deckobjects.tenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenD";
                break;
            case "jackD":
                Instantiate(deckobjects.jackH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackD";
                break;
            case "queenD":
                Instantiate(deckobjects.queenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenD";
                break;
            case "kingD":
                Instantiate(deckobjects.kingH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingD";
                break;
            case "aceD":
                Instantiate(deckobjects.aceH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceD";
                break;

            //Clubs
            case "oneC":
                Instantiate(deckobjects.oneH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "oneC";
                break;
            case "twoC":
                Instantiate(deckobjects.twoH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoC";
                break;
            case "threeC":
                Instantiate(deckobjects.threeH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeC";
                break;
            case "fourC":
                Instantiate(deckobjects.fourH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourC";
                break;
            case "fiveC":
                Instantiate(deckobjects.fiveH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveC";
                break;
            case "sixC":
                Instantiate(deckobjects.sixH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixC";
                break;
            case "sevenC":
                Instantiate(deckobjects.sevenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenC";
                break;
            case "eightC":
                Instantiate(deckobjects.eightH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightC";
                break;
            case "nineC":
                Instantiate(deckobjects.nineH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineC";
                break;
            case "tenC":
                Instantiate(deckobjects.tenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenC";
                break;
            case "jackC":
                Instantiate(deckobjects.jackH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackC";
                break;
            case "queenC":
                Instantiate(deckobjects.queenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenC";
                break;
            case "kingC":
                Instantiate(deckobjects.kingH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingC";
                break;
            case "aceC":
                Instantiate(deckobjects.aceH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceC";
                break;

            //Spades
            case "oneS":
                Instantiate(deckobjects.oneH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "oneS";
                break;
            case "twoS":
                Instantiate(deckobjects.twoH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "twoS";
                break;
            case "threeS":
                Instantiate(deckobjects.threeH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "threeS";
                break;
            case "fourS":
                Instantiate(deckobjects.fourH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fourS";
                break;
            case "fiveS":
                Instantiate(deckobjects.fiveH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "fiveS";
                break;
            case "sixS":
                Instantiate(deckobjects.sixH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sixS";
                break;
            case "sevenS":
                Instantiate(deckobjects.sevenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "sevenS";
                break;
            case "eightS":
                Instantiate(deckobjects.eightH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "eightS";
                break;
            case "nineS":
                Instantiate(deckobjects.nineH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "nineS";
                break;
            case "tenS":
                Instantiate(deckobjects.tenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "tenS";
                break;
            case "jackS":
                Instantiate(deckobjects.jackH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "jackS";
                break;
            case "queenS":
                Instantiate(deckobjects.queenH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "queenS";
                break;
            case "kingS":
                Instantiate(deckobjects.kingH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "kingS";
                break;
            case "aceS":
                Instantiate(deckobjects.aceH, new Vector3(1, 1, 0), Quaternion.identity);
                lastCardPlayedOpponent = "aceS";
                break;

        }
        #endregion
    }

    // Use this for initialization
	void Start () 
	{
        //processturn();
        dealPlayerCards();
        cardToScreen();
	}
	



}