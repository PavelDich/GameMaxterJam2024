using UnityEngine;
using UnityEngine.UI;
using Minicop.Library.Stats;
using Mirror;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    [RequireComponent(typeof(Image))]
    public class ParameterUI : NetworkBehaviour
    {
        [SerializeField, HideInInspector]
        private Image _parameterImage;
        [SerializeField, HideInInspector]
        private NetworkIdentity _networkIdentity;
        public void UpdateValue(Parameter parameter)
        {
            CmdUpdateValue(parameter.Value / parameter.Max);
            [Command(requiresAuthority = false)]
            void CmdUpdateValue(float value) => RpcUpdateValue(value);
            [Server, ClientRpc]
            void RpcUpdateValue(float value) => _parameterImage.fillAmount = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            _parameterImage = GetComponent<Image>();
            _networkIdentity = GetComponentInParent<NetworkIdentity>();
        }
    }
}