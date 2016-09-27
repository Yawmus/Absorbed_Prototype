using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveOnComplete : MonoBehaviour {

	private float speed = 5f; 
	private Vector3 startingPos; 

	void Start(){
		startingPos = transform.position; 
	}

	void Update(){

		Debug.Log (enabled); //It always equals true, never false. FIGURE THIS OUT

		if (enabled) {
			gameObject.SetActive(false); 
		}
//			if (transform.position.z < -11) {
//				transform.Translate (speed * Time.deltaTime, 0, 0); 	
//			}
//		} else {
//			if(transform.position.x > startingPos.x)
//				transform.Translate (-speed * Time.deltaTime, 0, 0); 
//				//transform.position = Vector3.MoveTowards (transform.position, startingPos, speed * Time.deltaTime); 
//		}
	}

}