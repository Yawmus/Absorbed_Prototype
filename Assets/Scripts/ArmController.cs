using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour
{

    Animator anim;
    int jumpHash = Animator.StringToHash("jump");
    int holding_orbHash = Animator.StringToHash("holding_orb");
    float distance = 10.0f;
    public GameObject righthand;
    public GameObject speedorb;
    public GameObject fliporb;
    public GameObject swaporb;
  
    

	// Use this for initialization
	void Start ()
    {

        anim = GetComponent<Animator>();
        righthand = GameObject.Find("Player/arms/locator_right_arm/R_ForeArm/R_Hand/R_FingerBase");
        speedorb = GameObject.Find("Orbs/Speed Orb");
        fliporb = GameObject.Find("Orbs/Flip Orb");
        swaporb = GameObject.Find("Orbs/Swap Orb");
        


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

        distance = Vector3.Distance(righthand.transform.position, speedorb.transform.position);
        if (distance<10)
        {
            anim.SetTrigger(holding_orbHash);
        }

	
	}
}
