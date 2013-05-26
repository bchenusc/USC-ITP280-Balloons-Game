using UnityEngine;
using System.Collections;

public class script_constrain : MonoBehaviour {

	public Transform target;
	public int offset_x = 0;
	public int offset_y = 0;
	
	void Update(){
		Vector3 newPosition = new Vector3(target.position.x + offset_x, target.position.y+offset_y,0);
		
		transform.position=newPosition;	
	}
	
}
