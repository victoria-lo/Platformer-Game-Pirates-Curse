using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public Animator anim;
    public GameObject Background;
    public GameObject Torty;
    public Texture prologue;
    private AudioManager bgm;
    
    void Start()
    {
        if (bgm != null)
        {
            bgm = FindObjectOfType<AudioManager>();
            bgm.changeMusic();
        }
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
        foreach(char letter in sentences[index].ToCharArray())
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
            if(index == 1)
            {
                anim.SetTrigger("Prologue2");
            }
            if(index == 2)
            {
                Torty.SetActive(false);
                Background.GetComponent<RawImage>().texture = prologue;
                StartCoroutine(loadScene());
            }
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1.8f);
        anim.SetTrigger("FadeOut");
        textBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level 1-1");
    }
    public void Skip()
    {
        StartCoroutine(SkipScene());
    }
    IEnumerator SkipScene()
    {
        anim.SetTrigger("FadeOut");
        textBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level 1-1");
    }
}
