using UnityEngine;
using System.Collections;

public class script_balloon_float : MonoBehaviour {
	private float new_y;
	private float time;
	public float timeStep;
	public float stretch;
	
	// Use this for initialization
	void Start () {
		new_y = transform.position.y;
		time = 0f;
		timeStep = 0.03f;
		stretch = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		time = time + timeStep;
		new_y = transform.position.y + Mathf.Sin(time) * stretch;
		transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
	}
}
