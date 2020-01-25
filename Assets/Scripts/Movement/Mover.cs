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

        public void StartMoveAction(Vector3 destination)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }



        public void MoveTo(Vector3 destination)
        {
            
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
    }
}
