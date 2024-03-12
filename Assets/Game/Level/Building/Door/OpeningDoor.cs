using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class OpeningDoor : SetterObject
    {
        [SerializeField] Animator _animator;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        internal override void ChangeState(bool newstate)
        {
            if (newstate) { audioSource.Play(); }
            _animator.SetBool("Open", newstate);
        }
    }
}