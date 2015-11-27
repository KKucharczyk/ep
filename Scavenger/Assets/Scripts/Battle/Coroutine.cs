using UnityEngine;
using System.Collections;
using System;

public class Coroutine : MonoBehaviour
{ 
	void Update() {
		if (PlayerInformation.chanceForBlocking) {
			//if(Input.GetKeyDown(KeyCode.Return)) {
			if(Input.acceleration.y >= -0.5) {
				PlayerInformation.isAttackBlocked = true;

			}
		}
	}
}