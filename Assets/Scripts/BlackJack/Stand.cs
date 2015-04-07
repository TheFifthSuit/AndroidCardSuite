using UnityEngine;
using System.Collections;

public class Stand : MonoBehaviour {

	void OnClick()
	{
		GameObject manager = GameObject.Find("_Manager");
		BlackjackMain bjmain = manager.GetComponent<BlackjackMain>();

		bjmain.Stand();
	}
}