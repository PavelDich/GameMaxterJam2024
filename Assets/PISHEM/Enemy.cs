using System.Collections;
using System.Collections.Generic;
using Mirror.Examples.Basic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace GCinc.GameMaxterJam2024.PavelDich
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [Range(0, 360)] public float ViewAngle = 90f;
        public float ViewDistance = 15f;
        public float DetectionDistance = 3f;
        public Transform EnemyEye;
        [Inject]
        public NetworkManager _networkManager;
        public List<GameObject> Target = new List<GameObject>();
        private NavMeshAgent agent;
        private float rotationSpeed;
        public float DamagePerSecond = 20;
        private Transform agentTransform;
        private AudioSource audioSource;
        public Transform[] waypoints;
        private int destPoint = 0;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            rotationSpeed = agent.angularSpeed;
            agentTransform = agent.transform;
            audioSource = GetComponent<AudioSource>();
        }
        void GotoNextPoint()
        {
            if (waypoints.Length == 0)
                return;

            agent.destination = waypoints[destPoint].position;
            destPoint = (destPoint + 1) % waypoints.Length;
            Vector3 directionToWaypoint = (waypoints[destPoint].position - transform.position).normalized;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToWaypoint, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        private void Update()
        {
            Target = _networkManager.Players;
            foreach (GameObject go in Target)
            {
                float distanceToPlayer = Vector3.Distance(go.transform.position, agent.transform.position);
                if (distanceToPlayer <= DetectionDistance || IsInView(go.transform))
                {
                    RotateToTarget(go.transform);
                    MoveToTarget(go.transform);

                }
                else
                {
                    audioSource.mute = true;
                }
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();
                //DrawViewState();
            }
        }

        private bool IsInView(Transform go)
        {
            float realAngle = Vector3.Angle(EnemyEye.forward, go.position - EnemyEye.position);
            RaycastHit hit;
            if (Physics.Raycast(EnemyEye.transform.position, go.position - EnemyEye.position, out hit, ViewDistance))
            {
                if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, go.position) <= ViewDistance && hit.transform == go.transform)
                {
                    return true;

                }
            }
            return false;

        }
        private void RotateToTarget(Transform go)
        {
            Vector3 lookVector = go.position - agentTransform.position;
            lookVector.y = 0;
            if (lookVector == Vector3.zero) return;
            agentTransform.rotation = Quaternion.RotateTowards
                (
                    agentTransform.rotation,
                    Quaternion.LookRotation(lookVector, Vector3.up),
                    rotationSpeed * Time.deltaTime
                );

        }
        private void MoveToTarget(Transform go)
        {
            agent.SetDestination(go.position);
            audioSource.mute = false;
        }


        private void DrawViewState()
        {
            Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
            Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
            Debug.DrawLine(EnemyEye.position, left, Color.yellow);
            Debug.DrawLine(EnemyEye.position, right, Color.yellow);
        }
        private void OnCollisionStay(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Alive alive = collision.GetComponent<Alive>();
                alive.Health.ChangeValue(alive.Health.Value - DamagePerSecond * Time.deltaTime);
            }
        }
        public void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.name);
        }
    }
}