using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;
    public Sprite sprite;
    private Checkpoint instance;
    public static bool checkpoint = false;
#pragma warning disable 649
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sfx;
    void Start()
    {
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!checkpoint)
            {
                source.PlayOneShot(sfx);
                checkpoint = true;
            }
            gm.lastCheckPointPos = new Vector3(transform.position.x, transform.position.y, -5);
            GetComponent<SpriteRenderer>().sprite = sprite;
            instance = this;
        }
    }
}
