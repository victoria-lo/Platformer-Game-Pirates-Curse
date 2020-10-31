using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScene6 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    private GameObject player;
    public float typingSpeed;
    public static bool initial = false;

    private Animator anim;

    public GameObject continueButton;
    public GameObject gameUI;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D (Collider2D col)
    {
        if (!initial)
        {
            gameUI.SetActive(false);
            textBox.gameObject.SetActive(true);
            StartCoroutine(Type());
            
            player.GetComponent<Animator>().SetFloat("velocityX", 0f);
            player.GetComponent<Animator>().SetBool("grounded", true);
            player.GetComponent<PlayerPlatformerController>().enabled = false;
            initial = true;
        } 
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
            if(index == 2)
            {
                anim.SetTrigger("Wake");
            }
            if (index == 11)
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
        yield return new WaitForSeconds(2);
        gameUI.SetActive(true);
        textBox.gameObject.SetActive(false);
        player.GetComponent<PlayerPlatformerController>().enabled = true;
        //this.gameObject.SetActive(false);
    }

}

