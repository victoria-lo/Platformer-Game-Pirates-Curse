using UnityEngine;
using UnityEngine.UI;

public class BunnyBoss : MonoBehaviour
{
    public GameObject dialogueManager;
    public float health;
    private float timeBtwDamage = 2f;

    public Slider healthBar;
    private Animator anim;
    private GameObject player;
    public bool isDead;
    public GameObject canvas;

    
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("death");
            isDead = true;
            canvas.SetActive(false);
            dialogueManager.SetActive(true);
            SaveLoad.Save("Level5-1");
            player.GetComponent<Animator>().SetFloat("velocityX", 0f);
            player.GetComponent<PlayerPlatformerController>().enabled = false;
        }

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                other.GetComponent<Health>().health -= 1;
                timeBtwDamage = 2;
            }
        }

        if (other.tag == "Hat")
        {
            health -= 0.05f;
            Vector3 startPos = transform.position;
            Vector3 endPos;
            if (other.transform.parent.position.x < transform.position.x) //Player attacks from left. push enemy right
            {
                endPos = new Vector3(transform.position.x + 30f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, endPos, 1 * Time.deltaTime);
            }
            if (other.transform.parent.position.x > transform.position.x) //Player attacks from right. push enemy left
            {
                endPos = new Vector3(transform.position.x - 30f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, endPos, 1 * Time.deltaTime);
            }
            transform.position = Vector3.Lerp(transform.position, startPos, 1 * Time.deltaTime);
        }
    }
}