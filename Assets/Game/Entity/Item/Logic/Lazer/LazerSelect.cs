using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class LazerSelect : GetterObject
    {
        // ������ - ��������� �� �����.
        // ��� �� ������������ ����� ��� ������.

        [SerializeField, HideInInspector]
        private Renderer _rend;

        public void OnValidate()
        {
            _rend = GetComponent<Renderer>();
        }
        protected void Start()
        {
            _rend.material.color = new Color(20, 20, 20);
        }
        public virtual void Select(GameObject gm)
        {
            if (!_enabled && IsEnabled) IsActiveted = true;
        }

        public virtual void Deslect(GameObject gm)
        {
            if (_enabled) IsActiveted = false;
        }
    }
}