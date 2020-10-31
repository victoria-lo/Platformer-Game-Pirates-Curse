using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector3 lastCheckPointPos;
#pragma warning disable 649
    [SerializeField] GameObject introDialogue;
    [SerializeField] RawImage textBox;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject HpBar;
    [SerializeField] GameObject bunny;
    [SerializeField] Animator anim;
#pragma warning restore 649
    void Awake() //prevents multiple GameMaster objects
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        if (Level5Intro.initial)
        {
            introDialogue = FindObjectOfType<Level5Intro>().gameObject;
            Destroy(introDialogue);
            gameUI.SetActive(true);
            textBox.gameObject.SetActive(false);
            bunny.SetActive(false);
        }
        if (CutScene6.initial)
        {
            introDialogue = FindObjectOfType<CutScene6>().gameObject;
            Destroy(introDialogue);
            gameUI.SetActive(true);
            textBox.gameObject.SetActive(false);
        }
        if (Level6Intro.initial)
        {
            introDialogue = FindObjectOfType<Level6Intro>().gameObject;
            anim.SetTrigger("dive");
            Destroy(introDialogue);
            gameUI.SetActive(true);
            joystick.SetActive(true);
            HpBar.SetActive(true);
            textBox.gameObject.SetActive(false);
            bunny.SetActive(false);
        }

    }
}
