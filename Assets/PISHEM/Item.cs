using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Box,
    Test
}

public class Item : MonoBehaviour
{
    public ItemType type;
    bool flag = false;

    public void Interaction()
    {
        if (type == ItemType.Box)
        {
            flag = !flag;
            GetComponentInParent<Animator>().SetBool("Open", flag);
            GetComponentInParent<Animator>().SetBool("Close", !flag);

        }
        if (type == ItemType.Test)
        {
            Destroy(gameObject);
        }
    }
       
    
}