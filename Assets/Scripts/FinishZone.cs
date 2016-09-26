using UnityEngine;
using System.Collections;

public class FinishZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other){
		if (enabled && other.gameObject.GetComponent<Player> () != null) {
			Debug.Log ("Level done - load main menu");
			//Application.LoadLevel ("MainMenu");
		}
	}
}
