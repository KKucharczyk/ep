using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpTarget;
	public static bool isWarping = false;

	IEnumerator OnTriggerEnter2D(Collider2D other) {
		isWarping = true;
		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader>();

		yield return StartCoroutine (sf.Fading ());

		other.gameObject.transform.position = warpTarget.position;
		Camera.main.transform.position = warpTarget.position;
		isWarping = false;
		yield return StartCoroutine (sf.Unfading ());

	}
}
