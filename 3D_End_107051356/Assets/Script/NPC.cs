using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialog;
    [Header("對話內容")]
    public Text textContent;

    public bool playerInArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "機器人")
        {
            playerInArea = true;
            Dialog();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "機器人")
        {
            playerInArea = false;
        }
    }

    private void Dialog()
    {
        for (int i = 0; i < data.dialougA.Length; i++)
        {
            print(data.dialougA[i]);
        }
    }
}
