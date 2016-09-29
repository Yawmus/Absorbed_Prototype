using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone : MonoBehaviour {

	BoxCollider b;
	Bin bin;
	public List<GameObject> paths = new List<GameObject>();

	Material old;
	public Material complete;
	List<GameObject> objects = new List<GameObject>();
	Queue<GameObject> removeObjects = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
		b = GetComponent<BoxCollider> ();
		bin = transform.parent.GetComponent<Bin> ();

		old = paths [1].GetComponent<MeshRenderer> ().material;
	}

	// Update is called once per frame
	void Update () {
		bool temp;

		foreach (GameObject g in objects) {
			if (!b.bounds.Intersects (g.GetComponent<Collider> ().bounds)) {
				removeObjects.Enqueue (g);
			}
		}
		while (removeObjects.Count > 0)
			objects.Remove(removeObjects.Dequeue ());

		temp = false;
		foreach (GameObject g in objects) {
			if (g.GetComponent<Orb> ().type == bin.expectedType) {
				temp = true;
			}
		}

		bin.satisfied = temp;

		if (bin.satisfied) {
			foreach (GameObject path in paths)
				path.GetComponent<MeshRenderer> ().material = complete;
			
		} else {
			foreach (GameObject path in paths)
				path.GetComponent<MeshRenderer> ().material = old;
		}
	}
	void OnTriggerEnter(Collider other){
		Orb b = other.gameObject.GetComponent<Orb> ();
		if (b == null)
			return;
		objects.Add (b.gameObject);
	}

	public List<GameObject> GetContainedObjects(){
		return objects;
	}
}
