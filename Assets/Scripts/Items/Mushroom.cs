using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(this.tag == "Purple")
        {
            player.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 36;
        }
        if (this.tag == "Red")
        {
            player.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 20;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (this.tag == "Purple")
        {
            player.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 12;
        }
        if (this.tag == "Red")
        {
            player.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 12;
        }
    }
}
