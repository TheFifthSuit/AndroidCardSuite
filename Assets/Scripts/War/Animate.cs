using UnityEngine;
using System.Collections;

public class Animate : MonoBehaviour {

    public GameObject objectToAnimate;
    private bool flipPlayed = false;
    
    void OnMouseDown()
    {
        if (Application.loadedLevelName == "War")
        {
            GameObject manager = GameObject.Find("_Manager");
            WarMain warmain = manager.GetComponent<WarMain>();

            playAnimation();
            warmain.setPlayButton(gameObject.name);
        }
    }

    //flips cards that have not been flipped yet on screen
    public void playAnimation()
    {
        if (flipPlayed == false)
        {
            objectToAnimate.animation.Play();
            flipPlayed = true;
        }
        else
        {

        }
    }

    public void playAnimation(GameObject objectToFlip)
    {
        if (flipPlayed == false)
        {
            objectToAnimate.animation.Play();
            flipPlayed = true;
        }
        else
        {

        }
    }
}
