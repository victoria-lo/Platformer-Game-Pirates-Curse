using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBoss : MonoBehaviour
{
    public GameObject dialogueManager;
    public float health;
    float timer;
    bool isJumping = true;

    public Slider healthBar;

    private Animator anim;
    private GameObject player;
    private Animator tortyAnim;

    public bool isDead;
    public GameObject canvas;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        tortyAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        timer = Random.Range(6.5f, 16.5f);
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.applyRootMotion = true;
            anim.SetTrigger("die");
            isDead = true;
            canvas.SetActive(false);
            dialogueManager.SetActive(true);
            SaveLoad.Save("Level5-2");
            tortyAnim.SetFloat("velocityX", 0f);
            player.GetComponent<PlayerPlatformerController>().enabled = false;
        }

        healthBar.value = health;

        if (timer <= 0)
        {
            if (isJumping)
            {
                anim.SetTrigger("attack");
                timer = Random.Range(16.5f, 26.5f);
                isJumping = false;
            }
            if (!isJumping)
            {
                anim.SetTrigger("jump");
                timer = Random.Range(6.5f, 16.5f);
                isJumping = true;
            }

        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hat")
        {
            health -= 0.1f;
        }
        if (other.CompareTag("Player") && isDead == false)
        {
            other.GetComponent<Health>().health -= 1;
        }
    }
}