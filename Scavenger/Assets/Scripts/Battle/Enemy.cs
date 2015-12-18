using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MovingObject {

	// components
	private Animator animator;
	private Transform target;
	// sounds
	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;
	// HUD
	private Text lifePointsText;
	private Image blockPossibilityImage1;
	private Image blockPossibilityImage2;
	private Image blockPossibilityImage3;
	private Image blockPossibilityImage4;

	// amount of damage dealt to player	
	public int playerDamage;


	private bool skipMove;
	private bool enemyAlive = true;
	public static int lifePoints;


	
	protected override void Start () {
		GameManager.instance.AddEnemyToList (this);
		enemyAlive = true;
		lifePoints = 50;

		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		lifePointsText = GameObject.Find ("enemyLifePointsText").GetComponent<Text>();
		blockPossibilityImage1 = GameObject.Find ("blockPossibilityImage1").GetComponent<Image> ();
		blockPossibilityImage2 = GameObject.Find ("blockPossibilityImage2").GetComponent<Image> ();
		blockPossibilityImage3 = GameObject.Find ("blockPossibilityImage3").GetComponent<Image> ();
		blockPossibilityImage4 = GameObject.Find ("blockPossibilityImage4").GetComponent<Image> ();

		lifePointsText.text = "Life: 50";
		blockPossibilityImage1.gameObject.SetActive (false);
		blockPossibilityImage2.gameObject.SetActive (false);
		blockPossibilityImage3.gameObject.SetActive (false);
		blockPossibilityImage4.gameObject.SetActive (false);

		base.Start ();
	}

	protected override void AttemptMove<T> (int xDir, int yDir) 
	{
		if (enemyAlive) {
			if (skipMove) {
				skipMove = false;
				return;
			}

			base.AttemptMove<T> (xDir, yDir);
			skipMove = true;
		}
	}

	public void MoveEnemy() {
		if (enemyAlive) {
			int xDir = 0;
			int yDir = 0;

			if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon) 
				yDir = target.position.y > transform.position.y ? 1 : -1;
			else
				xDir = target.position.x > transform.position.x ? 1 : -1;

			AttemptMove<Player> (xDir, yDir);
		}
	}

	public void loseLifePoints(int loss) {
		if (enemyAlive) {
			Enemy.lifePoints -= loss;
			lifePointsText.text = "Life: " + Enemy.lifePoints;
			//checkIfDead ();
		}
	}

	public void checkIfDead() {
		if (lifePoints <= 0) {
			enemyAlive = false;
			gameObject.SetActive(false);
			Application.LoadLevelAsync("Overworld");
		}
	}

	protected override void OnCantMove<T>(T component) {
		if (enemyAlive) {

			Player hitPlayer = component as Player;
			int blockChance = Random.Range(1,3);

			// if there is a chance to block attack...
			if(blockChance == 1) { 
				int blockDirection = Random.Range(1,5);
				// wait on other thread fixed time and wait for input in coroutine
				Invoke("closeBlockInterval", 0.7f);
				switch(blockDirection) {
				case 1:
					blockPossibilityImage1.gameObject.SetActive (true);
					break;
				case 2:
					blockPossibilityImage2.gameObject.SetActive (true);
					break;
				case 3:
					blockPossibilityImage3.gameObject.SetActive (true);
					break;
				case 4:
					blockPossibilityImage4.gameObject.SetActive (true);
					break;
				}

				// variable for coroutine script
				PlayerInformation.chanceForBlocking = true;
				// blocking coroutine
				StartCoroutine(wait(hitPlayer));
			}
			// if there is no possibility to block enemy attack...
			else {	
				//Debug.Log ("Standard attack - 10 hp.");
				animator.SetTrigger ("enemyAttack");
				hitPlayer.loseLifePoints (playerDamage);
			}
		}
	}

	private void closeBlockInterval() {
		PlayerInformation.chanceForBlocking = false;
		blockPossibilityImage1.gameObject.SetActive (false);
		blockPossibilityImage2.gameObject.SetActive (false);
		blockPossibilityImage3.gameObject.SetActive (false);
		blockPossibilityImage4.gameObject.SetActive (false);

	}

	IEnumerator wait(Player hitPlayer) {
		// wait while there is a chance for blocking
		yield return new WaitForSeconds (0.7f);
		// enemy attack animation
		animator.SetTrigger ("enemyAttack");
		//Debug.Log ("Blocking possible!");
		// if attack was not blocked...	
		if (PlayerInformation.isAttackBlocked == false) {
			// ... make player lose lifePoints
			hitPlayer.loseLifePoints (playerDamage);
			//Debug.Log ("Attack not deflected! Losing 10hp.");
		}
		// if attack was blocked succesfully..
		else
			Debug.Log ("Nope"); // Do nothing.

		// reset blocking flag
		PlayerInformation.isAttackBlocked = false;
	}



}
