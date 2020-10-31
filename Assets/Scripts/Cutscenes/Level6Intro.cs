using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level6Intro : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    private GameObject player;
    public float typingSpeed;
    public static bool initial = false;
    public Animator anim;
    public GameObject bunny;
    public GameObject continueButton;
    public GameObject gameUI;
    public GameObject HpBar;

    private AudioManager bgm;

    public GameObject joystick;

    void Start()
    {
        if(bgm != null)
        {
            bgm = FindObjectOfType<AudioManager>();
            bgm.changeMusic();
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        gameUI.SetActive(false);
        joystick.SetActive(false);   
        StartCoroutine(Wait());
        player.GetComponent<Animator>().SetFloat("velocityX", 0f);
        player.GetComponent<Animator>().SetBool("grounded", true);
        player.GetComponent<PlayerPlatformerController>().enabled = false;
        initial = true;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.1f);
        textBox.gameObject.SetActive(true);
        StartCoroutine(Type());
        StopCoroutine(Wait());
    }
    void Update()
    {
        if (index <= sentences.Length - 2)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void Next()
    {
        continueButton.SetActive(false);
        if (index <= sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            if (index == 3)
            {
                bunny.SetActive(false);
            }
            if (index == 4)
            {
                anim.SetTrigger("intro");
            }
            if (index == 8)
            {
                StartCoroutine(loadScene());
            }
        }
        else
        {
            textDisplay.text = "";
        }
    }
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(5);
        gameUI.SetActive(true);
        joystick.SetActive(true);
        HpBar.SetActive(true);
        textBox.gameObject.SetActive(false);
        player.GetComponent<PlayerPlatformerController>().enabled = true;
        anim.SetTrigger("dive");
        this.gameObject.SetActive(false);
    }

}

