using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : StateMachineBehaviour
{
    private float waitTime;
    private GameObject moveSpot;
    public GameObject moveSpotObj;
    public GameObject physicIngredient;
    
    public float speed;
    public float startWaitTime;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveSpot = Instantiate(moveSpotObj);
        moveSpot.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.position = Vector2.MoveTowards(animator.transform.parent.position, moveSpot.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(animator.transform.parent.position, moveSpot.transform.position) < 0.2f) {
            if (waitTime <= 0) {
            moveSpot.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            waitTime = startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Destroy(moveSpot);
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
