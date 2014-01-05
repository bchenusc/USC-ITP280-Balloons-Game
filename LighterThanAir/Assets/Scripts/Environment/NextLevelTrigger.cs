using UnityEngine;
using System.Collections;

/*
 * How to use this script:
 * 1. Place on the next level trigger prefab.
 * 
 * 
 * @Brian Chen
 */

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class NextLevelTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Player")){
			other.GetComponent<PlayerMove>().enabled = false;
			other.GetComponent<Rigidbody2D>().isKinematic = true;
			GameState.Get.FadeOutToNextLevel();
		}
	}
}
