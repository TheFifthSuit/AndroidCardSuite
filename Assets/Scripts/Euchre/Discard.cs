using UnityEngine;
using System.Collections;

public class Discard : MonoBehaviour {


	void OnMouseDown()
	{
		if (Application.loadedLevelName == "Euchre")
		{
			GameObject manager = GameObject.Find("_Manager");
			EuchreMain emain = manager.GetComponent<EuchreMain>();

			if (emain.cardIsDiscarded == false)
			{
			   emain.removeToKitty(gameObject.name);
			}

		}
	}
	

}