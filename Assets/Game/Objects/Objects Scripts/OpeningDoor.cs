using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningDoor : SetterObject
{
    [SerializeField] Animator _animator;

    internal override void ChangeState(bool newstate)
    {
       _animator.SetBool("Open", newstate);
    }
}