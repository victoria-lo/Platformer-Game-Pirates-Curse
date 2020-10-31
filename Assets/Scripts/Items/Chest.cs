using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Sprite open;
    private bool collided = false;
    public static bool happy = false;
    public GameObject content;
    public Joybutton upButton;
#pragma warning disable 649
    [SerializeField] GameObject powerup;
#pragma warning restore 649

    float timer;
    bool hasPower = false;

    void Start()
    {
        upButton = GameObject.Find("Up").GetComponent<Joybutton>();
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Player" && GoldCount.key > 0)
        {
            collided = true;
            happy = true;
        }
    }
    void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Player")
        {
            collided = false;
            happy = false;
        }
    }
    void Update()
    {
        if (collided)
        {
            if(this.GetComponent<SpriteRenderer>().sprite != open)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || upButton.up)
                {
                    this.GetComponent<SpriteRenderer>().sprite = open;
                    if (content.gameObject != null)
                    {
                        content.gameObject.SetActive(true);
                        GoldCount.key -= 1;
                        content.GetComponent<TranslateItem>().move = true;

                        if (content.gameObject.name == "Money")
                        {
                            GoldCount.goldCount += 25;
                        }
                        else
                        {
                            Sprite sprite = content.GetComponent<SpriteRenderer>().sprite;
                            if (powerup != null)
                            {
                                powerup.GetComponent<Image>().sprite = sprite;
                                powerup.SetActive(true);
                            }

                        }
                        if (powerup != null)
                        {
                            hasPower = true;
                            timer = 20;
                            if (content.gameObject.name == "Immunity")
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().immunity = true;
                            }
                            else if (content.gameObject.name == "AttackUp")
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().attackPower = 2;
                            }
                            else if (content.gameObject.name == "Ninja")
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.75f);
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().invisible = true;
                            }
                            else if (content.gameObject.name == "HiJump")
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 18;
                            }
                        }
                    }
                }
            }
        }
        if(hasPower && timer <= 0)
        {
            powerup.SetActive(false);
            timer = 0;
            hasPower = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().immunity = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().attackPower = 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 12;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().invisible = false;
        }
        else
        {
            timer -= Time.deltaTime;

        }
    }
}
