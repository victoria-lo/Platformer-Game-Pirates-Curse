using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlocks : MonoBehaviour
{
    Vector3 startPos, endPos;
    public float speed;
    public float posX;

    private bool right;
    private bool onboard = false;
    PhysicsObject player;

    void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = new Vector3(posX, transform.position.y, transform.position.z);
        if (startPos.x > posX)
            right = false;
        else
            right = true;
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PhysicsObject>();
        if (transform.position.x > Math.Max(startPos.x, endPos.x))
            right = false;
        if (transform.position.x < Math.Min(startPos.x, endPos.x))
            right = true;
        if (right)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            if (onboard)
            {
                player.xMovement = speed;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            if (onboard) {
                player.xMovement = -speed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && Math.Abs(player.transform.position.y - transform.position.y) <= 1 && player.transform.position.y > transform.position.y)
        {
            onboard = true;

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")) {
            onboard = false;
            player.xMovement = 0;
        }
    }
}
