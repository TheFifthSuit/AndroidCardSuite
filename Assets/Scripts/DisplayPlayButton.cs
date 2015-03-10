using UnityEngine;
using System.Collections;

public class DisplayPlayButton : MonoBehaviour {

	public GameObject playbutton;
	private float posX;
    private float posY;

	void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;
	}

    void OnMouseDown()
    {
        //this if is added so opponents cards do not display play button when clicked
        if (posY < 0.0f)
        {
            Instantiate(playbutton, new Vector3(posX, posY + transform.localScale.y, 0), Quaternion.identity);
        }
    }
}
