using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float fallDelay = 1.0f;
    Vector3 startPos;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
            
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (transform.position == startPos)
        {
            StopAllCoroutines();
        }
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().isTrigger= true;
        yield return new WaitForSeconds(2);
        GetComponent<Collider2D>().isTrigger = false;
        rb2D.bodyType = RigidbodyType2D.Static;
        transform.position = startPos;
        StopAllCoroutines();
    }
}
