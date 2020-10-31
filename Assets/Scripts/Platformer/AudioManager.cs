using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] AudioClip Prologue;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioClip Level5;
    public AudioClip Level6End;
    [SerializeField] AudioClip Level6Intro;
    [SerializeField] AudioClip CutScene1;
    [SerializeField] AudioClip CutScene2;
    [SerializeField] AudioClip CutScene3;
    [SerializeField] AudioClip CutScene4;
    [SerializeField] AudioSource source;
    public static AudioManager instance;
#pragma warning restore 649
    void Awake()
    {
        if (AudioManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            AudioManager.instance = this;
        }
        else
            Destroy(gameObject);
        source = GetComponent<AudioSource>();

    }
    void OnEnable()
    {   //Get the saved music volume, standard = 10f
        float music = PlayerPrefs.GetFloat("volume", 0f);
        //Set the music volume to the saved volume
        SetVolume(music);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        changeMusic();
    }

    public void changeMusic()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Prologue")
        {
            source.clip = Prologue;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level1-1")
        {
            source.clip = Prologue;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level1-2")
        {
            source.clip = Prologue;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Cutscene1")
        {
            source.clip = CutScene1;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Cutscene2")
        {
            source.clip = CutScene2;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level2-1")
        {
            source.clip = Level2;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level2-2")
        {
            source.clip = Level2;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Cutscene3")
        {
            source.clip = CutScene3;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            source.clip = Level3;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Cutscene4")
        {
            source.clip = CutScene4;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level4")
        {
            source.clip = CutScene4;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level5-1")
        {
            source.clip = Level5;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level5-2")
        {
            source.clip = Level5;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level5-3")
        {
            source.clip = Level5;
            source.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level6")
        {
            source.clip = Level6Intro;
            source.Play();
        }
    }

    public void SetVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("volume", volume);
        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("volume", volume);
        //Save changes
        PlayerPrefs.Save();
    }

}
