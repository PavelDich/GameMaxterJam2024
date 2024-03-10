using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : GetterObject
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeState(true);
        audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeState(false);
    }
}

