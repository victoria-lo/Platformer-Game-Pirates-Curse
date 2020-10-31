using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactables : MonoBehaviour
{
    public GameObject examineButton;
    public GameObject thoughtBubble;
    GameObject joystick;
    #pragma warning disable 649
    [SerializeField] Sprite switchSprite;
    [SerializeField] GameObject currWindow;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject lighter;
    [SerializeField] GameObject pillar;
    [SerializeField] GameObject PseudoWires;
    [SerializeField] GameObject ScrapMetal;
#pragma warning restore 649

    [SerializeField] Transform player;
    Vector3 localScl;

    void Start()
    {
        joystick = GameObject.Find("JoyStick_Canvas");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        localScl = examineButton.transform.localScale;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (this.name != "Wires" || Passcode.safeOpen)
        {
            examineButton.SetActive(true);
            thoughtBubble.SetActive(true);
        }

        if (player.GetComponent<PlayerPlatformerController>().m_FacingRight)
        {
            if (localScl.x <= 0)
            {
                localScl.x *= -1;
                examineButton.transform.localScale = localScl;
            }
        }
        else
        {
            if (localScl.x >= 0)
            {
                localScl.x *= -1;
                examineButton.transform.localScale = localScl;
            }
        }
        
    }

    void OnTriggerStay2D(Collider2D col)//makes sures the words stays the right orientation even when player flips
    {
        if (player.GetComponent<PlayerPlatformerController>().m_FacingRight)
        {
            if (localScl.x <= 0)
            {
                localScl.x *= -1;
                examineButton.transform.localScale = localScl;
            }
        }
        else
        {
            if (localScl.x >= 0)
            {
                localScl.x *= -1;
                examineButton.transform.localScale = localScl;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        examineButton.SetActive(false);
        thoughtBubble.SetActive(false);
    }

    public void Examine() //For DoorLock, Safe, Shelf, BunnyPoster, Checklist
    {
        currWindow.SetActive(true);
        joystick.SetActive(false);
        blackScreen.SetActive(true);
        backButton.SetActive(true);
        player.GetComponent<PlayerPlatformerController>().enabled = false;
        
    }
    public void Back()
    {
        GameObject[] allWindows = GameObject.FindGameObjectsWithTag("Window");
        foreach (GameObject window in allWindows)
        {
            window.SetActive(false);
        }
        blackScreen.SetActive(false);
        backButton.SetActive(false);
        joystick.SetActive(true);
        player.GetComponent<PlayerPlatformerController>().enabled = true;
    }

    public void Pull() //For Switch
    {
        examineButton.SetActive(false);
        thoughtBubble.SetActive(false);
        this.GetComponent<SpriteRenderer>().sprite = switchSprite;
        pillar.SetActive(false);
        examineButton.GetComponent<Button>().interactable = false;
        examineButton.GetComponent<TextMeshProUGUI>().text = "Something Unlocked";
        examineButton.GetComponent<TextMeshProUGUI>().fontSize = 3;
        examineButton.SetActive(true);
        thoughtBubble.SetActive(true);
    }

    public void Drawer() //For Drawer (Lighter)
    {
        if (!lighter.activeSelf)
        {
            examineButton.SetActive(false);
            thoughtBubble.SetActive(false);
            lighter.SetActive(true);
            lighter.GetComponent<TranslateItem>().move = true;
            examineButton.GetComponent<Button>().interactable = false;
            examineButton.GetComponent<TextMeshProUGUI>().text = "LIGHTER FOUND";
            examineButton.GetComponent<TextMeshProUGUI>().fontSize = 3;
            examineButton.SetActive(true);
            thoughtBubble.SetActive(true);
        }
        
    }
    public void Push() //For NorthMagnet
    {
        if (!pillar.activeSelf)
        {
            examineButton.SetActive(false);
            thoughtBubble.SetActive(false);
            this.GetComponent<TranslateItem>().move = true;
            examineButton.GetComponent<Button>().interactable = false;
            examineButton.GetComponent<TextMeshProUGUI>().text = "Nice landing!";
            examineButton.GetComponent<TextMeshProUGUI>().fontSize = 3;
            examineButton.SetActive(true);
            thoughtBubble.SetActive(true);
        }
    }

    public void Cut() //For Wires
    {
        Timer.wireCut = true;
        Timer.timerStart = true;
        examineButton.SetActive(false);
        thoughtBubble.SetActive(false);
        PseudoWires.SetActive(true);
        this.gameObject.SetActive(false);
        TranslateItem.itemsNeeded += 1;
        Debug.Log(TranslateItem.itemsNeeded);
    }

    public void PickUp() //For Scrap Metal
    {
        examineButton.SetActive(false);
        thoughtBubble.SetActive(false);
        ScrapMetal.GetComponent<TranslateItem>().move = true;
        examineButton.GetComponent<Button>().interactable = false;
        examineButton.GetComponent<TextMeshProUGUI>().text = "SCRAP METAL FOUND";
        examineButton.GetComponent<TextMeshProUGUI>().fontSize = 2.5f;
        examineButton.SetActive(true);
        thoughtBubble.SetActive(true);
    }
}
