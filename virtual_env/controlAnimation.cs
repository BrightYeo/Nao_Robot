using UnityEngine;
using System.Collections;

public class controlAnimation : MonoBehaviour {

    protected Animator animator;
   public static bool walk = false;
   public static bool work = false;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("StartWalkAnimation", false);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (animator)
        {
            AnimatorStateInfo stateInfor = animator.GetCurrentAnimatorStateInfo(0);
                   
                if (walk)
                {
                    animator.SetBool("StartWalkAnimation", true);
                  
                }
                else
                {
                    animator.SetBool("StartWalkAnimation", false);
                  
                }
        }
	}
}
