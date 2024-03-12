using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class MirrorReflection : LazerSelect
    {
        // ������, ���������� �������� ���, �������� ����.
        // ���������� - ����� ������������ ����� ���� ������������ ������ ��������� ������ 2 ��������.

        public float defaultLenght = 50;

        public RaycastHit hit;

        private GameObject _originOfLazer;
        private LineRenderer _lineRenderer;
        private LazerSelect _oldSelect;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public override void Select(GameObject gm)
        {
            _originOfLazer = gm;
            _lineRenderer.enabled = true;
            ReflectLazer(_originOfLazer);
        }

        public override void Deslect(GameObject gm)
        {
            if ((gm.GetComponent<LazerSelect>() != _oldSelect || !gm.GetComponent<LazerSelect>()) && _oldSelect)
            {
                _oldSelect.Deslect(gameObject);
            }
            _lineRenderer.enabled = false;
            _oldSelect = null;
        }

        void ReflectLazer(GameObject gm)
        {
            // ��������� ������� + ���������� ���.
            // ���� ����������.

            Vector3 startPos;
            if (gm.GetComponent<RayStart>())
            {
                startPos = gm.GetComponent<RayStart>().hit.point;
            }
            else
            {
                startPos = gm.GetComponent<MirrorReflection>().hit.point;
            }

            Vector3 forw = (transform.position - gm.transform.position).normalized;
            forw = Vector3.Reflect(forw, Quaternion.Euler(0, 90, 0) * transform.forward);
            //forw.y = (transform.position - gm.transform.position).y;

            _lineRenderer.SetPosition(0, startPos);

            Ray ray = new Ray(startPos, forw);

            Debug.DrawRay(startPos, forw * defaultLenght, Color.red);

            // ����� ����� ��������.
            // ����������� ������ ����, ����� ������� �������.
            // ���� ��������.

            if (Physics.Raycast(ray, out hit))
            {
                _lineRenderer.SetPosition(1, hit.point);

                LazerSelect selectable = hit.collider.gameObject.GetComponent<LazerSelect>();
                if (selectable)
                {
                    if (gm != gameObject && (gm.GetComponent<LazerSelect>() != _oldSelect || !gm.GetComponent<LazerSelect>()))
                    {
                        selectable.Select(gameObject);
                    }
                    if (selectable == _oldSelect)
                    {
                        return;
                    }
                    _oldSelect = selectable;
                    return;
                }
            }
            if ((gm.GetComponent<LazerSelect>() != _oldSelect || !gm.GetComponent<LazerSelect>()) && _oldSelect)
            {
                _oldSelect.Deslect(gameObject);
            }
            if (_lineRenderer.enabled)
            {
                _lineRenderer.SetPosition(1, (ray.direction * defaultLenght) + startPos);
            }
        }
    }
}