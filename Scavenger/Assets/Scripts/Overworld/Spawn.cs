using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	IEnumerator OnTriggerEnter2D(Collider2D other) {

		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader>();

		if (other.tag == "Player") {
			if(countEncounterChance() == true) {
				yield return StartCoroutine(sf.FadeToBlack());
				Application.LoadLevel("Battle Mode");
			}
		}
	}

	private bool countEncounterChance() {;
		int chance = Random.Range (1,3);
		Debug.Log ("Encounter chance: " + chance);
		if (chance == 1) {
			Debug.Log ("Log: Fight encountered.");
			return true;
		}
		else
			return false;
	}
}
