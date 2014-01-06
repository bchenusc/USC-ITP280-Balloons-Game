using UnityEngine;
using System.Collections;

/*
 * How to use this class?
 * 1. Place the class on the player character with the colliders. 
 * 2. Ensure the player has a collider 2D and rigidbody 2D attached.
 * 3. Ensure the player is tagged as player and on the player layer.
 * 4. Customize float numbers located at the top of the script to tweak horizontal movement values.
 * 
 * Notes:
 * Class Handles:
 * 		LEFT GROUNDED
 * 		LEFT RISING
 * 		RIGHT GROUNDED
 * 		RIGHT RISING
 * 
 * Does NOT handle:
 * 		MOVING UP
 * 		MOVING DOWN
 * 		COLLISION
 * 
 * @Brian Chen
 * 
*/

[RequireComponent (typeof (PlayerStats))]

public class PlayerMove : MonoBehaviour {

	//Temporary variables -- DO NOT TOUCH (variables are set from playerstats.
	public float f_horiz = 0;
	public float f_maxSpeed=0; //horizontal only
	public float f_moveForce=0; //horizontal only
	public  PlayerStats.BalloonType bt_hasBalloon;
	public bool b_isGrounded = false;
	//------------------------------------

	Animator animator;
	PlayerStats playerStats;

	void Start () {
		animator = transform.GetComponent<Animator>();
		playerStats = transform.GetComponent<PlayerStats>();

		f_maxSpeed = playerStats.G_HorizontalMaxSpeed;
		f_moveForce = playerStats.G_HorizontalForce;
	}
	

	void FixedUpdate () {
		#region Computer Controls
		if (b_isGrounded || bt_hasBalloon != PlayerStats.BalloonType.none){
			HandleInput();
			Animate();
		}
		#endregion
	}

#region Other Script Function Calls

	public void GotBalloon(PlayerStats.BalloonType bt){
		//Checks the player stats to see if player is ballooned or not.
		if (bt != PlayerStats.BalloonType.none){
			f_maxSpeed = playerStats.A_HorizontalMaxSpeed;
			bt_hasBalloon = playerStats.HasBalloon;
		}
		else
			f_maxSpeed = playerStats.G_HorizontalMaxSpeed;
			bt_hasBalloon = playerStats.HasBalloon;
			return;
	}

	public void IsGrounded(){
		b_isGrounded = playerStats.IsGrounded;
	}

#endregion

#region Private functions
	private void HandleInput(){

		f_horiz = Input.GetAxisRaw("Horizontal"); //Gather input

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(f_horiz != 0 && f_horiz * rigidbody2D.velocity.x < f_maxSpeed)
			// ... add a force to the player.
			//rigidbody2D.AddForce(Vector2.right * f_horiz * f_moveForce);
			rigidbody2D.velocity = new Vector2(Mathf.Sign(f_horiz) * f_maxSpeed, rigidbody2D.velocity.y);
		// If the player's horizontal velocity is greater than the maxSpeed...
		/*if(f_horiz != 0 && Mathf.Abs(rigidbody2D.velocity.x) > f_maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * f_maxSpeed, rigidbody2D.velocity.y);
			*/
		if (f_horiz == 0){
			rigidbody2D.velocity = Vector2.up * rigidbody2D.velocity.y;
		}
		// If the input is moving the player right and the player is facing left...
		if(f_horiz > 0.1f && !playerStats.FacingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(f_horiz < -0.1f && playerStats.FacingRight)
			// ... flip the player.
			Flip();
		
	}

	private void Flip ()
	{
		// Switch the way the player is labelled as facing.
		playerStats.FacingRight = !playerStats.FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	private void Animate(){
		//Check if there is an animator. If there is no animator, then do not animate
		if (animator == null || animator.runtimeAnimatorController == null)
			return;

		try
			{
			//Handle animation here.
				animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
			}
		catch{}
	}
#endregion


#region Delegate Calls
	void Awake(){
		PlayerStats.OnIsBallooned += GotBalloon;
		PlayerStats.OnIsGrounded += IsGrounded;
	}
	void OnDisabled(){
		PlayerStats.OnIsBallooned -= GotBalloon;
		PlayerStats.OnIsGrounded -= IsGrounded;
	}
#endregion
}
