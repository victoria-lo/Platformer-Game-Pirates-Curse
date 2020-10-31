using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutScene3 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public Animator anim;
    public GameObject Background;
    public Texture nextBackground;
    private AudioManager bgm;

    void Start()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            bgm = FindObjectOfType<AudioManager>();
            bgm.changeMusic();
        }
        StartCoroutine(Type());
        anim.SetTrigger("moveRight");
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
            if (index == 1)
            {
                anim.SetBool("Shake",true);
            }
            if (index == 4)
            {
                anim.SetBool("Shake", false);
            }
            if (index==5)
            {
                anim.SetTrigger("FadeIn");
                Background.GetComponent<RawImage>().texture = nextBackground;
            }
            if (index == 8)
            {
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
        yield return new WaitForSeconds(6.0f);
        anim.SetTrigger("FadeOut");
        textBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene("Level3");
        SaveLoad.Save("Level3");
        TranslateItem.itemsNeeded = 0;
        bgm.GetComponent<AudioSource>().clip = bgm.Level3;
        bgm.GetComponent<AudioSource>().Play();
    }

    public void Skip()
    {
        StartCoroutine(SkipScene());
    }
    IEnumerator SkipScene()
    {
        anim.SetTrigger("FadeOut");
        textBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene("Level3");
        SaveLoad.Save("Level3");
        TranslateItem.itemsNeeded = 0;
        bgm.GetComponent<AudioSource>().clip = bgm.Level3;
        bgm.GetComponent<AudioSource>().Play();
    }
}

