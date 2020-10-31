using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Passcode : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] string[] codes; //For passcode buttons
    [SerializeField] TextMeshProUGUI mySlot; //For passcode buttons
    [SerializeField] TextMeshProUGUI[] slots; //For Enter Button
    [SerializeField] GameObject currWindow; //For Enter Button
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] GameObject door;
    [SerializeField] GameObject examineButton;
    [SerializeField] GameObject thoughtBubble;
    public AudioSource source;
    public AudioClip Correct;

    #pragma warning restore 649

    private int currIndex;
    public static bool safeOpen = false;
    void Start()
    {
        mySlot = GetComponent<TextMeshProUGUI>();
        currIndex = 0;
    }

    public void Press()
    {
        if (currIndex < codes.Length-1)
        {
            currIndex += 1;
        }
        else
        {
            currIndex = 0;
        }
        mySlot.text = codes[currIndex];
    }

    public void Enter()
    {
        if (currWindow.name == "DoorLockWindow")
        {
            if (slots[0].text == "0" && slots[1].text == "8" && slots[2].text == "1" && slots[3].text == "2")
            {
                textBox.text = "CLICK! The door opened.";
                source.PlayOneShot(Correct);
                door.SetActive(false);
                examineButton.SetActive(false);
                thoughtBubble.SetActive(false);
            }
            else
            {
                textBox.text = "CAPTAIN TORTY: Looks like the wrong 4-digit code.";
            }
        }
        if (currWindow.name == "SafeWindow")
        {
            if (slots[0].text == "B" && slots[1].text == "U" && slots[2].text == "N" && slots[3].text == "N" && slots[4].text == "Y")
            {
                textBox.text = "CLICK! The safe opens!";
                source.PlayOneShot(Correct);
                safeOpen = true;
                if (Passcode.safeOpen)
                {
                    examineButton.SetActive(false);
                    thoughtBubble.SetActive(false);
                    examineButton.GetComponent<Button>().interactable = false;
                    examineButton.GetComponent<TextMeshProUGUI>().text = "SCISSORS FOUND";
                    examineButton.GetComponent<TextMeshProUGUI>().fontSize = 3;
                    examineButton.SetActive(true);
                    thoughtBubble.SetActive(true);
                }
            }
            else
            {
                textBox.text = "CAPTAIN TORTY: Looks like the wrong 5-letter code.";
            }
        }
    }
}
