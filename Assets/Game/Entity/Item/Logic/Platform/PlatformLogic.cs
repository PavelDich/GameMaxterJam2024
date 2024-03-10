using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class PlatformLogic : SetterObject
    {
        [SerializeField] Transform[] _platformPositions;
        [SerializeField] float _speedOfPlatform;
        [SerializeField] float _sleepTime;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _platformPositions[Convert.ToInt32(_enabled)].position) > .1)
            {
                transform.position = Vector3.MoveTowards(transform.position, _platformPositions[Convert.ToInt32(_enabled)].position, _speedOfPlatform * Time.deltaTime);
                audioSource.volume = .5f;
            }
            else
            {
                audioSource.volume = 0;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.transform.SetParent(transform);
        }

        private void OnCollisionExit(Collision collision)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}