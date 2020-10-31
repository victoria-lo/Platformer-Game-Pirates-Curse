using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private int rand;
    private Transform playerPos;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float difference = Mathf.Abs(playerPos.position.y - animator.transform.position.y);
        rand = Random.Range(0, 2);
        if (rand == 0 && difference <= 0.5f)
        {
            animator.SetTrigger("jump");
        }
        if (rand == 1 && difference <= 0.5f)
        {
            animator.SetTrigger("attack");
        }
    }
}