using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject deathParticles;
    public Transform groundDetect;
    public float walkSpeed;
    public float distance = -7;
    public float health;
    public float atkDist;
    public float yAggroDis;

    public Transform target;
    public float chaseRadius; //Checks if Player is near Enemy so Enemy will stop its patrol

    private Animator anim;
    private bool m_FacingRight = false;
    private bool pathFind = true;
#pragma warning disable 649

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckDist();
        if (pathFind)
        {
            path();
        }
    }
    void OnTriggerEnter2D(Collider2D triggerCollider) //Enemy Lerps when attacked
    {
        if (triggerCollider.tag == "Hat")
        {
            Vector3 startPos = transform.position;
            Vector3 endPos;
            if (triggerCollider.transform.parent.position.x < transform.position.x) //Player attacks from left. push enemy right
            {
                endPos = new Vector3(transform.position.x + 30f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, endPos, 1 * Time.deltaTime);
            }
            if (triggerCollider.transform.parent.position.x > transform.position.x) //Player attacks from right. push enemy left
            {
                endPos = new Vector3(transform.position.x - 30f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, endPos, 1 * Time.deltaTime);
            }
            transform.position = Vector3.Lerp(transform.position, startPos, 1 * Time.deltaTime);
            health -= target.GetComponent<PlayerPlatformerController>().attackPower;
            if (health <= 0)
            {
                death();
            }
        }
    }
    void death()
    {
        deathParticles.SetActive(true);
        deathParticles.transform.parent = transform.parent;
        Destroy(gameObject);
        Destroy(deathParticles, 1.0f);
    }

    void CheckDist()
    {
        if (!target.gameObject.GetComponent<PlayerPlatformerController>().invisible && Math.Abs(target.position.y - transform.position.y) <= yAggroDis)
        {
            if (Math.Abs(target.position.x - transform.position.x) <= chaseRadius)
            {
                anim.SetBool("Attack", true);
                StartCoroutine(Attack());

            }
            else
            {
                anim.SetBool("Attack", false);
                pathFind = true;
                StopCoroutine(Attack());
            }
        }
    }
    void path()
    {
        transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector3.down, distance);

        if (groundInfo.collider == false || groundInfo.collider.gameObject.name == "Sky" || groundInfo.collider.gameObject.name == "Wall")
        {
            if (!m_FacingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                m_FacingRight = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                m_FacingRight = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundDetect.position, groundDetect.position + Vector3.down * distance);
    }
    IEnumerator Attack()
    {
        pathFind = false;
        Vector3 originalPosition = transform.position;
        Vector3 attackPosition;

        while (Math.Abs(target.position.y - transform.position.y) <= yAggroDis)
        {
            if (target.position.x < transform.position.x) //if Player is on the left
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                m_FacingRight = false;
                attackPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                m_FacingRight = true;
                attackPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }

            float attackSpeed = 2;
            float percent = 0;
            while (percent <= 1)
            {
                percent += Time.deltaTime * attackSpeed;
                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
                yield return null;
            }
        }
    }
}