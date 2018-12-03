using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : StateMachineBehaviour {

    /*Vector3 target;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        target = new Vector2(animator.transform.position.x + Random.Range(-1, 1), animator.transform.position.y+ Random.Range(-1, 1));
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.transform.position != target && animator.GetBool("Wandering"))
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, animator.gameObject.GetComponent<Enemy>().speed * Time.deltaTime);
        else
            animator.SetBool("Wandering", false);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}*/
}
