using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : StateMachineBehaviour {

    Vector2 playerPos;
    Vector2[] path;
    Pathfinder pf;
    float step;
    
     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = animator.gameObject.GetComponent<Enemy>().player.transform.position;
        pf = new Pathfinder(AINavMeshGenerator.instance);
        step = animator.gameObject.GetComponent<Enemy>().speed * Time.deltaTime;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        path = pf.FindPath(animator.transform.position, playerPos);
        foreach(var point in path)
        {
            if (Vector2.Distance(animator.transform.position, point) > .1f)
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, point, step);
            else
            {
                continue;
            }
        }   
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
	//}
}
