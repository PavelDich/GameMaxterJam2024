using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class SetterObject : InteractionBase
    {
        // ������� - �������, ������� ��������� �� ��������� ������� ����� ������.
        [SerializeField] public int _index = 0;

        internal bool _enabled = false;

        internal void Start()
        {
            _setters.Add(gameObject);
        }

        // ������� �� ��������� ������� ������.
        internal virtual void ChangeState(bool newstate)
        {
            _enabled = newstate;
        }
    }
}