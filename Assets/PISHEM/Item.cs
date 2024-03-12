using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Box,
    Pismo
}

public class Item : MonoBehaviour
{
    public ItemType type;
    bool flag = true;
    public GameObject panel;

    public void Interaction()
    {
        if (type == ItemType.Box)
        {
            flag = !flag;
            GetComponentInParent<Animator>().SetBool("Open", flag);
            GetComponentInParent<Animator>().SetBool("Close", !flag);

        }
        if (type == ItemType.Pismo)
        {
            panel.SetActive(true);

        }
    }
    public void ClosePismo()
    {
        panel.SetActive(false);
    }
    
}