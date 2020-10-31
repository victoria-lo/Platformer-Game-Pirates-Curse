using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour
{
    float timer;
    private Transform playerPos;
    public float speed;
#pragma warning disable 649
    Vector3 target;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 5;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerPos.position.x < animator.transform.position.x) //if Player is on the left
        {
            animator.transform.eulerAngles = new Vector3(0, 0, 0);
            target = new Vector3(playerPos.position.x + 2, animator.transform.position.y, animator.transform.position.z);
        }
        else
        {
            animator.transform.eulerAngles = new Vector3(0, -180, 0);
            target = new Vector3(playerPos.position.x - 2, animator.transform.position.y, animator.transform.position.z);
        }
        if (animator.GetComponent<BunnyBoss>().health < 50)
        {
            speed = 7;
        }
        float difference = Mathf.Abs(playerPos.position.y - animator.transform.position.y);
        if (difference >= 0.5f)
        {
            animator.SetTrigger("idle");
        }
        if(timer <= 0)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            timer -= Time.deltaTime;
        }
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}