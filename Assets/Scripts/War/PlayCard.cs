using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayCard : MonoBehaviour {

    public GameObject winMessage;
    public GameObject loseMessage;
    private GameObject ob;
    
    public void playcard()
    {
        GameObject manager = GameObject.Find("_Manager");
        WarMain warmain = manager.GetComponent<WarMain>();
        warmain.cardToScreenOpponent(0, 300);

        Animate animateScript = warmain.objectToAnimate.GetComponent<Animate>();
        animateScript.playAnimation(warmain.objectToAnimate);
        warmain.processturn();

        if (warmain.lastHandWinner == "PlayerOne")
        {
            //winMessage.animation.Play("left_entrance");
            ob = Instantiate(winMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.Find("Canvas").transform);
            //ob.transform.parent = GameObject.Find("Canvas").transform;
            ob.animation.Play();
        }
        if (warmain.lastHandWinner == "Opponent")
        {
            ob = Instantiate(loseMessage, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.Find("Canvas").transform);
            //ob.transform.parent = GameObject.Find("Canvas").transform;
            ob.animation.Play();
        }
		warmain.removePlayButton ();
    }
}
