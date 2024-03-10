using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayStart : MonoBehaviour
{
    // Точка, испускающая лазерный луч в сторону Z оси.
    // Наклеить её на стену или прилепить на коробку.

    public float defaultLenght = 50;
    public RaycastHit hit;

    private LineRenderer _lineRenderer;
    private LazerSelect _oldSelect;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        LazerBeam();
    }

    private void LazerBeam()
    {
        _lineRenderer.SetPosition(0, transform.position);

        Ray ray = new Ray(transform.position, transform.forward);

        // Много проверок, которые совершаются каждый кадр.
        // Мне стыдно, позже подправлю.

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
            _lineRenderer.SetPosition(1, transform.forward * defaultLenght);
        }
        if (_oldSelect)
        {
            _oldSelect.Deslect(gameObject);
        }
    }

}
