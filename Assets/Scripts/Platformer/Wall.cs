using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Wall : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    Transform playerPos;
#pragma warning disable 649
    [SerializeField] Transform m_Follow;
    [SerializeField] float screenX;
    bool hit = false;
    public GameObject plantBoss;
#pragma warning restore 649
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnTriggerExit2D (Collider2D col)
    {
        if(playerPos.position.x > transform.position.x && !hit)
        {
            hit = true;
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            playerPos.GetComponent<Animator>().SetFloat("velocityX", 0f);
            playerPos.gameObject.GetComponent<PlayerPlatformerController>().enabled = false;
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = screenX;
            StartCoroutine(ChangeTarget());
        }
    }

    IEnumerator ChangeTarget()
    {
        plantBoss.SetActive(true);
        yield return new WaitForSeconds(2);
        vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.364f;
        vcam.GetComponent<CinemachineVirtualCamera>().Follow = m_Follow;
        playerPos.gameObject.GetComponent<PlayerPlatformerController>().enabled = true;
        StopCoroutine(ChangeTarget());
    }
}
