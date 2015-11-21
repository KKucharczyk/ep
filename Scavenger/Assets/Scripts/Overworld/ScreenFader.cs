using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	Animator anim;
	bool isFading = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}

	// Fading animation Clear -> Black
	public IEnumerator Fading() {
		isFading = true;
		anim.SetTrigger ("FadeIn");
		while (isFading)
			yield return null;
	}

	// Unfading animation Black -> Clear
	public IEnumerator Unfading() {
		isFading = true;
		anim.SetTrigger ("FadeOut");
		while (isFading)
			yield return null;
	}

	void AnimationComplete() {
		isFading = false;
	}
}
