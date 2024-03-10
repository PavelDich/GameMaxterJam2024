using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NetworkIdentity))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Item : NetworkBehaviour
    {
        public Transform Transform { get; private set; }
        public Collider Collider { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public NetworkIdentity NetworkIdentity { get; private set; }

        [SyncVar, HideInInspector]
        public bool isGrubed = false;
        protected override void OnValidate()
        {
            base.OnValidate();
            Transform = GetComponent<Transform>();
            Collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();
            NetworkIdentity = GetComponent<NetworkIdentity>();
        }
    }
}