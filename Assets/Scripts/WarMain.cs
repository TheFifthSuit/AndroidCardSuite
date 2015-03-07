using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarMain : MonoBehaviour {


	//card names within deck should match Prefab names should have seperate prefab for all 52 cards
	string[] cardDeck = new string[] {"1H", "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "10H", "JH", "QH", "KH", "AH", 
									  "1D", "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "10D", "JD", "QD", "KD", "AD", 
									  "1C", "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "JC", "QC", "KC", "AC",
									  "1S", "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "10S", "JS", "QS", "KS", "AS"};

	//list of each players 26 cards they randomly got to use for this game
	List<string> playerOneCards = new List<string>();
	List<string> playerTwoCards = new List<string>();


	private void dealPlayerCards ()
	{
		List<int> dealtCards = new List<int>();

		for (int i = 0; i <= 25; i++) 
		{
			int drawnCard = Random.Range(0, 52);

			if(dealtCards.IndexOf(drawnCard) == -1)
			{
				playerOneCards.Add (cardDeck[drawnCard]);
				dealtCards.Add (drawnCard);
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
				playerTwoCards.Add (cardDeck[drawnCard]);
				dealtCards.Add (drawnCard);
			}
			else
			{
				i--;
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		dealPlayerCards ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
