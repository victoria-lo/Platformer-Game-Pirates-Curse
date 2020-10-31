using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool jump, attack;
    [HideInInspector]
    public bool used;
    [HideInInspector]
    public bool up, down;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.gameObject.name == "Jump")
        {
            jump = true;
            used = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.name == "Attack")
        {
            attack = true;
        }
        else if (this.gameObject.name == "Up")
        {
            up = true;
        }
        else if (this.gameObject.name == "Down")
        {
            down = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.gameObject.name == "Jump")
        {
            jump = false;
        }
    }

}
