using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Camera myCam;
	public float m_speed = 0.1f;
	public float scale = 5f;

	public GameObject player;


	IEnumerator Start () {
		ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader>();

		// inicjalizacja postaci na współrzędnych podanych w PlayerInformation
		GameObject.Instantiate (player, PlayerInformation.position, Quaternion.identity);
		myCam = GetComponent<Camera> ();
		yield return StartCoroutine(sf.FadeToClear());
	}

	void Update () {
		myCam.orthographicSize = (Screen.height / 100f) / scale;	
		if (target)
			transform.position = Vector3.Lerp(transform.position, PlayerInformation.position, m_speed) + new Vector3(0,0,-10);

	}
}
