using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {

	public GameObject playbutton;
    private WarMain warmain;
	public float posX;
	public float posY;

	void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;
	}

	void OnMouseDown()
	{
		Instantiate(playbutton, new Vector3(posX, posY + transform.localScale.y, 0), Quaternion.identity);

	}


}
