using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Minicop.Library.Stats;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NetworkIdentity))]
    public class Alive : NetworkBehaviour
    {
        public Parameter Health = new Parameter();
        [SyncVar(hook = nameof(HealthSyncing))]
        public float HealthSync;
        private void HealthSyncing(float oldValue, float newValue) => Health.Value = newValue;
        private void CheckHealth(float oldValue, float newValue)
        {
            if (oldValue > newValue) Health.Regeneration(this);

            CmdSync(Health.Value);
            [Command(requiresAuthority = false)] void CmdSync(float newValue) => SrvSync(newValue);
            [Server] void SrvSync(float newValue) => HealthSync = newValue;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            Parameter.OnValidate.Invoke();
            HealthSync = Health.Value;
        }
        protected virtual void Start()
        {
            Health.OnChenges.Value.AddListener(CheckHealth);
        }
    }
}