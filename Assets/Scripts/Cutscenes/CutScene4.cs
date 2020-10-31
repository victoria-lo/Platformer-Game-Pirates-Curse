using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutScene4 : MonoBehaviour
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
    public GameObject Torty;
    public GameObject Bunny;
    private AudioManager bgm;
    void Start()
    {
        if(FindObjectOfType<AudioManager>() != null)
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
            if (index == 0)
            {
                anim.SetTrigger("moveLeft");
            }
            if (index == 1)
            {
                anim.SetTrigger("moveRight");
            }
            if (index == 9)
            {
                Torty.SetActive(false);
                Bunny.SetActive(false);
                Background.GetComponent<RawImage>().texture = nextBackground;
            }
            if (index == 13)
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
        yield return new WaitForSeconds(5.5f);
        anim.SetTrigger("FadeOut");
        textBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene("Level4");
        SaveLoad.Save("Level4");
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
        SceneManager.LoadScene("Level4");
        SaveLoad.Save("Level4");
    }
}

