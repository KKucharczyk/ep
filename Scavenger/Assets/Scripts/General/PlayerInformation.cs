using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour {

	public static bool encounterPossibility = true;

	// Hero position on the overworld map.
	public static Vector2 position = new Vector2(-7.5f, 2.2f);

	public static int lifePoints = 100;

	// Hero level
	public static int level = 1;
	
	// Experience points
	public static int experience = 0; 

}
