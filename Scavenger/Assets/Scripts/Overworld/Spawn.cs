using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	IEnumerator OnTriggerEnter2D(Collider2D other) {

		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader>();

		if (other.tag == "Player") {
			Debug.Log ("Detected in spawn area.");
			if(countEncounterChance() == true) {
				yield return StartCoroutine(sf.FadeToBlack());
				Application.LoadLevel("Main");
			}
		
		}

	}

	private bool countEncounterChance() {;
		int chance = Random.Range (1,10);
		Debug.Log ("Encounter chance: " + chance);
		if (chance == 9)
			return true;
		else
			return false;
	}
}
