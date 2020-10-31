using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutScene5 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public Animator anim;
    public GameObject gameUI;
#pragma warning disable 649
    private AudioManager bgm;
    void Start()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            bgm = FindObjectOfType<AudioManager>();
        }
        gameUI.SetActive(false);
        textBox.gameObject.SetActive(true);
        StartCoroutine(Type());
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
    public void Skip()
    {
        StartCoroutine(loadScene());
    }
    public void Next()
    {
        continueButton.SetActive(false);
        if (index <= sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            if (index == 12)
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
        yield return new WaitForSeconds(5.5f);
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Level5-1");
        if(FindObjectOfType<AudioManager>()!= null)
        {
            bgm.GetComponent<AudioSource>().clip = bgm.Level5;
            bgm.GetComponent<AudioSource>().Play();
        }
        
    }

}

