using UnityEngine;
using System.Collections;

public class DisplayPlayButton : MonoBehaviour {

	public GameObject playbutton;

	void Start()
	{

	}
    
    void OnMouseDown()
    {
		GameObject manager = GameObject.Find("_Manager");
		WarMain warmain = manager.GetComponent<WarMain>();

		warmain.setPlayButton (gameObject.name);
    }
}
