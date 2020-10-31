using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [HideInInspector]
    public Transform player;
    public Animator transitionAnim;
    public Animator playerAnim;
    public string sceneName;
    public float posX,posY;
    public Joybutton upButton;
    GameObject gm;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAnim = player.GetComponent<Animator>();
        transitionAnim = GameObject.Find("Transition").GetComponent<Animator>();
        upButton = GameObject.Find("Up").GetComponent<Joybutton>();
    }

    void OnTriggerStay2D (Collider2D col)
    {
        if (col.CompareTag("Player") && (Input.GetKeyDown(KeyCode.UpArrow) || upButton.up))
        {
            if (sceneName != "")
                SaveLoad.Save(sceneName);
            upButton.up = false;
            StartCoroutine(loadScene());
        }
        
    }

    IEnumerator loadScene()
    {
        playerAnim.SetTrigger("enter");
        yield return new WaitForSeconds(0.14f);
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.1f);

        if (sceneName != "")
        {
            if (GameObject.Find("GameMaster") != null)
            {
                gm = GameObject.Find("GameMaster");
                Destroy(gm);
            }
            SceneManager.LoadScene(sceneName);
            GameObject.Find("AutosaveText").GetComponent<TMPro.TextMeshProUGUI>().alpha = 0;
        }
        else
        {
            transitionAnim.SetTrigger("transIdle");
            player.position = new Vector3(posX, posY, player.position.z);
        }
    }
}
