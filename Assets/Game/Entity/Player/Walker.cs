using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using Minicop.Library.Stats;


namespace GCinc.GameMaxterJam2024.PavelDich
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NetworkIdentity))]
    [RequireComponent(typeof(Rigidbody))]
    public class Walker : NetworkBehaviour
    {
        [field: SerializeField, HideInInspector, Header("Components")]
        public Transform _transform { get; private set; }
        [field: SerializeField, HideInInspector]
        public NetworkIdentity _networkIdentity { get; private set; }
        [field: SerializeField, HideInInspector]
        public Rigidbody _rigidbody { get; private set; }
        [SerializeField]
        private Transform _head;


        [field: SerializeField, Header("Parameters")]
        public float JumpForce { get; private set; } = 250;
        [field: SerializeField]
        public float GroundDistance { get; private set; } = 1;
        [field: SerializeField]
        public LayerMask Ground { get; private set; }

        [field: SerializeField]
        public float SpeedMove { get; private set; } = 5;
        [field: SerializeField]
        public float SpeedSprint { get; private set; } = 10;


        public Parameter Stamina = new Parameter();
        [SyncVar(hook = nameof(StaminaSyncing))]
        public float StaminaSync;
        private void StaminaSyncing(float oldValue, float newValue) => Stamina.Value = newValue;
        private void CheckStamina(float oldValue, float newValue)
        {
            if (oldValue > newValue) Stamina.Regeneration(this);

            CmdSync(Stamina.Value);
            [Command(requiresAuthority = false)] void CmdSync(float newValue) => SrvSync(newValue);
            [Server] void SrvSync(float newValue) => StaminaSync = newValue;
        }

        private float _rotationX;
        private float _rotationY;





        protected override void OnValidate()
        {
            base.OnValidate();

            _transform = GetComponent<Transform>();
            _networkIdentity = GetComponent<NetworkIdentity>();
            _rigidbody = GetComponent<Rigidbody>();

            Parameter.OnValidate.Invoke();
            StaminaSync = Stamina.Value;
        }
        protected virtual void Start()
        {
            Stamina.OnChenges.Value.AddListener(CheckStamina);
        }



        public void Move(Vector3 vector)
        {
            CmdMove(vector);
        }
        [Command(requiresAuthority = false)]
        private void CmdMove(Vector3 vector)
        {
            Vector3 moveDirection = transform.TransformDirection(new Vector3(vector.x, 0f, vector.z));
            _rigidbody.velocity = moveDirection * SpeedMove + new Vector3(0f, _rigidbody.velocity.y, 0f);
        }

        public void Sprint(Vector3 vector)
        {
            CmdSprint(vector);
        }
        [Command(requiresAuthority = false)]
        private void CmdSprint(Vector3 vector)
        {
            Vector3 moveDirection = transform.TransformDirection(new Vector3(vector.x, 0f, vector.z));
            _rigidbody.velocity = moveDirection * SpeedSprint + new Vector3(0f, _rigidbody.velocity.y, 0f);

            Stamina.ChangeValue(Stamina.Value - 1 * Time.fixedDeltaTime);
        }

        public void Jump()
        {
            CmdJump();
        }
        [Command(requiresAuthority = false)]
        private void CmdJump()
        {
            RaycastHit[] hitColliders = Physics.RaycastAll(_transform.position, Vector3.down, GroundDistance, Ground);
            if (hitColliders.Length <= 0) return;

            _rigidbody.AddForce(new Vector3(0f, JumpForce, 0f));
            Stamina.ChangeValue(Stamina.Max);
        }

        public void Rotate(Vector3 rotation, Vector3 sensativity)
        {
            CmdRotate(rotation, sensativity);
            _rotationY -= rotation.y * sensativity.y * 0.02f;
            _head.localRotation = Quaternion.Euler(Mathf.Clamp(_rotationY, -80f, 80f), 0f, 0f);
        }
        [Command(requiresAuthority = false)]
        private void CmdRotate(Vector3 rotation, Vector3 sensativity)
        {
            _rotationX += rotation.x * sensativity.x * 0.02f;
            _transform.rotation = Quaternion.Euler(0f, _rotationX, 0f);
        }
    }
}