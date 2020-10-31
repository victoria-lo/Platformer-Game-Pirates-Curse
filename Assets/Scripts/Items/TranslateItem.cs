using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranslateItem : MonoBehaviour
{
    public float posX, posY;
    public float speed;
    public bool move = false;

    #pragma warning disable 649
    [SerializeField] GameObject Particles;
    #pragma warning restore 649
    public static int itemsNeeded;
    void Update()
    {
        Vector3 targetPos = new Vector3(posX, posY, transform.position.z);
        if (move)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        } 
        if(transform.position == targetPos)
        {
            StartCoroutine(ObtainItem());
        }
        
    }
    IEnumerator ObtainItem()
    {
        yield return new WaitForSeconds(0.5f);
        if (this.tag == "ItemNeeded")
        {
            itemsNeeded += 1;
        }
        Particles.SetActive(true);
        Particles.transform.parent = transform.parent;
        Destroy(gameObject);
        Destroy(Particles, 1.0f);

        StopCoroutine(ObtainItem());
    }

}