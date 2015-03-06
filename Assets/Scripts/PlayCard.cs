using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {

	public GameObject playbutton;
	public float posX;
	public float posY;

	void Start()
	{
		posX = transform.localScale.x;
		posY = transform.localScale.y;
	}

	void OnMouseDown()
	{
		Instantiate(playbutton, new Vector3(posX, posY, 0), Quaternion.identity);
	}


}
