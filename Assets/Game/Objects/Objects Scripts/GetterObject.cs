using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterObject : InteractionBase
{
    // √еттеры - объекты, с которыми может взаимодействовать игрок, геттеры мен€ют индекс своей группы.

    [SerializeField] public int _index = 0;
    
    internal bool _enabled = false;

    internal void Start()
    {
        _getters.Add(gameObject);
    }

    // »зменение состо€ни€ индекса группы.
    public void ChangeState(bool newstate) 
    {
        _enabled = newstate;

        UpdateState(_index, newstate);
    }
}
