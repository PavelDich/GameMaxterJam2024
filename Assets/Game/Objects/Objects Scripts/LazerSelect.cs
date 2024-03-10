using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSelect : GetterObject
{
    // Геттер - реагирует на лазер.
    // Так же родительский класс для зеркал.

    Renderer _rend;

    public void Start()
    {
        _rend = GetComponent<Renderer>();
        _rend.material.color = new Color(20, 20, 20);
    }
    public virtual void Select(GameObject gm)
    {
        if (!_enabled) 
        {
            ChangeState(true);
            _rend.material.color = new Color(20, 20, 20);
        }
    }

    public virtual void Deslect(GameObject gm) 
    {
        if (_enabled)
        {
            ChangeState(false);
            _rend.material.color = new Color(200, 20, 200);
        }
    }
}
