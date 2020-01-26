using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;


namespace RPG.Movement
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Mover : MonoBehaviour, IAction
    {
        // Start is called before the first frame update
        [SerializeField] float maxSpeed = 6f;

        Vector3 target;

        private NavMeshAgent navMesh;
        private Animator anim;

        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination,float speedFraction)
        {
            
            this.GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }



        public void MoveTo(Vector3 destination, float speedFraction)
        {
            SetSpeedFraction(speedFraction);
            navMesh.isStopped = false;
            navMesh.SetDestination(destination);
            
        }

        public void Cancel()
        {
            navMesh.isStopped = true;
        }

        private void UpdateAnimator()
        {
            var globalVel = navMesh.velocity;
            Vector3 localVel = transform.InverseTransformDirection(globalVel);
            anim.SetFloat("ForwardSpeed", localVel.z);
        }

        private void SetSpeedFraction(float speedFraction)
        {
            navMesh.speed = maxSpeed*speedFraction;
        }
    }
}
