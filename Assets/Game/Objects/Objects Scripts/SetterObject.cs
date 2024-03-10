using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterObject : InteractionBase
{
    // Сеттеры - объекты, которые реагируют на изменение индекса своей группы.
    [SerializeField] public int _index = 0;

    internal bool _enabled = false;

    internal void Start()
    {
        _setters.Add(gameObject);
    }

    // Реакция на изменение индекса группы.
    internal virtual void ChangeState(bool newstate)
    {
        _enabled = newstate;
    }
}
