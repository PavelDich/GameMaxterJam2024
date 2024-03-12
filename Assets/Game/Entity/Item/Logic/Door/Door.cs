using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class Door : Logic
    {
        public Door() =>
            OnActivated.AddListener(Use);

        public void Use(bool state)
        {
            
        }
        public UnityEvent Open = new UnityEvent();
        public UnityEvent Close = new UnityEvent();
    }
}