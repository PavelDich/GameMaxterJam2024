using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObject : LazerSelect
{
    public float defaultLenght = 50;

    private LineRenderer _lineRenderer;
    private RaycastHit hit;

    private LazerSelect _oldSelect;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Select(GameObject gm)
    {
        Vector3 normed = (transform.position - gm.transform.position).normalized;
        normed = Vector3.Reflect(normed, transform.position);
        ReflectLazer(normed);
    }

    public override void Deslect(GameObject gm)
    {
    }

    void ReflectLazer(Vector3 forw)
    {
        _lineRenderer.SetPosition(0, transform.position);

        Ray ray = new Ray(transform.position, forw);

        Debug.DrawRay(transform.position, forw * defaultLenght, Color.red);

        RaycastHit hit;

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
