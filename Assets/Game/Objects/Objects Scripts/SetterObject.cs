using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterObject : InteractionBase
{
    // Общий класс предметов с которыми игрок взаимодействует
    [SerializeField] public int _index = 0;

    internal bool _enabled = false;

    internal void Start()
    {
        _setters.Add(gameObject);
    }

    internal virtual void ChangeState(bool newstate)
    {
        _enabled = newstate;
    }
}
