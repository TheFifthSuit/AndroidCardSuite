using UnityEngine;
using System.Collections;

public class PlayCardToHand : MonoBehaviour {


	void OnMouseDown()
	{
		if (Application.loadedLevelName == "Euchre")
		{
			GameObject manager = GameObject.Find("_Manager");
			EuchreMain emain = manager.GetComponent<EuchreMain>();

			if (emain.cardIsPlayed == false)
			{
				emain.playPlayerCardClicked(gameObject.name);
				//print (gameObject.name);
			}

		}
	}
	

}