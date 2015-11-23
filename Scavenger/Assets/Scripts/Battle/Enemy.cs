using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MovingObject {

	public int playerDamage;
	private Animator animator;
	private Transform target;
	private bool skipMove;
	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;

	public static int  lifePoints = 50;
	private Text lifePointsText;

	private bool enemyAlive = true;

	// Use this for initialization
	protected override void Start () {
		GameManager.instance.AddEnemyToList (this);
		enemyAlive = true;
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		lifePointsText = GameObject.Find ("enemyLifePointsText").GetComponent<Text>();
		lifePointsText.text = "Life: 50";
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

	protected override void OnCantMove<T>(T component) {
		if (enemyAlive) {
			Player hitPlayer = component as Player;
			animator.SetTrigger ("enemyAttack");
			hitPlayer.loseLifePoints (playerDamage);

			//SoundManager.instance.RandomizeSfx (enemyAttack1, enemyAttack2);

		}
	}

	public void loseLifePoints(int loss) {
		if (enemyAlive) {
			Enemy.lifePoints -= loss;
			lifePointsText.text = "Life: " + Enemy.lifePoints;
			checkIfDead ();
		}
	}

	private void checkIfDead() {
		if (lifePoints <= 0) {
			//SoundManager.instance.PlaySingle(gameOverSound);
			//SoundManager.instance.musicSource.Stop();
			enemyAlive = false;
			gameObject.SetActive(false);
		}
	}

}
