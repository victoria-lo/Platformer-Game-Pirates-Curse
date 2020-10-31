using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level5Intro : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    private GameObject player;
    public float typingSpeed;
    public static bool initial = false;
    public GameObject joystick;
    public GameObject continueButton;
    public GameObject gameUI;
    public GameObject bunny;
    void Start()
    {
        gameUI.SetActive(false);
        joystick.SetActive(false);
        textBox.gameObject.SetActive(true);
        StartCoroutine(Type());
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Animator>().SetFloat("velocityX", 0f);
        player.GetComponent<Animator>().SetBool("grounded", true);
        player.GetComponent<PlayerPlatformerController>().enabled = false;
        initial = true;
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
            if(index == 4)
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
        yield return new WaitForSeconds(3.5f);
        Destroy(continueButton);
        gameUI.SetActive(true);
        joystick.SetActive(true);
        textBox.gameObject.SetActive(false);
        bunny.SetActive(false);
        player.GetComponent<PlayerPlatformerController>().enabled = true;
        this.gameObject.SetActive(false);
    }

}

