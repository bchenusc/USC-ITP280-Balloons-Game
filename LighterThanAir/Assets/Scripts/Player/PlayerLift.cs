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
	private float f_maxFallingSpeed;

	private PlayerStats.BalloonType bt_balloon;

	public float y_velocity;

	#region Delegate functions
	private void GotBalloon(PlayerStats.BalloonType bt){
		if (bt == PlayerStats.BalloonType.up){
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.up * f_maxLiftSpeed);
			bt_balloon = bt;
			return;
		}

		if(bt == PlayerStats.BalloonType.down){
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (-Vector2.up * f_maxFallingSpeed);
			bt_balloon = bt;
			return;
		}

		if(bt == PlayerStats.BalloonType.none){
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.gravityScale = 1;
			bt_balloon = bt;
			return;
		}
	}
	#endregion

	void Awake(){
		#region Setup Delegate
			PlayerStats.OnIsBallooned += GotBalloon;
		#endregion
	}
	void OnDestroy(){
		try{
			PlayerStats.OnIsBallooned -= GotBalloon;
		}catch{};
	}

	void Start(){
		playerStats = transform.GetComponent<PlayerStats>();
		f_maxLiftSpeed = playerStats.A_RisingMaxSpeed;
		f_maxFallingSpeed = playerStats.A_FallingMaxSpeed;
	}

	void Update(){
		#region keep pushing upward if y velocity is stopped.
		if (rigidbody2D.velocity.y < 0.1f){
			if (bt_balloon != PlayerStats.BalloonType.none){

				if (bt_balloon == PlayerStats.BalloonType.up){
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, f_maxLiftSpeed);
				}else if (bt_balloon == PlayerStats.BalloonType.down){
					rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -f_maxFallingSpeed);
				}

			}
		}
		#endregion
	}
}













