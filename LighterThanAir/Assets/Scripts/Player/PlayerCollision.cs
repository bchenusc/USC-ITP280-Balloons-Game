using UnityEngine;
using System.Collections;

/*
 * How to use this class?
 * 1. Place the class on the player character with the colliders. 
 * 2. Ensure the player has a collider 2D and rigidbody 2D attached.
 * 3. Ensure the player is tagged as player and on the player layer.
 * 
 * How to change HasBalloon and how does it affect other scripts?
 * 1. playerStats.HasBalloon = true;
 * 2. This will trigger an event system notifying each script balloon has changed.
 * 3. You should not need to do anything special after.
 * 
 * @Brian Chen
 * 
*/

//This may change based on what scripts on the player are affected by the player's collision.
[RequireComponent (typeof (Rigidbody))]
		//--Custom
[RequireComponent (typeof (PlayerStats))]
//After setting Required Components. Make sure to initialize them in the void Start().

public class PlayerCollision : MonoBehaviour {

	//Caching -- DO NOT TOUCH 
	private PlayerStats playerStats;
	private Animator animator;
	private BoxCollider2D myBoxCollider;
	private CircleCollider2D myCircleCollider;
	//------------------------

	private Transform t_balloon = null;

	//Balloon positioning
	private Vector3 v3_UpBalloonOffset = new Vector3(0.07f, 0.5f, 0);
	private Vector3 v3_DownBalloonOffset = new Vector3(0, 0.35f, 0);
	
	//Box collider info -- DO NOT TOUCH
	private Vector2 v2_ResetBoxColSize = new Vector2(0.35f, 0.45f);
	private Vector2 v2_ResetBoxColOffset = new Vector2 (0,-0.045f);
	private Vector2 v2_ResetCircleColOffset = new Vector2(0,0f);
	private float f_ResetCircleColRadius = 0.19f;

	//New collider size and offset for up balloon.
	private Vector2 v2_UP_NewBoxColSize = new Vector2(.35f, 1.1f); //new size y
	private Vector2 v2_UP_NewBoxColOffset= new Vector2(0, 0.45f); //new offset y

	//New collider size and offset for down balloon.
	private Vector2 v2_DOWN_NewBoxColSize = new Vector2(0.9f, 0.2f); //new size y
	private Vector2 v2_DOWN_NewBoxColOffset= new Vector2(0, -0.35f); //new offset y
	private Vector2 v2_DOWN_NewCircleColOffset= Vector2.zero;


	void OnCollisionEnter2D(Collision2D other){
		//Collision with spike
		if (other.gameObject.CompareTag("Spike")){

			#region up balloon
			if (PlayerStats.BalloonType.up == playerStats.HasBalloon && other.contacts[0].point.y > transform.position.y + 0.5f){
				DestroyBalloon();
			}
			else 
			if (PlayerStats.BalloonType.down == playerStats.HasBalloon && other.contacts[0].point.y < transform.position.y - 0.1f){
				DestroyBalloon();
			}
			else {
				//TODO - KILL SELF HERE
				Debug.Log("PlayerCollision: im dead");
			}
			#endregion
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		//Collision with balloon
		#region balloon up
		if (other.gameObject.CompareTag("BalloonUp") && playerStats.HasBalloon == PlayerStats.BalloonType.none){
			#region snap, parent, change collider, animate
			t_balloon = other.transform;
			t_balloon.parent = transform;
				//Snap balloon to right above the player.
			t_balloon.position = transform.position + v3_UpBalloonOffset;
			myBoxCollider.size = v2_UP_NewBoxColSize;
			myBoxCollider.center = v2_UP_NewBoxColOffset;
			playerStats.HasBalloon = PlayerStats.BalloonType.up;
			animator.SetBool("HasUpBalloon", true);
			#endregion
		}
		#endregion
		else
		#region balloon down
		if (other.gameObject.CompareTag("BalloonDown") && playerStats.HasBalloon == PlayerStats.BalloonType.none){
			#region snap, parent, change collider, animate
			//Snap the player to the balloons position.
			t_balloon = other.transform;
			transform.position = t_balloon.position + v3_DownBalloonOffset;

			//Then parent
			t_balloon.parent = transform;
				
				//CircleCollider transformations
			myCircleCollider.center = v2_DOWN_NewCircleColOffset;
				//BoxCollider transformations
			myBoxCollider.size = v2_DOWN_NewBoxColSize;
			myBoxCollider.center = v2_DOWN_NewBoxColOffset;
			playerStats.HasBalloon = PlayerStats.BalloonType.down;
			#endregion
		}
		#endregion
	}

	private void DestroyBalloon(){
		Destroy(t_balloon.gameObject);
		#region box collider reset
		myBoxCollider.size = v2_ResetBoxColSize;
		myBoxCollider.center = v2_ResetBoxColOffset;
		#endregion
		#region circlecollider reset
		myCircleCollider.radius = f_ResetCircleColRadius;
		myCircleCollider.center = v2_ResetCircleColOffset;
		#endregion

		playerStats.HasBalloon = PlayerStats.BalloonType.none;

		//Set all animator balloon variables to false.
		animator.SetBool("HasUpBalloon", false);
	}

	//We do our script caching here.
	void Start(){
		#region Cache Private variables
		playerStats = transform.GetComponent<PlayerStats>();
		animator = transform.GetComponent<Animator>();
		myBoxCollider = transform.GetComponent<BoxCollider2D>();
		myCircleCollider  = transform.GetComponent<CircleCollider2D>();
		#endregion

	}
}












