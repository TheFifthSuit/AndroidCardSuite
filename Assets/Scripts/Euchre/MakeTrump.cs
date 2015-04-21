using UnityEngine;
using System.Collections;

public class MakeTrump : MonoBehaviour {

	void OnClick()
	{
		GameObject manager = GameObject.Find("_Manager");
		EuchreMain emain = manager.GetComponent<EuchreMain>();

		emain.trumpButtonClicked();
	}
}