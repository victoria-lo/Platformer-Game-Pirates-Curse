using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public GameObject goldParticles;
#pragma warning disable 649
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sfx;

    void Start()
    {
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Player" && this.tag == "Gold")
        {
            GoldCount.goldCount += 1;
            goldParticles.SetActive(true);
            source.PlayOneShot(sfx);
            goldParticles.transform.parent = transform.parent;
            Destroy(gameObject);
            Destroy(goldParticles, 1.0f);
        }

        if (triggerCollider.tag == "Player" && this.tag == "Key")
        {
            GoldCount.key += 1;
            source.PlayOneShot(sfx);
            Destroy(gameObject);
        }
        if (triggerCollider.tag == "Player" && this.tag == "Heart")
        {
            goldParticles.SetActive(true);
            goldParticles.transform.parent = transform.parent;
            source.PlayOneShot(sfx);
            Destroy(gameObject);
            Destroy(goldParticles, 1.0f);
        }
    }
}
