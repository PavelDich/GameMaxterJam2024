using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : SetterObject
{
    [SerializeField] Transform[] _platformPositions;
    [SerializeField] float _speedOfPlatform;
    [SerializeField] float _sleepTime;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _platformPositions[Convert.ToInt32(_enabled)].position) < 10) 
        {
            transform.position = Vector3.MoveTowards(transform.position, _platformPositions[Convert.ToInt32(_enabled)].position, _speedOfPlatform * Time.deltaTime);
        }
    }
}
