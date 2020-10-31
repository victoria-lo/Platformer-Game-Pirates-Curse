using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Level6End : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public RawImage textBox;
    public string[] sentences;
    private int index;
    private GameObject player;
    Vector3 playerPos;
    public float typingSpeed;
    public Animator anim;
    public Animator transitionAnim;
    public Animator transition2Anim;
    public GameObject continueButton;
    public GameObject gameUI;

    public GameObject bunny;
    public GameObject minion1;
    public GameObject minion2;
    public GameObject joystick;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector3(0.77f, -3.4f, -5);
        player.transform.position = playerPos;
        gameUI.SetActive(false);
        joystick.SetActive(false);

        textDisplay.text = "";
        StartCoroutine(Wait());
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Animator>().SetFloat("velocityX", 0f);
        player.GetComponent<Animator>().SetBool("grounded", true);
        player.GetComponent<PlayerPlatformerController>().enabled = false;

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
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
            if(index == 12)
            {
                bunny.SetActive(true);
                minion1.SetActive(true);
                minion2.SetActive(true);
            }
            if (index == 19)
            {
                transitionAnim.SetTrigger("fade");
            }
            if (index == 21)
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
        yield return new WaitForSeconds(4.9f);
        transition2Anim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Credits");
    }

}

