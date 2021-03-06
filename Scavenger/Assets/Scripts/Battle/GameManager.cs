﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;
	public int playerFoodPoints = 100;
	public static GameManager instance = null;
	[HideInInspector] public bool playersTurn = true;

	private GameObject levelImage = null;
	public BoardManager boardScript;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		//DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy>();
		boardScript = GetComponent<BoardManager>();
		//InitGame ();
	}

	private void OnLevelWasLoaded(int index) {
		InitGame ();
	}

	void InitGame() {
		doingSetup = true;
		Invoke ("HideLevelImage", levelStartDelay);

		enemies.Clear ();
		boardScript.SetupScene ();
	}

	private void HideLevelImage() {
		doingSetup = false;
	}

	public void GameOver() {
		levelImage.SetActive (false);
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (playersTurn || enemiesMoving || doingSetup)
			return;

		StartCoroutine (MoveEnemies());
	}

	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}

	IEnumerator MoveEnemies() {
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if (enemies.Count == 0) {
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) {
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		playersTurn = true;
		enemiesMoving = false;
	}
}
