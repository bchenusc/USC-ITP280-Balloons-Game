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
	//------------------------

	//Balloon positioning
	private float f_BalloonOffsetX = 0.07f;
	private float f_BalloonOffsetY = 0.5f;
	private Transform t_balloon = null;

	//Box collider info -- DO NOT TOUCH
	private float f_BColSizeX = 0.35f; //original size x
	private float f_BColOffsetX = 0; //original offset x
	private float f_OGBColSizeY = 0.3f; //original size y
	private float f_OGBColOffsetY = 0.02f; //original offset y
	private float f_BALBColSizeY = 1.1f; //new size y
	private float f_BALBColOffsetY= 0.45f; //new offset y


	void OnCollisionEnter2D(Collision2D other){
		//Collision with spike
		if (other.gameObject.CompareTag("Spike")){
			if (other.contacts[0].point.y > transform.position.y + 0.5f){
				DestroyBalloon();
			}
			else {
				//TODO - KILL SELF HERE
				Debug.Log("PlayerCollision: im dead");
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		//Collision with balloon
		if (other.gameObject.CompareTag("Balloon")){
			t_balloon = other.transform;
			t_balloon.parent = transform;
			//Snap balloon to right above the player.
			t_balloon.position = transform.position + new Vector3 (f_BalloonOffsetX, f_BalloonOffsetY);
			myBoxCollider.size = new Vector2 (f_BColSizeX,f_BALBColSizeY);
			myBoxCollider.center = new Vector2(f_BColOffsetX,f_BALBColOffsetY);
			playerStats.HasBalloon = true;
			animator.SetBool("HasUpBalloon", true);
		}
	}

	private void DestroyBalloon(){
		Destroy(t_balloon.gameObject);
		myBoxCollider.size = new Vector2 (f_BColSizeX,f_OGBColSizeY);
		myBoxCollider.center = new Vector2(f_BColOffsetX,f_OGBColOffsetY);
		playerStats.HasBalloon = false;
		animator.SetBool("HasUpBalloon", false);
	}

	//We do our script caching here.
	void Start(){
		#region Cache Private variables
		playerStats = transform.GetComponent<PlayerStats>();
		animator = transform.GetComponent<Animator>();
		myBoxCollider = transform.GetComponent<BoxCollider2D>();
		#endregion

	}
}












