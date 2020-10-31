using UnityEngine;

public class SpikeVine : MonoBehaviour
{
    [HideInInspector]
    public Animator tortyAnim;
    [HideInInspector]
    public Transform playerPos;
    [HideInInspector]
    public Joybutton upButton;
    [HideInInspector]
    public GameObject debugLog;
#pragma warning disable 649
    [HideInInspector]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sfx;

    public GameObject spikeVine;
    Animator myAnimator;
    bool nearBy;
    void Start()
    {
        nearBy = false;
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        myAnimator = spikeVine.GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tortyAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        debugLog = GameObject.Find("DebugLog");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        nearBy = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        nearBy = false;
    }
    void Update()
    {
        if (nearBy)
        {
            upButton = GameObject.Find("Up").GetComponent<Joybutton>();
            if (Input.GetKeyDown(KeyCode.C) || upButton.up)
            {
                if (GoldCount.goldCount >= 10)
                {
                    GoldCount.goldCount -= 10;
                    source.PlayOneShot(sfx);
                    tortyAnim.SetTrigger("cut");
                    myAnimator.SetTrigger("Chop");
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().dead = true;
                    Invoke("Destroy", 1.5f);
                }
                upButton.up = false;
            }
        }
    }
    void Destroy()
    {
        if(spikeVine != null)
        {
            Destroy(spikeVine);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().dead = false;
    }
}
