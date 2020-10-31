using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level5End : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    private GameObject player;
    public float typingSpeed;
    public Animator anim;
    public GameObject continueButton;
    public GameObject gameUI;
    public GameObject bunny;
    void Start()
    {
        gameUI.SetActive(false);
        textBox.gameObject.SetActive(true);
        textDisplay.text = "";
        StartCoroutine(Wait());
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Animator>().SetFloat("velocityX", 0f);
        player.GetComponent<Animator>().SetBool("grounded", true);
        player.GetComponent<PlayerPlatformerController>().enabled = false;
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        bunny.SetActive(true);
        StartCoroutine(Type());
        StopCoroutine(Wait());
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
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        if (GameObject.Find("GameMaster") != null)
        {
            GameObject gm;
            gm = GameObject.Find("GameMaster");
            Destroy(gm);
        }
        SceneManager.LoadScene("Level5-2");
    }

}

