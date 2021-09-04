using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_state : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ITEM",false);     animator.SetBool("BST",false); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
       
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK")) animator.SetInteger("State",-1);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK1")) animator.SetInteger("State",0);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK2")) animator.SetInteger("State",1);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK3")) animator.SetInteger("State",2);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLACK4")) animator.SetInteger("State",3);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.BLEACH")) animator.SetInteger("State",4);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.GOLD")) animator.SetInteger("State",5);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ORANGE")) animator.SetInteger("State",6);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.GRAPE")) animator.SetInteger("State",7);
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.RED")) animator.SetInteger("State",8);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
