using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour
{

    Animator anim;
    int jumpHash = Animator.StringToHash("jump");


	// Use this for initialization
	void Start ()
    {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
    {

        float move = Input.GetAxis("Vertical");
        anim.SetFloat("speed", move);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(jumpHash);
        }
	
	}
}
