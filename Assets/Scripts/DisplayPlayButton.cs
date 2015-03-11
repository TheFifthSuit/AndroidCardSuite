using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayPlayButton : MonoBehaviour {

	public GameObject playbutton;
    private GameObject playbuttonToDelete;
	private float posX;
    private float posY;
    private bool playbuttonRemoveOnClick = false;
	private bool HandFinished = false;

	void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;
	}

    void OnMouseDown()
    {
		checkForWinOrLose ();
        
        if(HandFinished == false)
        {
            if (playbuttonRemoveOnClick == false)
            {
                //this if is added so opponents cards do not display play button when clicked
                if (posY < 0.0f)
                {
                    playbuttonToDelete = Instantiate(playbutton, new Vector3(posX, posY + transform.localScale.y, 0), Quaternion.identity) as GameObject;
                    playbuttonRemoveOnClick = true;
                }
            }
            else if (playbuttonRemoveOnClick == true)
            {
                Destroy(playbuttonToDelete);
                playbuttonRemoveOnClick = false;
            }
        }
    }
	

	private void checkForWinOrLose()
	{
			GameObject handobjectwin = GameObject.Find ("HandWon");
			GameObject handobjectlost = GameObject.Find ("HandLost");

			if (handobjectwin != null || handobjectlost != null) 
			{
				HandFinished = true;
			}
	}

}
