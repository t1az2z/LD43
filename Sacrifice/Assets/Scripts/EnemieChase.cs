using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemieChase : StateMachineBehaviour {

    Enemy enemy;
    AudioSource steps;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        enemy = animator.GetComponent<Enemy>();
        enemy.ailerp.enabled = true;
        steps = animator.gameObject.GetComponent<Enemy>().steps;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (enemy.player.transform.position.x*1.1f > enemy.transform.position.x)
            enemy.sr.flipX = false;
        else
            enemy.sr.flipX = true;
        if (!animator.gameObject.GetComponent<AudioSource>().isPlaying)
        {

            steps.volume = Random.Range(.2f, .35f);
            steps.pitch = Random.Range(.8f, 1f);
            steps.Play();
        }

        if (!animator.GetComponent<Enemy>().alive)
        {
            animator.GetComponent<Enemy>().EnemyDeath();
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        steps.Stop();

    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
