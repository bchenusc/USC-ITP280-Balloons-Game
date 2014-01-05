using UnityEngine;
using System.Collections;

public class BlockSnap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (Round (transform.position.x), 
		                                  Round(transform.position.y),
		                                  0);
	}

	//Rounds to nearest 0.5f;
	float Round(float f){
		float floor = Mathf.Floor(f);
		float ceiling = Mathf.Ceil(f);

		//example boolean: 7.6 >= 7 + 0.5  @result floor = 7.5.
		if (f >= floor + 0.5f)
		{
			floor += 0.5f;
		}else{
			ceiling -= 0.5f;
		}

		float toRound = f-floor;
		//example toRound = 0.1 = 7.6 - 7.5 
		if (toRound < 0.25f){
			return floor;
		}else
		{
			return ceiling;
		}
	}
}

















