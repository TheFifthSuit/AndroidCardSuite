using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DeckObjects : MonoBehaviour {

    //Hearts
    public GameObject twoH;
    public GameObject threeH;
    public GameObject fourH;
    public GameObject fiveH;
    public GameObject sixH;
    public GameObject sevenH;
    public GameObject eightH;
    public GameObject nineH;
    public GameObject tenH;
    public GameObject jackH;
    public GameObject queenH;
    public GameObject kingH;
    public GameObject aceH;

    //Diamonds
    public GameObject twoD;
    public GameObject threeD;
    public GameObject fourD;
    public GameObject fiveD;
    public GameObject sixD;
    public GameObject sevenD;
    public GameObject eightD;
    public GameObject nineD;
    public GameObject tenD;
    public GameObject jackD;
    public GameObject queenD;
    public GameObject kingD;
    public GameObject aceD;

    //Clubs
    public GameObject twoC;
    public GameObject threeC;
    public GameObject fourC;
    public GameObject fiveC;
    public GameObject sixC;
    public GameObject sevenC;
    public GameObject eightC;
    public GameObject nineC;
    public GameObject tenC;
    public GameObject jackC;
    public GameObject queenC;
    public GameObject kingC;
    public GameObject aceC;

    //Spades
    public GameObject twoS;
    public GameObject threeS;
    public GameObject fourS;
    public GameObject fiveS;
    public GameObject sixS;
    public GameObject sevenS;
    public GameObject eightS;
    public GameObject nineS;
    public GameObject tenS;
    public GameObject jackS;
    public GameObject queenS;
    public GameObject kingS;
    public GameObject aceS;

    //cards list
    public List<GameObject> allCards = new List<GameObject>();
    public List<GameObject> selectCards = new List<GameObject>();
	private bool listCreated = false;

    private void fillAllCards()
    {
        allCards.Add(twoH);
        allCards.Add(threeH);
        allCards.Add(fourH);
        allCards.Add(fiveH);
        allCards.Add(sixH);
        allCards.Add(sevenH);
        allCards.Add(eightH);
        allCards.Add(nineH);
        allCards.Add(tenH);
        allCards.Add(jackH);
        allCards.Add(queenH);
        allCards.Add(kingH);
        allCards.Add(aceH);
        
        allCards.Add(twoD);
        allCards.Add(threeD);
        allCards.Add(fourD);
        allCards.Add(fiveD);
        allCards.Add(sixD);
        allCards.Add(sevenD);
        allCards.Add(eightD);
        allCards.Add(nineD);
        allCards.Add(tenD);
        allCards.Add(jackD);
        allCards.Add(queenD);
        allCards.Add(kingD);
        allCards.Add(aceD);
        allCards.Add(threeD);

        allCards.Add(twoC);
        allCards.Add(threeC);
        allCards.Add(fourC);
        allCards.Add(fiveC);
        allCards.Add(sixC);
        allCards.Add(sevenC);
        allCards.Add(eightC);
        allCards.Add(nineC);
        allCards.Add(tenC);
        allCards.Add(jackC);
        allCards.Add(queenC);
        allCards.Add(kingC);
        allCards.Add(aceC);

        allCards.Add(twoS);
        allCards.Add(threeS);
        allCards.Add(fourS);
        allCards.Add(fiveS);
        allCards.Add(sixS);
        allCards.Add(sevenS);
        allCards.Add(eightS);
        allCards.Add(nineS);
        allCards.Add(tenS);
        allCards.Add(jackS);
        allCards.Add(queenS);
        allCards.Add(kingS);
        allCards.Add(aceS);
		listCreated = true;
    }


    public void createListOfSpecificCards(string cardName)
    {
		if(listCreated == false)
			fillAllCards();

		foreach (GameObject ob in allCards)
        {
            if (ob.name.ToString() == cardName)
            {
                selectCards.Add(ob);
            }
        }
    }

    public void clearListOfSpecificCards()
    {
        selectCards.Clear();
    }
}
