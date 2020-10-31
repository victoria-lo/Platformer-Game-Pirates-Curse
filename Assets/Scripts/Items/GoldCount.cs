using UnityEngine;
using TMPro;
public class GoldCount : MonoBehaviour
{
    TextMeshProUGUI text;
    public static int goldCount;
    public static int key;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        text.text = goldCount.ToString();
    }
}
