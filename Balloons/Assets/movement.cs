using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	
	OTAnimatingSprite sprite_script;
	public Transform myPrefab;

	// Use this for initialization
	void Start () {
		sprite_script = transform.GetComponent<OTAnimatingSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
				sprite_script.frameIndex=1;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow) &&!Input.GetKey(KeyCode.RightArrow)){
			rigidbody.velocity = new Vector3(-300,rigidbody.velocity.y,0);
			sprite_script.flipHorizontal = true;
			sprite_script.speed = 5;
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow)){
				sprite_script.frameIndex=1;
		}
		
		if (Input.GetKey(KeyCode.RightArrow)&& !Input.GetKey(KeyCode.LeftArrow)){
			rigidbody.velocity = new Vector3(300,rigidbody.velocity.y,0);
			sprite_script.flipHorizontal = false;
			sprite_script.speed = 5;
		}
		
		if (!Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) || !Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) ){
				rigidbody.velocity = new Vector3(0, rigidbody.velocity.y,0);
				sprite_script.frameIndex =0;
				sprite_script.speed=0;
		}
		
		if (Input.GetKeyDown (KeyCode.Space)){
			Transform clone;
			clone = Instantiate (myPrefab, transform.position + new Vector3 (0,50,0), transform.rotation) as Transform;
			clone.rigidbody.AddForce (1000, 0 ,0);
			Destroy (clone.gameObject, 2);
		}
	}
	
	void OnCollisionEnter(Collision c){
		if (c.transform.name == "Player"){
				
		}
	}
}
