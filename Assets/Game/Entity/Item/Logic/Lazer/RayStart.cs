using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class RayStart : Logic
    {
        public float defaultLenght = 50;
        public RaycastHit hit;

        [SerializeField, HideInInspector]
        private LineRenderer _lineRenderer;
        private LazerSelect _oldSelect;

        protected override void OnValidate()
        {
            base.OnValidate();
            _lineRenderer = GetComponent<LineRenderer>();
        }
        void Update()
        {
            if (IsEnabled) 
            {
                _lineRenderer.enabled = true;
                LazerBeam();
                return;
            }
            else if (_oldSelect) 
            {
                _oldSelect.Deslect(gameObject);
                _oldSelect = null;
            }
            _lineRenderer.enabled = false;
        }

        private void LazerBeam()
        {
            _lineRenderer.SetPosition(0, transform.position);

            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                _lineRenderer.SetPosition(1, hit.point);

                LazerSelect selectable = hit.collider.gameObject.GetComponent<LazerSelect>();
                if (selectable)
                {
                    selectable.Select(gameObject);
                    if (selectable == _oldSelect)
                    {
                        return;
                    }
                    _oldSelect = selectable;
                    return;
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, transform.forward * defaultLenght + transform.position);
            }
            if (_oldSelect)
            {
                _oldSelect.Deslect(gameObject);
            }
        }

    }
}