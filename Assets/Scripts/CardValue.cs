using UnityEngine;
using System.Collections;

public class CardValue : MonoBehaviour {

	public int cardNumberValue;


	// Use this for initialization

    public int getCardValue()
    {
        if (this.gameObject.name == "2H")
        {
            cardNumberValue = 2;
        }

        return cardNumberValue;
    }
    
}
