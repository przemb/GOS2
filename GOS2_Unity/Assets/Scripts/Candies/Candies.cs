using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Candies : MonoBehaviour
{

	private SpriteRenderer sr;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer> ();
		sr.enabled = false;
	}

	void Update ()
	{
		Debug.Log ("Wcisnieto");
		if (Input.GetKeyDown ("i")) {

			if (sr.enabled == true) {

				sr.enabled = false;
			} else {
				sr.enabled = true;
			}
		}
	}
}