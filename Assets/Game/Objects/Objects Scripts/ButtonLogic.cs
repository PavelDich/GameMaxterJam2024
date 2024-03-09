using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : GetterObject
{
    // Скрипт кнопки
    [SerializeField] private BoxCollider _bc;

    private void OnTriggerEnter(Collider other)
    {
        ChangeState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeState(false);
    }
}

