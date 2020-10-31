using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirahnaPlantAnim : StateMachineBehaviour
{
    private int rand;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0, 2);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rand == 0)
        {
            animator.SetTrigger("attack2");
        }
        if (rand == 1)
        {
            animator.SetTrigger("attack");
        }
    }
}
