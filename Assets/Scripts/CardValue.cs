using UnityEngine;
using System.Collections;

public class CardValue : MonoBehaviour {

	public int cardNumberValue;

	// Use this for initialization
	void Start () 
	{
		if (this.gameObject.name == "2H") 
		{
			cardNumberValue = 2;
		}
	}
}
