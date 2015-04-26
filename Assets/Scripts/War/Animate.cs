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
	private bool clickedDiscard = false;

    
    void OnMouseDown()
    {

		GameObject manager = GameObject.Find ("_Manager");
		WarMain warmain = manager.GetComponent<WarMain> ();
		EuchreMain emain = manager.GetComponent<EuchreMain> ();


		if (Application.loadedLevelName == "War") {

            playAnimation();
            warmain.setPlayButton(gameObject.name);

		}


		if (Application.loadedLevelName == "Euchre") {
			//moves cards to position for euchre and only happens on double click.
			if (clickCounter != 1) {
				clickCounter++;
			} else if (clickCounter == 1) {
				
				if(emain.cardIsDiscarded == false)
				{
					string discard = gameObject.name.ToString().Replace("(Clone)","");
					print ("DISCARD " + discard);
					//emain.playerOneCards.Remove(gameObject);
					int indexCard = emain.playerOneCards.IndexOf(gameObject);
					print ("INDEX " + indexCard);


					emain.clearBoardNoReplaceKitty();
					emain.displayAllNoKitty();

					//clickedDiscard = true;
					clickCounter = 0;
				}

				
				if(clickedDiscard == true)
				{

					/*
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
					foreach (GameObject go in emain.playerOneCards)
					{
						if (go.name.ToString() == this.gameObject.name.ToString().Replace("(Clone)",""))
							playerOne = true;
                    }
                }
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    foreach (GameObject go in emain.playerTwoCards)
                    {
						if (go.name.ToString() == this.gameObject.name.ToString().Replace("(Clone)",""))
                            playerTwo = true;
                    }
                }
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    foreach (GameObject go in emain.opponentOneCards)
                    {
						if (go.name.ToString() == this.gameObject.name.ToString().Replace("(Clone)",""))
                            opponentOne = true;
                    }
                }
                if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false)
                {
                    foreach (GameObject go in emain.opponentTwoCards)
                    {
						if (go.name.ToString() == this.gameObject.name.ToString().Replace("(Clone)",""))
                            opponentTwo = true;
                    }
                }

					//startAnimation = true;
					//playAnimation();
					//emain.playGame();
					clickCounter = 0;
					//emain.playEveryoneElsesCards();
					*/
				}



            }
        }

			}

			
    public void moveCardIntoPosition()
    {

        GameObject manager = GameObject.Find("_Manager");
        EuchreMain emain = manager.GetComponent<EuchreMain>();

			
		if (emain.cardIsDiscarded == true && clickedDiscard == true) {

        /*
			if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false) {
				foreach (GameObject go in emain.playerOneCards) {
					if (go.name.ToString () == this.gameObject.name.ToString ().Replace ("(Clone)", ""))
						playerOne = true;
				}
			}
			if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false) {
				foreach (GameObject go in emain.playerTwoCards) {
					if (go.name.ToString () == this.gameObject.name.ToString ().Replace ("(Clone)", ""))
						playerTwo = true;
				}
			}
			if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false) {
				foreach (GameObject go in emain.opponentOneCards) {
					if (go.name.ToString () == this.gameObject.name.ToString ().Replace ("(Clone)", ""))
						opponentOne = true;
				}
			}
			if (playerOne == false && playerTwo == false && opponentOne == false && opponentTwo == false) {
				foreach (GameObject go in emain.opponentTwoCards) {
					if (go.name.ToString () == this.gameObject.name.ToString ().Replace ("(Clone)", ""))
						opponentTwo = true;
				}
			}
			startAnimation = true;
			playAnimation ();
*/
		}

    }


			
    //flips cards that have not been flipped yet on screen
    public void playAnimation()
    {
		/*
	
		if (clickedDiscard == true) {
			if (flipPlayed == false) {
				this.gameObject.animation.Play ();
				flipPlayed = true;
			} else {

			}
		}

*/
    }


  public void playAnimation(GameObject objectToFlip)
    {
		/*
		if (clickedDiscard == true) {
			if (flipPlayed == false) {
				objectToFlip.animation.Play ();
				flipPlayed = true;
			} else {

			}
		}
*/
   }

    void FixedUpdate()
    {
		/*

		if (clickedDiscard == true) {
			if (startAnimation && playerOne == true) {
				gameObject.transform.localPosition = Vector3.Lerp (transform.position, new Vector3 (0f, -200f, 0), Time.deltaTime * 1);
			} else if (startAnimation && playerTwo == true) {
				gameObject.transform.localPosition = Vector3.Lerp (transform.position, new Vector3 (0f, 200f, 0), Time.deltaTime * 1);
			} else if (startAnimation && opponentOne == true) {
				gameObject.transform.localPosition = Vector3.Lerp (transform.position, new Vector3 (-400f, 0f, 0), Time.deltaTime * 1);
			} else if (startAnimation && opponentTwo == true) {
				gameObject.transform.localPosition = Vector3.Lerp (transform.position, new Vector3 (400f, 0f, 0), Time.deltaTime * 1);
			}
		}
*/
    }


}