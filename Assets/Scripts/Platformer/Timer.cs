using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int Minutes = 5;
    public int Seconds = 0;

    private TextMeshProUGUI m_text;
    private float m_leftTime;

    public static bool wireCut = false;
    public static bool timerStart = false;
    bool resetLevel = true;

    public GameObject timerBox;
    public GameObject dialogueBox;
    public Animator transitionAnim;
    public GameObject joystick;
    [SerializeField] Transform player;

    private void Awake()
    {
        wireCut = false;
        timerStart = false;
        resetLevel = true;
        m_text = GetComponent<TextMeshProUGUI>();
        m_leftTime = GetInitialTime();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (wireCut)//Wire is cut
        {
            //  Update countdown clock
            if (timerStart)
            {
                StartCoroutine(TimerStart());
                timerStart = false;
            }
            
            m_leftTime -= Time.deltaTime;
            Minutes = GetLeftMinutes();
            Seconds = GetLeftSeconds();

            //  Show current clock
            if (m_leftTime > 0f)
            {
                m_text.text = "0" + Minutes + ":" + Seconds.ToString("00");
            }
            else
            {
                //  The countdown clock has finished
                m_text.text = "00:00";
                if (resetLevel)
                {
                    StartCoroutine(ResetLevel());
                    resetLevel = false;
                }
            }
        }
        if (TranslateItem.itemsNeeded == 4)
        {
            StartCoroutine(loadScene());
            TranslateItem.itemsNeeded = 0;
        }
    }

    private float GetInitialTime()
    {
        return Minutes * 60f + Seconds;
    }

    private int GetLeftMinutes()
    {
        return Mathf.FloorToInt(m_leftTime / 60f);
    }

    private int GetLeftSeconds()
    {
        return Mathf.FloorToInt(m_leftTime % 60f);
    }

    IEnumerator ResetLevel() //Restarts Level3
    {
        TranslateItem.itemsNeeded = 0;
        player.GetComponent<PlayerPlatformerController>().enabled = false;
        timerBox.GetComponentInChildren<TextMeshProUGUI>().text = "SECURITY COUNTDOWN OVER. \nALL SECURITY SYSTEMS RESET.";
        timerBox.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Level3");
        transitionAnim.SetTrigger("transIdle");
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerPlatformerController>().enabled = true;
        timerBox.SetActive(false);
        StopCoroutine(ResetLevel());
    }

    IEnumerator TimerStart()
    {
        timerBox.GetComponentInChildren<TextMeshProUGUI>().text = "SECURITY COUNTDOWN START. \nALL SECURITY SYSTEMS RESET WHEN TIME OUT.";
        timerBox.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        timerBox.SetActive(false);
        StopCoroutine(TimerStart());
    }
    IEnumerator loadScene()
    {
        dialogueBox.SetActive(true);
        joystick.SetActive(false);
        yield return new WaitForSeconds(5f);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Cutscene4"); //Load to cutscene
        SaveLoad.Save("CutScene4");
    }
}
