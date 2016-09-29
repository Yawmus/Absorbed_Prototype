using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OrbIndicators : MonoBehaviour {

	List<Bin> bins = new List<Bin>();
	public GameObject flip, swap, speed;
	public GameObject heldOrb;
	public Material swapMat, flipMat, speedMat;
	public GameObject player;
	Material basic;

	// Use this for initialization
	void Start () {
		foreach (Transform child in GameObject.Find("Bins").transform) {
			bins.Add(child.GetComponent<Bin>());
		}
		basic = heldOrb.GetComponent<Image> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Bin bin in bins) {
			switch (bin.expectedType) {
			case Orb.Type.Speed:
				speed.SetActive (!bin.satisfied);
				break;
			case Orb.Type.Swap:
				swap.SetActive (!bin.satisfied);
				break;
			case Orb.Type.Flip:
				flip.SetActive (!bin.satisfied);
				break;
			}
		}
		heldOrb.GetComponent<Image> ().material = basic;
		if (player.GetComponent<Player> ().grabbedOrb != null) {
			if (player.GetComponent<Player> ().absorbed) {
				switch (player.GetComponent<Player> ().grabbedOrb.type) {
				case Orb.Type.Speed:
					heldOrb.GetComponent<Image> ().material = speedMat;
					break;
				case Orb.Type.Flip:
					heldOrb.GetComponent<Image> ().material = flipMat;
					break;
				case Orb.Type.Swap:
					heldOrb.GetComponent<Image> ().material = swapMat;
					break;
				}
			}

		}

	}
}
