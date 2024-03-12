using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class PressurePlat : Logic
    {
        [SerializeField]
        private LayerMask _layerPlayer;
        private void OnTriggerEnter(Collider col)
        {
            if ((~_layerPlayer & (1 << col.gameObject.layer)) != 0) return;
            IsActiveted = true;
        }

        private void OnTriggerExit(Collider col)
        {
            if ((~_layerPlayer & (1 << col.gameObject.layer)) != 0) return;
            IsActiveted = false;
        }
    }
}