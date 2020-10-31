using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour
{
    public GameObject dialogueEnd;
    public float health;

    public Slider healthBar;

    private GameObject player;
    private Animator tortyAnim;
    private AudioManager bgm;

    public bool isDead;
    public GameObject HpBar;
    Animator anim;
    private void Start()
    {
        bgm = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        tortyAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0 && isDead == false)
        {
            isDead = true;
            HpBar.SetActive(false);
            tortyAnim.SetFloat("velocityX", 0f);
            if(player.transform.localScale.x < 0) //Make player face right
            {
                Vector3 theScale = player.transform.localScale;
                theScale.x *= -1;
                player.transform.localScale = theScale;
            }
            if (bgm != null)
            {
                bgm.GetComponent<AudioSource>().clip = bgm.Level6End;
                bgm.GetComponent<AudioSource>().Play();
            }
            player.GetComponent<PlayerPlatformerController>().enabled = false;
            anim.SetTrigger("die");
            dialogueEnd.SetActive(true);
        }

        healthBar.value = health;

    }
    private void OnTriggerUpdate2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDead == false)
        {

            other.GetComponent<Health>().health -= 1;
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hat")
        {
            health -= 0.05f;
        }
    }
}