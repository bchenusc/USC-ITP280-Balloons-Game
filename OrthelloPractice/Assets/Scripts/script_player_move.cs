using UnityEngine;
using System.Collections;

public class script_player_move : MonoBehaviour {
	public bool has_balloon;
	private bool grounded;
	
	private bool hit_top;
	private bool keep_rising;
	
	private bool too_far_left;
	private bool too_far_right;
	
	private OTAnimatingSprite sprite;
	public int animSpeed = 5;
	Transform balloon;
	
	//Player settings
	private int ground_speed = 300;
	private int air_speed = 200;
	private int rise_speed = 100;
	
	void Awake(){
		Physics.IgnoreLayerCollision(0,8);	
	}
	
	// Use this for initialization
	// On initialize, player is grounded and unmoving, is not rising and has no balloon
	void Start () {
		sprite = transform.GetComponent<OTAnimatingSprite>();
		sprite.speed = 0;
		grounded = false;
		keep_rising = false;
		has_balloon = false;
	}
	
	// Update is called once per frame
	// Sprite horizontal default = facing right
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
		 	Application.LoadLevel(0);	
		}
		
		if (has_balloon) {
			#region HAS_BALLOON
			sprite.frameIndex = 4;
			if (keep_rising){
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, rise_speed, 0);
			}
			balloon.position = new Vector3(transform.position.x + 5, transform.position.y + 20, transform.position.z);
			
			// If both left and right
			if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
			}
			// If Left and not right and not too far left
			if (!too_far_left && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(-air_speed, rigidbody.velocity.y, 0);
				
			}
			// If right and not left and not too far right
			if (!too_far_right && Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
				rigidbody.velocity = new Vector3(air_speed, rigidbody.velocity.y, 0);
			}
			//If not left and not right
			if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
			}
#endregion
			
		} else {
			#region NO_BALLOON
			// If grounded and both left and right
			if (Input.GetKey(KeyCode.LeftArrow) && grounded && Input.GetKey(KeyCode.RightArrow)) {
				sprite.speed = 0;
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				sprite.frameIndex = 0;
			}
			// If left and grounded
			if (Input.GetKeyDown(KeyCode.LeftArrow) && grounded) {
				sprite.Play(1);
			}
			// If left and not too far left and grounded
			if (!too_far_left&& Input.GetKey(KeyCode.LeftArrow) && grounded && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(-ground_speed, rigidbody.velocity.y, 0);
				sprite.speed = animSpeed;
				sprite.flipHorizontal = true;
			}
			// If right and grounded
			if (Input.GetKeyDown(KeyCode.RightArrow) && grounded) {
				sprite.Play(1);
			}
			// If not too far right and right and grounded
			if (!too_far_right&& Input.GetKey(KeyCode.RightArrow) && grounded && !Input.GetKey(KeyCode.LeftArrow)) {
				rigidbody.velocity = new Vector3(ground_speed, rigidbody.velocity.y, 0);
				sprite.speed = animSpeed;
				sprite.flipHorizontal = false;
			}
			// If nothing
			if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				sprite.speed = 0;
				sprite.frameIndex = 0;
				transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
			}
			if(!grounded) {
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				sprite.speed = 0;
				sprite.frameIndex = 0;
			}
			#endregion
		}
	}
	
	public void changeGrounded(bool ground) {
		grounded = ground;
	}
	
	public void changeHasBalloon(bool hasballoon){
		rigidbody.velocity = new Vector3(0,0,0);
		has_balloon = hasballoon;	
	}
	
	public void changeGravity(bool gravity){
		rigidbody.useGravity = gravity;	
	}
	
	public void changeRising(bool rise){
		//Keep rising or stop?
		keep_rising = rise;	
	}
	
	
	public void tooFarLeft(bool left){
		too_far_left = left;
	}
	public void tooFarRight(bool right){
		too_far_right = right;	
	}
	
	public void stop_X_velocity(){
		transform.rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
	}
	
	public void toggleManCollider(bool toggle){
		if (toggle){
				transform.GetComponent<BoxCollider>().size = new Vector3(1,2,10);
				transform.GetComponent<BoxCollider>().center = new Vector3(0,0.8f,0);
		}
		else{
			//Original size of player collider
			transform.GetComponent<BoxCollider>().size = new Vector3 (1,1,1);
			transform.GetComponent<BoxCollider>().center = new Vector3(0,0,0);
		}
	}
	
	public void destroyBalloon(){
		Destroy(balloon.gameObject);
		changeHasBalloon(false);
		changeGravity(true);
		toggleManCollider(false);
		changeRising(false);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag("balloon")) {
			changeRising(true);
			toggleManCollider(true);
			balloon = other.transform;
			other.transform.position = new Vector3(other.transform.position.x, transform.position.y + 20, other.transform.position.z);
			changeGravity(false);
			changeHasBalloon(true);
			sprite.frameIndex = 4;
			
		}
	}
}
