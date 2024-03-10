using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;
using UnityEditor;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class Inventory : NetworkBehaviour
    {
        private Transform _transform;
        public NetworkIdentity NetworkIdentity { get; private set; }

        public Item Item { get; private set; }
        [field: SerializeField]
        public float GrabDistance { get; private set; }
        [field: SerializeField]
        public LayerMask ItemLayer { get; private set; }

        [SerializeField]
        private Transform _hand;
        [SerializeField]
        private Transform _head;
        protected override void OnValidate()
        {
            base.OnValidate();
            _transform = GetComponent<Transform>();
            NetworkIdentity = GetComponent<NetworkIdentity>();
            if (_hand == null) Debug.LogError("Please change hand");
            if (_head == null) Debug.LogError("Please change head");
        }

        public void Grab()
        {
            RaycastHit[] hitColliders = Physics.RaycastAll(_head.position, _head.forward, GrabDistance, ItemLayer);
            if (hitColliders.Length <= 0) return;
            if (!hitColliders[0].collider.TryGetComponent<Item>(out Item item)) return;
            CmdGrab(item.NetworkIdentity);
        }
        [Command(requiresAuthority = false)]
        protected void CmdGrab(NetworkIdentity itemNetID)
        {
            Item item = itemNetID.GetComponent<Item>();
            if (item.isGrubed) return;
            if (Item != null) Drop();
            RpcGrab(item.NetworkIdentity);

        }
        [ClientRpc]
        protected void RpcGrab(NetworkIdentity itemNetID)
        {
            Debug.Log("5");
            Item item = itemNetID.GetComponent<Item>();
            item.Transform.SetParent(_hand, true);
            item.Collider.enabled = false;
            item.Rigidbody.isKinematic = true;
            item.Transform.position = _hand.position;
            item.Transform.rotation = _hand.rotation;
            item.isGrubed = true;
            Item = item;
        }

        public void Use()
        {

        }
        public void Drop()
        {
            cmdDrop();
        }
        [Command(requiresAuthority = false)]
        protected void cmdDrop()
        {
            if (Item == null) return;
            RpcDrop();
        }
        [ClientRpc]
        protected void RpcDrop()
        {
            Item.Transform.SetParent(null, true);
            Item.Collider.enabled = true;
            Item.Rigidbody.isKinematic = false;
            Item.isGrubed = false;
            Item = null;
        }
    }
}