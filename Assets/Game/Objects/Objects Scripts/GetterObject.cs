using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterObject : InteractionBase
{
    [SerializeField] public int _index = 0;
    
    internal bool _enabled = false;

    internal void Start()
    {
        _getters.Add(gameObject);
    }

    public void ChangeState(bool newstate) 
    {
        _enabled = newstate;

        UpdateState(_index, newstate);
    }
}
