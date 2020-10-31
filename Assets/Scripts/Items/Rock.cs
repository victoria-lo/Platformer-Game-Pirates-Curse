using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject rockParticles;
    public GameObject boxCollider;
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag =="Hat")
        {
            rockParticles.SetActive(true);
            rockParticles.transform.parent = transform.parent;
            Destroy(gameObject);
            Destroy(boxCollider);
            Destroy(rockParticles, 1.0f);
        }
    }
}
