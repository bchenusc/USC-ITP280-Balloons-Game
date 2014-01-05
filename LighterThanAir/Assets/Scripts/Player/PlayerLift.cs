using UnityEngine;
using System.Collections;

/*
 * How to use this class?
 * 1. Place the class on the player character with the colliders. 
 * 2. Ensure the player has a collider 2D and rigidbody 2D attached.
 * 3. Ensure the player is tagged as player and on the player layer.
 * 
 * Notes: 
 * This class lifts the player up at a constant rate defined in PlayerStats.
 * 
 * @Brian Chen
 * 
*/


[RequireComponent (typeof (PlayerStats))]

public class PlayerLift : MonoBehaviour {

	//Caching
	private PlayerStats playerStats;

	private float f_maxLiftSpeed;

	#region Delegate functions
	private void GotBalloon(){
		newVelocity(playerStats.HasBalloon);
	}
	#endregion

	void Awake(){
		#region Setup Delegate
			PlayerStats.OnIsBallooned += GotBalloon;
		#endregion
	}

	void Start(){
		playerStats = transform.GetComponent<PlayerStats>();
		f_maxLiftSpeed = playerStats.A_RisingMaxSpeed;
	}

	private void newVelocity(bool t){
		if (t){
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.up * f_maxLiftSpeed);
		}
		else {
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.gravityScale = 1;
		}
	}

}













