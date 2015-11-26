using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject {

	public int wallDamage = 1;
	public float restartLevelDelay = 1f;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;

	public int damage = 10;

	private Vector2 touchOrigin = -Vector2.one;

	private Animator animator;

	private Text lifePoints;

	// Use this for initialization
	protected override void Start () {
		animator = GetComponent<Animator> ();
		lifePoints = GameObject.Find ("playerLifePointsText").GetComponent<Text>();
		lifePoints.text = "Life: " + PlayerInformation.lifePoints;
		base.Start ();
	}

	protected override void AttemptMove<T>(int xDir, int yDir) {
		base.AttemptMove<T> (xDir, yDir);
		RaycastHit2D hit;
		if (Move (xDir, yDir, out hit)) {
			SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
		}

		checkIfGameOver ();
		GameManager.instance.playersTurn = false;
	}

	private void checkIfGameOver() {
		if (PlayerInformation.lifePoints <= 0) {
			//SoundManager.instance.PlaySingle(gameOverSound);
			//SoundManager.instance.musicSource.Stop();
			GameManager.instance.GameOver ();
		}
	}

	protected override void OnCantMove<T>(T component) {
		Enemy hitWall = component as Enemy;
		//hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("playerChop");
		hitWall.loseLifePoints (damage);


	}

	private void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Exit") {
			PlayerInformation.encounterPossibility = false;
			Application.LoadLevelAsync("Overworld");
		} 
	}

	public void loseLifePoints(int loss) {
		animator.SetTrigger ("playerHit");
		PlayerInformation.lifePoints -= loss;
		lifePoints.text = "Life: " + PlayerInformation.lifePoints;
		checkIfGameOver ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerInformation.chanceForBlocking == true)
			StartCoroutine (wait ());

		if (!GameManager.instance.playersTurn)
			return;

		int horizontal = 0;
		int vertical = 0;

#if UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		if (horizontal != 0) {
			vertical = 0;
		}

#else

		if(Input.touchCount > 0)
		{
			Touch myTouch = Input.touches[0];

			if(myTouch.phase == TouchPhase.Began)
			{
				touchOrigin = myTouch.position;
			}

			else if(myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				Vector2 touchEnd = myTouch.position;
				float x = touchEnd.x - touchOrigin.x;
				float y = touchEnd.y - touchOrigin.y;
				touchOrigin.x = -1;
				if(Mathf.Abs (x) > Mathf.Abs(y))
					horizontal = x > 0 ? 1 : -1;
				else
					vertical = y > 0 ? 1 : -1;
			}
		}

#endif

		if (horizontal != 0 || vertical != 0)
			AttemptMove<Enemy> (horizontal, vertical);
	}

	IEnumerator wait() {
		//Debug.Log ("Waiting");
		yield return new WaitForSeconds (5f);
	}
}
