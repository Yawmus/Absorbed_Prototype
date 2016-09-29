using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour
{

    Animator anim;
    int jumpHash = Animator.StringToHash("jump");
    int holdingHash = Animator.StringToHash("holding_orb");
    int notholdingHash = Animator.StringToHash("not_holding_orb");
    float distance_speed = 10.0f;
    float distance_flip = 10.0f;
    float distance_swap = 10.0f;
    public GameObject righthand;
    public GameObject speedorb;
    public GameObject fliporb;
    public GameObject swaporb;
  
    

	// Use this for initialization
	void Start ()
    {

        anim = GetComponent<Animator>();
        righthand = GameObject.Find("Player/arms/locator_right_arm/R_ForeArm/R_Hand");
        speedorb = GameObject.Find("Orbs/Speed orb/Point light");
        fliporb = GameObject.Find("Orbs/Flip orb/Point light");
        swaporb = GameObject.Find("Orbs/Swap orb/Point light");
        //anim.SetTrigger(notholdingHash);
        


    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Vertical");
        anim.SetFloat("speed", move);

        // Check for Spacebar press.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(jumpHash);
        }


        //Check between distance of speedorb and palm of hand.  Hold or not old animation.
        distance_speed = Vector3.Distance(righthand.transform.position, speedorb.transform.position);
        distance_flip = Vector3.Distance(righthand.transform.position, fliporb.transform.position);
        distance_swap = Vector3.Distance(righthand.transform.position, swaporb.transform.position);
        //Debug.Log("Distance between speed orb and hand is " + distance_speed); //prints to console distance between hand and orb

        //holding or not holding SPEED ORB and driving corresponding animation.
        if (distance_speed < .099f)
        {
            anim.ResetTrigger(notholdingHash);
            anim.SetTrigger(holdingHash);
        }
        else if (distance_flip < .099f)
        {
            anim.ResetTrigger(notholdingHash);
            anim.SetTrigger(holdingHash);
        }
        else if (distance_swap < .099f)
        {
            anim.ResetTrigger(notholdingHash);
            anim.SetTrigger(holdingHash);
        }
        else
        {
            anim.ResetTrigger(jumpHash);
            anim.ResetTrigger(holdingHash);
            if (distance_speed > .1f)
            {
                anim.SetTrigger(notholdingHash);
            }
            else if (distance_swap > .1f)
            {
                anim.SetTrigger(notholdingHash);
            }
            else if (distance_flip > .1f)
            {
                anim.SetTrigger(notholdingHash);
            }
        }

        
        

        
        

        
              

    }        
}
