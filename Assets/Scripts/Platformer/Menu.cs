using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public Animator anim;
    public GameObject fadeScreen;
    public GameObject optionMenu;
    AudioManager audioManager;
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    void Start()
    {
        fadeScreen.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void newGame()
    {
        fadeScreen.SetActive(true);
        anim.SetTrigger("end");
        Invoke("loadScene", 2.0f);
    }

    void loadScene()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void options()
    {
        optionMenu.SetActive(true);
    }
    public void back()
    {
        optionMenu.SetActive(false);
    }

    public void quit()
    {
        quit();
    }
    public void title()
    {
        fadeScreen.SetActive(true);
        anim.SetTrigger("end");
        Invoke("loadTitle", 1.2f);
    }
    void loadTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void load()
    {
        audioManager.changeMusic();
        SaveLoad.Load();  
    }
}
