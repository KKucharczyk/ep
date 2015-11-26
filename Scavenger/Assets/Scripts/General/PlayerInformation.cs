using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour {

	// -------------------------------------------------
	// Overworld parameters
	// -------------------------------------------------

	//private static Vector2 houseInteriorLocation = new Vector2();
	private static Vector2 inFrontOfHouseLocation = new Vector2(-7.5f, 2.2f);

	// Hero position on the overworld map.
	public static Vector2 position = inFrontOfHouseLocation;

	// Blocks multiple encounters in the same spawn point
	public static bool encounterPossibility = true;


	// -------------------------------------------------
	// RPG stats
	// -------------------------------------------------

	// Life points
	public static int lifePoints = 1000;

	// Hero level
	public static int level = 1;
	
	// Experience points
	public static int experience = 0; 

	public static int strength = 1;
	public static int dexterity = 1;
	public static int luck = 1;
	

	// -------------------------------------------------
	// Battle mode parameters
	// -------------------------------------------------
	
	public static bool chanceForBlocking = false;

	public static bool isAttackBlocked = false;

}
