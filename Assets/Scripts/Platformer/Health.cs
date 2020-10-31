using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public bool immunity = false;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject hat;
    private Animator anim;
    public Animator transitionAnim;

    [SerializeField] AudioSource source;
    public AudioClip sfx;

    Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        anim = GetComponent<Animator>();
        transitionAnim = GameObject.Find("Transition").GetComponent<Animator>();
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }
    void Update()
    {
        if (GoldCount.goldCount == 50)
        {
            GoldCount.goldCount = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().health += 1;
        }
        if (immunity) {
            health = 5;
        }
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (health <= 0 && !immunity)
        {
            StartCoroutine(Die());
        }
    
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!immunity)
        {
            if (collider.tag == "Enemy" && !hat.activeSelf && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().invisible)
            {
                health -= 1;
                source.PlayOneShot(sfx);
            }
            if (collider.tag == "Heart" && !hat.activeSelf)
            {
                health += 1;
            }
            if (collider.tag == "Spikes" && !hat.activeSelf)
            {
                StartCoroutine(Die());
            }
        }
    }
    IEnumerator Die()
    {
        anim.SetTrigger("hurt");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().dead = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerPlatformerController>().enabled = false;
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        health = 3;
        SceneManager.LoadScene(scene.name);
        GetComponent<PlayerPlatformerController>().enabled = true;
    }
}
