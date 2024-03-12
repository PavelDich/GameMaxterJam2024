using System.Collections;
using System.Collections.Generic;
using GCinc.GameMaxterJam2024.PavelDich;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Box,
    Pismo
}

public class Item : Logic
{
    public Item() => IsActiveted = true;

    public void Interaction()
    {
        IsActiveted = !IsActiveted;
        GetComponentInParent<Animator>().SetBool("Open", IsActiveted);
        GetComponentInParent<Animator>().SetBool("Close", !IsActiveted);
    }
}