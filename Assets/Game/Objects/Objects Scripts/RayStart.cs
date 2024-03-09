using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayStart : MonoBehaviour
{
    public Transform _pointer;

    public float defaultLenght = 50;

    private LineRenderer _lineRenderer;
    public RaycastHit hit;

    private LazerSelect _oldSelect;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            _lineRenderer.SetPosition(1, hit.point);

            LazerSelect selectable = hit.collider.gameObject.GetComponent<LazerSelect>();
            if (selectable != _oldSelect && _oldSelect)
            {
                _oldSelect.Deslect(gameObject);
            }
            if (selectable)
            {
                selectable.Select(gameObject);
                _oldSelect = selectable;
            }
        }
        else if (_oldSelect)
        {
            _lineRenderer.SetPosition(1, transform.forward * defaultLenght);
            _oldSelect.Deslect(gameObject);
        }


    }
}
