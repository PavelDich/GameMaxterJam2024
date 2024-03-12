using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    public class Logic : NetworkBehaviour
    {
        [SyncVar, SerializeField]
        protected bool _isActiveted = false;
        public bool IsActiveted
        {
            get { return _isActiveted; }
            set
            {
                CmdSync(value);
                [Command(requiresAuthority = false)] void CmdSync(bool newValue) => SrvSync(newValue);
                [Server] void SrvSync(bool newValue)
                {
                    _isActiveted = newValue;
                    RpcSync(newValue);
                }
                [ClientRpc]
                void RpcSync(bool newValue) =>
                    OnActivated.Invoke(newValue);
            }
        }
        public UnityEvent<bool> OnActivated = new UnityEvent<bool>();
        public void SetIsActiveted(bool value) => IsActiveted = value;

        [SyncVar, SerializeField]
        protected bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                CmdSync(value);
                [Command(requiresAuthority = false)] void CmdSync(bool newValue) => SrvSync(newValue);
                [Server] void SrvSync(bool newValue)
                {
                    _isEnabled = newValue;
                    RpcSync(newValue);
                }
                [ClientRpc]
                void RpcSync(bool newValue) =>
                    OnEnabled.Invoke(newValue);
            }
        }
        public UnityEvent<bool> OnEnabled = new UnityEvent<bool>();
        public void SetIsEnabled(bool value) => IsEnabled = value;
    }
}