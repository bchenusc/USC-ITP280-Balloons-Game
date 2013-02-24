using UnityEngine;
using System.Collections;

//Comment Comment

public class script_guy_control : MonoBehaviour {
	private bool has_balloon;
	private bool grounded;
	private OTAnimatingSprite sprite;
	public int animSpeed = 5;
	
	// Use this for initialization
	void Start () {
		sprite = transform.GetComponent<OTAnimatingSprite>();
		sprite.speed = 0;
		grounded = false;
		has_balloon = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (has_balloon) {
			sprite.frameIndex = 4;
			if (sprite.flipHorizontal == true)
				transform.position = new Vector3(transform.parent.position.x + 6, transform.parent.position.y - 20, transform.position.z);
			else transform.position = new Vector3(transform.parent.position.x - 5, transform.parent.position.y - 20, transform.position.z);
		} else {
			if (Input.GetKey(KeyCode.LeftArrow) && grounded && Input.GetKey(KeyCode.RightArrow)) {
				sprite.speed = 0;
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				sprite.frameIndex = 0;
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow) && grounded) {
				sprite.Play(1);
			}
			if (Input.GetKey(KeyCode.LeftArrow) && grounded && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(-300, rigidbody.velocity.y, 0);
				sprite.speed = animSpeed;
				sprite.flipHorizontal = true;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) && grounded) {
				sprite.Play(1);
			}
			if (Input.GetKey(KeyCode.RightArrow) && grounded && !Input.GetKey(KeyCode.LeftArrow)) {
				rigidbody.velocity = new Vector3(300, rigidbody.velocity.y, 0);
				sprite.speed = animSpeed;
				sprite.flipHorizontal = false;
			}
			if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				sprite.speed = 0;
				sprite.frameIndex = 0;
				transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
			}
		}
	}
	
	public void changeGrounded(bool ground) {
		grounded = ground;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag("balloon")) {
			other.transform.position = new Vector3(other.transform.position.x, transform.position.y + 20, other.transform.position.z);
			has_balloon = true;
			rigidbody.useGravity = false;
			transform.parent = other.transform;
			sprite.frameIndex = 4;
			other.transform.GetComponent<script_balloon_float>().has_Guy(true);
		}
	}
}
