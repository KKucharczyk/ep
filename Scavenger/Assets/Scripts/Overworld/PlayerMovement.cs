using UnityEngine;

using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rbody;
	private Animator anim;
	private Vector2 touchOrigin = -Vector2.one;
	private Vector2 movement_vector;

	// Use this for initialization
	void Start ()
	{
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float horizontal = 0f;
		float vertical = 0f;

		if (Warp.isWarping == false) {
			if (Input.touchCount > 0) {
				Touch myTouch = Input.touches [0];
				
				if (myTouch.phase == TouchPhase.Began) {
					touchOrigin = myTouch.position;
				} else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
					Vector2 touchEnd = myTouch.position;

					float x = touchEnd.x - touchOrigin.x;
					float y = touchEnd.y - touchOrigin.y;
					touchOrigin.x = -1;

					if (Mathf.Abs (x) > Mathf.Abs (y))
						horizontal = x > 0 ? 1 : -1;
					else
						vertical = y > 0 ? 1 : -1;
				}

				movement_vector = new Vector2 (horizontal, vertical);
			}

			if (movement_vector != Vector2.zero) {
				anim.SetBool ("isWalking", true);
				anim.SetFloat ("input_x", movement_vector.x);
				anim.SetFloat ("input_y", movement_vector.y);
			} else 
				anim.SetBool ("isWalking", false);

			rbody.MovePosition (rbody.position + movement_vector * Time.deltaTime);
			PlayerInformation.position = rbody.position;

			//movement_vector = new Vector2(0f,0f);
		}
	}
}
