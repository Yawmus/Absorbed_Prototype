﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 4f;
	float gravity = 9.8f;
	float vSpeed = 0;
	private RaycastHit hit;
    private bool rotatePlayer = false;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0f;
	bool absorbed = false;
	public Orb grabbedOrb;
	private CharacterController cc;
	int swapTS = 0;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update() {
        int mod = 1, rotFlip = 0;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
		float rotationX;
		Vector3 vel = (transform.forward * v + transform.right * h) * speed;



        
        if (absorbed)
        {
            switch (grabbedOrb.type)
            {
            case Orb.Type.Speed:
                vel *= 2f;
                break;
			case Orb.Type.Flip:
				mod = -1;
				rotFlip = 1;
                break;
            }
        }

		rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX * mod;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY * mod;
		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

		Ray ray = new Ray(transform.position, Vector3.up * mod);
		if ((cc.collisionFlags & CollisionFlags.Above) != 0 || cc.isGrounded) {
			Debug.Log ("on the ground or ceiling");
			vSpeed = 0;
			// We are grounded, so recalculate
			// move direction directly from axes

			if (Input.GetButton("Jump")) {
				vSpeed = jumpSpeed * mod;
			}
		}
		
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, rotFlip * 180);
		vSpeed -= (gravity * Time.deltaTime) * mod;
		vel.y = vSpeed;


		cc.Move(vel * Time.deltaTime);

		if (grabbedOrb == null) {
			ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
			if (Physics.Raycast (ray, out hit, 1.5f)) {
				GameObject go = hit.collider.gameObject;
				if (go.GetComponent<Orb> () != null) {
					go.GetComponent<Orb> ().Hover ();

					if (Input.GetMouseButtonDown (0)) {
						GrabOrb (go);
					}
				}
			}
		} else {
			grabbedOrb.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * .6f);
			grabbedOrb.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);

			// Dropping orb
			if (Input.GetMouseButtonDown (0)) {
				DropOrb ();
			}
			// Throwing orb
			else if(Input.GetButtonDown("Throw")){
				ThrowOrb ();
			}
			// Absorb orb
		}
	}

	Vector3 swapPos;
	GameObject swapObj;

	public void FixedUpdate()
	{
		
		if (Input.GetButtonDown ("Absorb")) {
			// Instantanious vs persistent
			if (grabbedOrb.type == Orb.Type.Swap && swapTS == 0) {
				swapTS = 1;
			} else {
				absorbed = !absorbed;
			}
		}

		if (swapTS == 1) {
			Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
			if (Physics.Raycast (ray, out hit, 40f)) {
				GameObject go = hit.collider.gameObject;
				if (go.GetComponent<Orb> () != null) {
					swapPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
					swapObj = go;
					transform.position = new Vector3 (go.transform.position.x, go.transform.position.y + 1, go.transform.position.z);
					swapTS = 2;
				}
			}
		} else if(swapTS == 4){
			swapObj.transform.position = swapPos;
			swapTS = 0;
		}
		else if(swapTS != 0){
			swapTS++;
		}
	}

	public void GrabOrb(GameObject go)
	{
		grabbedOrb = go.GetComponent<Orb> ();
		grabbedOrb.grabbed = true;
		foreach (Collider c in go.GetComponents<Collider>())
			c.enabled = false;
		grabbedOrb.GetComponent<Rigidbody>().Sleep ();
	}
	public void DropOrb()
	{
		Vector3 pos = Camera.main.transform.position + (Camera.main.transform.forward * .35f);
		Orb b = grabbedOrb.GetComponent<Orb>();
		b.grabbed = false;
		grabbedOrb.transform.position = pos;
		b.GetComponent<Rigidbody>().WakeUp();
		//b.GetComponent<Rigidbody>().useGravity = true;
		b.GetComponent<Rigidbody>().isKinematic = false;
		b.GetComponent<Rigidbody>().velocity = cc.velocity;
		foreach(Collider c in grabbedOrb.GetComponents<Collider>())
			c.enabled = true;
		grabbedOrb = null;
		absorbed = false;
	}
	public void ThrowOrb()
	{
		Vector3 pos = Camera.main.transform.position + (Camera.main.transform.forward * .35f);
		Orb b = grabbedOrb.GetComponent<Orb>();
		b.grabbed = false;
		grabbedOrb.transform.position = pos;
		b.GetComponent<Rigidbody>().WakeUp();
		//b.GetComponent<Rigidbody>().useGravity = true;
		b.GetComponent<Rigidbody>().isKinematic = false;
		if(b.type == Orb.Type.Speed)
		{
			b.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
		}
		else
		{
			b.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
		}
		foreach (Collider c in grabbedOrb.GetComponents<Collider>())
			c.enabled = true;
		grabbedOrb = null;
		absorbed = false;
	}
}
