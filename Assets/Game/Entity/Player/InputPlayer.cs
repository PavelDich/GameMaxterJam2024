using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Mirror;


namespace GCinc.GameMaxterJam2024.PavelDich
{
    [DisallowMultipleComponent]
    public class InputPlayer : NetworkBehaviour
    {
        public GameObject[] LocalObjects;
        public GameObject[] GlobalObjects;

        public UnityEvent OnUseItem;
        public UnityEvent OnGrabItem;
        public UnityEvent OnDropItem;
        public UnityEvent<Vector3> OnBodyMove;
        public UnityEvent<Vector3> OnBodySprint;
        public UnityEvent<Vector3, Vector3> OnHeadMove;
        public UnityEvent OnJump;
        [SerializeField]
        private float _sensativityX = 100f;
        [SerializeField]
        private float _sensativityY = 100f;


        private void Start()
        {
            if (!isOwned) return;
            foreach (GameObject localObject in LocalObjects)
                localObject.SetActive(true);
            foreach (GameObject globalObject in GlobalObjects)
                globalObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void FixedUpdate()
        {
            if (isOwned) return;
            if (math.abs(Input.GetAxis("Horizontal")) > 0.5f || math.abs(Input.GetAxis("Vertical")) > 0.5f)
                if (!Input.GetKey(KeyCode.LeftShift))
                    OnBodyMove.Invoke(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
                else
                    OnBodySprint.Invoke(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
        }
        private void Update()
        {
            if (isOwned)
            {
                OnHeadMove.Invoke(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")),
                        new Vector3(_sensativityX, _sensativityY));
                if (Input.GetKeyDown(KeyCode.F))
                    OnGrabItem.Invoke();
                if (Input.GetKeyDown(KeyCode.F))
                    OnDropItem.Invoke();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    OnUseItem.Invoke();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    OnJump.Invoke();
            }
        }
    }
}