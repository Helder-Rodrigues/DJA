using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sample
{
    public class GhostScript : MonoBehaviour
    {
        public Transform player;
        private NavMeshAgent agent;

        private Animator Anim;
        private CharacterController Ctrl;
        // Cache hash values
        private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
        private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
        // dissolve
        [SerializeField] private SkinnedMeshRenderer[] MeshR;

        public bool respawn = false;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
                MOVE();
                Respawn();
        }

        public void MOVE()
        {
            if (GameManager.GhostMove)
                agent.destination = player.position;
        }

        // respawn
        private void Respawn()
        {
            if (respawn)
            {
                Ctrl.enabled = false;
                transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero)); // player facing
                Ctrl.enabled = true;

                // reset animation
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
    }
}