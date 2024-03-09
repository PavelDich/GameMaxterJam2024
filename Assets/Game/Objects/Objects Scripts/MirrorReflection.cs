using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : LazerSelect
{
    public float defaultLenght = 50;

    private LineRenderer _lineRenderer;
    public RaycastHit hit;

    private LazerSelect _oldSelect;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Select(GameObject gm)
    {
        ReflectLazer(gm);
    }

    public override void Deslect(GameObject gm)
    {
        _lineRenderer.enabled= false;
    }

    void ReflectLazer(GameObject gm)
    {
        Vector3 startPos;
        if (gm.GetComponent<RayStart>())
        {
            startPos = gm.GetComponent<RayStart>().hit.point;
        }
        else 
        {
            startPos = gm.GetComponent<MirrorReflection>().hit.point;
        }
        startPos.y = transform.position.y;

        Vector3 forw = (transform.position - gm.transform.position).normalized;
        forw = Vector3.Reflect(forw, transform.position);
        forw.y = transform.position.y;

        _lineRenderer.SetPosition(0, startPos);

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
