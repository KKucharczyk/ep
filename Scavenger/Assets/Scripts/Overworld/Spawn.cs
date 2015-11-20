using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	IEnumerator OnTriggerEnter2D(Collider2D other) {
		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader>();

		if (other.tag == "Player" && PlayerInformation.encounterPossibility == true) {
			if(countEncounterChance() == true) {
				yield return StartCoroutine(sf.FadeToBlack());
				Application.LoadLevel("Battle Mode");
			}
		}
	}

	void OnTriggerExit2D() {
		PlayerInformation.encounterPossibility = true;
	}

	private bool countEncounterChance() {;
		int encounterChance = Random.Range (1,5);
		Debug.Log ("Encounter chance: " + encounterChance);
		if (encounterChance == 1) {
			Debug.Log ("Log: Fight encountered.");
			return true;
		}
		else
			return false;
	}
}
