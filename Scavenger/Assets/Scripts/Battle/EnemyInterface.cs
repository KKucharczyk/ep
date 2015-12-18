using UnityEngine;
using System.Collections;

public interface EnemyInterface {

	// Use this for initialization

	void Start ();
	void AttemptMove<T> (int xDir, int yDir);
	void MoveEnemy();
	void OnCantMove<T>(T component);
}
