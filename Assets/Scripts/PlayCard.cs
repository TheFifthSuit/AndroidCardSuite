using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {

    public GameObject placeHolder;
    private bool mouseUp;
	private Sprite newSprite;

	void OnTriggerEnter2D(Collider2D collision)
    {
            transform.position = placeHolder.transform.position;

            transform.localScale += new Vector3(4F + (collision.gameObject.transform.localScale.x * 0.3f), 4f + (collision.gameObject.transform.localScale.y * 0.3f), 0);
			Destroy(gameObject);
    }

}
