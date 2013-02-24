using UnityEngine;
using System.Collections;

public class script_balloon_float : MonoBehaviour {
	private bool has_guy;
	
	private float new_y;
	private float time;
	public float time_step;
	public float stretch;
	
	// Use this for initialization
	void Start () {
		new_y = transform.position.y;
		time = 0f;
		time_step = 0.03f;
		stretch = 0.2f;
		has_guy = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!has_guy) {
			time = time + time_step;
			new_y = transform.position.y + Mathf.Sin(time) * stretch;
			transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
		} else {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 300, 0);
			
			if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
				rigidbody.velocity = new Vector3(-250, rigidbody.velocity.y, 0);
			}
			if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
				rigidbody.velocity = new Vector3(250, rigidbody.velocity.y, 0);
			}
		}
	}
	
	public void has_Guy(bool change) {
		has_guy = change;	
	}
}
