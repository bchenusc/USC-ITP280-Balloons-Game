using UnityEngine;
using System.Collections;

public class script_guy_control : MonoBehaviour {
	private bool grounded;
	private OTAnimatingSprite sprite;
	public int animSpeed = 5;
	
	// Use this for initialization
	void Start () {
		sprite = transform.GetComponent<OTAnimatingSprite>();
		sprite.speed = 0;
		grounded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow) && grounded) {
			sprite.Play(1);
			sprite.speed = animSpeed;
			sprite.flipHorizontal = true;
		}
		if (Input.GetKey(KeyCode.LeftArrow) && grounded) {
			rigidbody.velocity = new Vector3(-300, rigidbody.velocity.y, 0);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && grounded) {
			sprite.Play(1);
			sprite.speed = animSpeed;
			sprite.flipHorizontal = false;
		}
		if (Input.GetKey(KeyCode.RightArrow) && grounded) {
			rigidbody.velocity = new Vector3(300, rigidbody.velocity.y, 0);
		}
		if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
			sprite.speed = 0;
			sprite.frameIndex = 0;
			transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
			
		}
	}
	
	void OnCollisionEnter(Collision other) {
		Vector3 relativePosition = transform.InverseTransformPoint(other.contacts[0].normal);
		if (relativePosition.y < 0) grounded = true;
	}
	
	void OnCollisionExit(Collision other) {
		Vector3 relativePosition = transform.InverseTransformPoint(other.contacts[0].normal);
		if (relativePosition.y >= 0) grounded = false;
	}
}
