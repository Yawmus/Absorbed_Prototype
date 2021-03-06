﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	List<Bin> bins = new List<Bin>();
	GameObject finishZone;
	public GameObject door; 

	// Use this for initialization
	void Start () {
		//Debug.Log (GameObject.Find ("Bins").transform == null);
		foreach (Transform child in GameObject.Find("Bins").transform) {
			bins.Add(child.GetComponent<Bin>());
		}
		finishZone = transform.FindChild ("FinishZone").gameObject;
		finishZone.GetComponent<MeshRenderer> ().material.color = Color.black;
		finishZone.GetComponent<FinishZone> ().enabled = false;
		door.GetComponent<MoveOnComplete> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool temp = true;
		foreach (Bin bin in bins)
			if (!bin.satisfied) {
				temp = false;
			}

		// Open door, level complete
		if (temp) {
			finishZone.GetComponent<MeshRenderer> ().material.color = Color.white;
			finishZone.GetComponent<FinishZone> ().enabled = true;
			door.GetComponent<MoveOnComplete> ().enabled = true;
			Debug.Log ("Level complete - open door");
		} else {
			finishZone.GetComponent<MeshRenderer> ().material.color = Color.black;
			finishZone.GetComponent<FinishZone> ().enabled = false;
			door.GetComponent<MoveOnComplete> ().enabled = false;
			//Debug.Log ("LEVEL NOT COMPLETE"); 
		}
	}

	public void ExitLevel()
	{
		Debug.Log ("Goes to main menu");
		//Application.LoadLevel("MainMenu");
	}
}
