using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Heart : MonoBehaviour
{
    public GameObject goldParticles;
    public GameObject heart;
#pragma warning disable 649
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sfx;
#pragma warning restore 649
    void OnTriggerEnter2D(Collider2D triggerCollider)
    {

        if (triggerCollider.tag == "Player" && !goldParticles.activeSelf)
        {
            if (triggerCollider.GetComponent<Health>().health < 5)
            {
                source.PlayOneShot(sfx);
                goldParticles.SetActive(true);
                heart.SetActive(false);
                StartCoroutine(ReSpawn());
            }
        }
    }

    IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(8f);
        goldParticles.SetActive(false);
        heart.SetActive(true);
        StopCoroutine(ReSpawn());
    }
}
