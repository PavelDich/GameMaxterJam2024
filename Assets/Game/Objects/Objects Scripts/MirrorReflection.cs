using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : LazerSelect
{
    // Объект, отражающий лазерный луч, сохраняя угол.
    // Примечание - лазер отражающийся между двух параллельных зеркал совершить только 2 итерации.

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
        // Векторная алгебра + нечитаемый код.
        // Буду исправлять.

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
        forw = Vector3.Reflect(forw, transform.forward);

        _lineRenderer.SetPosition(0, startPos);

        Ray ray = new Ray(startPos, forw);

        Debug.DrawRay(startPos, forw * defaultLenght, Color.red);

        // Очень много проверок.
        // Выполняются каждый кадр, когда зеркало активно.
        // Буду работать.

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
            _lineRenderer.SetPosition(1, forw * defaultLenght);
        }
    }


}
