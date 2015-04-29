using UnityEngine;
using System.Collections;

public class Animate : MonoBehaviour {

    private bool flipPlayed = false;
    
    //for Euchre
    private bool startAnimation;
    private bool playerOne = false;
    private bool playerTwo = false;
    private bool opponentOne = false;
    private bool opponentTwo = false;
    private int clickCounter = 0;
    private bool isPlayerOneCard;
    private int cardIndexInList;

    
    void OnMouseDown()
    {
        GameObject manager = GameObject.Find("_Manager");
        WarMain warmain = manager.GetComponent<WarMain>();
        EuchreMain emain = manager.GetComponent<EuchreMain>();

        if (Application.loadedLevelName == "War")
        {
            playAnimation();
            warmain.setPlayButton(gameObject.name);
        }

        if (Application.loadedLevelName == "Euchre")
        {
            if (emain.winMessage != null || emain.passButton.activeSelf == true || emain.trumpButton.activeSelf == true)
            {
                return;
            }
            //check to see if we have 6 cards or not if so need to discard
            if (emain.playerOneCards.Count == 6)
            {
                /*
                ADD SOME TYPE OF MESSAGE SHOWING THAT YOU NEED TO DISCARD CARD
                */

                foreach (GameObject ob in emain.playerOneCards)
                {
                    string listCardName = ob.name.ToString() + "(Clone)";

                    if (listCardName == this.gameObject.name.ToString())
                    {
                        isPlayerOneCard = true;
                    }
                }

                if (isPlayerOneCard == true)
                {
                    //removes from playerOneCards GameObject List
                    for (int i = 0; i <= emain.playerOneCards.Count -1; i++)
                    {
                        string listCardName = emain.playerOneCards[i].name.ToString() + "(Clone)";

                        if (listCardName == this.gameObject.name.ToString())
                        {
                            emain.playerOneCards.RemoveAt(i);
                        }
                    }

                    //removes card from screen.
                    GameObject.Destroy(this.gameObject);
                    emain.trackLeader = 0;

					/*
                     REMOVE DISCARD MESSAGE HERE
                    */

                }
            }

            //moves cards to position for euchre and only happens on double click.
            if (clickCounter != 1)
            {
                clickCounter++;
            }
            else if (clickCounter == 1)
            {
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    for (int i = 0; i <= emain.playerOneCards.Count - 1;i++)
                        {
                            if (emain.playerOneCards[i].name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                            {
                                playerOne = true;
                                emain.handCards.Add(emain.playerOneCards[emain.playerOneCards.IndexOf(emain.playerOneCards[i])]);
                                emain.removeFromList(emain.playerOneCards.IndexOf(emain.playerOneCards[i]), "playerOne");
                                cardIndexInList = emain.handCards.Count - 1;
                            }
                        }
                }
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    foreach (GameObject go in emain.playerTwoCards)
                    {
                        if (go.name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                        {

							playerTwo = true;

                            emain.handCards.Add(emain.playerOneCards[emain.playerOneCards.IndexOf(go)]);
                            emain.removeFromList(emain.playerOneCards.IndexOf(go), "playerTwo");
                            cardIndexInList = emain.handCards.Count - 1;
                        }
                    }
                }
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    foreach (GameObject go in emain.opponentOneCards)
                    {
                        if (go.name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                        {

							opponentOne = true;

                            emain.handCards.Add(emain.playerOneCards[emain.playerOneCards.IndexOf(go)]);
                            emain.removeFromList(emain.playerOneCards.IndexOf(go), "opponentOne");
                            cardIndexInList = emain.handCards.Count - 1;
                        }
                    }
                }
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    foreach (GameObject go in emain.opponentTwoCards)
                    {
                        if (go.name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                        {
                            opponentTwo = true;
                            emain.handCards.Add(emain.playerOneCards[emain.playerOneCards.IndexOf(go)]);
                            emain.removeFromList(emain.playerOneCards.IndexOf(go), "opponentTwo");
                            cardIndexInList = emain.handCards.Count - 1;
                        }
                    }
                }


                startAnimation = true;
                playAnimation();
                clickCounter = 0;
                emain.playSpecificCardInHand(cardIndexInList);
				if(emain.trackLeader != 0)
				{
                	emain.playerOnePlayed = true;
				}
            }
        }
    }

    public void moveCardIntoPosition()
    {
        GameObject manager = GameObject.Find("_Manager");
        EuchreMain emain = manager.GetComponent<EuchreMain>();
        
        if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
        {
            foreach (GameObject go in emain.playerOneCards)
            {
                if (go.name.ToString() + "(Clone)"  == this.gameObject.name.ToString())
                    playerOne = true;
            }
        }
        if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
        {
            foreach (GameObject go in emain.playerTwoCards)
            {
                if (go.name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                    playerTwo = true;
            }
        }
        if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
        {
            foreach (GameObject go in emain.opponentOneCards)
            {
                if (go.name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                    opponentOne = true;
            }
        }
        if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
        {
            foreach (GameObject go in emain.opponentTwoCards)
            {
                if (go.name.ToString() + "(Clone)" == this.gameObject.name.ToString())
                    opponentTwo = true;
            }
        }
        startAnimation = true;
        playAnimation();
    }

    //flips cards that have not been flipped yet on screen
    public void playAnimation()
    {
        if (flipPlayed == false)
        {
            this.gameObject.animation.Play();
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
            objectToFlip.animation.Play();
            flipPlayed = true;
        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        if (startAnimation && playerOne == true)
        {
            gameObject.transform.localPosition = Vector3.Lerp(transform.position, new Vector3(0f, -200f, 0), Time.deltaTime * 1);
        }
        else if (startAnimation && playerTwo == true)
        {
            gameObject.transform.localPosition = Vector3.Lerp(transform.position, new Vector3(0f, 200f, 0), Time.deltaTime * 1);
        }
        else if (startAnimation && opponentOne == true)
        {
            gameObject.transform.localPosition = Vector3.Lerp(transform.position, new Vector3(-400f, 0f, 0), Time.deltaTime * 1);
        }
        else if (startAnimation && opponentTwo == true)
        {
            gameObject.transform.localPosition = Vector3.Lerp(transform.position, new Vector3(400f, 0f, 0), Time.deltaTime * 1);
        }
    }
}
