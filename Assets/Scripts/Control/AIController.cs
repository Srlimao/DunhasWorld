using UnityEngine;
using System.Collections;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float chaseTime = 5f;
        [SerializeField] PatrolPath path;
        [SerializeField] float waypointTolerance = .5f;
        [SerializeField] float dwellTime = 5f;
        [SerializeField] [Range(0f, 1f)] float patrolSpeedFraction = .2f;

        Transform target;
        Fighter fighterController;
        Mover moverController;
        Vector3 guardPosition;
        float timeSinceLastSawPlayer;
        int currentWaypointIndex = 0;
        float timeSinceArriveAtWaypoint;
        

        // Use this for initialization
        void Start()
        {
            fighterController = this.GetComponent<Fighter>();
            moverController = this.GetComponent<Mover>();
            guardPosition = transform.position;
            timeSinceLastSawPlayer = Mathf.Infinity;
            timeSinceArriveAtWaypoint = Mathf.Infinity;
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.GetComponent<CombatTarget>().IsAlive()) return;
            ProcessScout();
            if (target != null && target.GetComponent<CombatTarget>().IsAlive())
            {
                print(target.name);
                //moverController.StartMoveAction(target.position);
                fighterController.Attack(target.GetComponent<CombatTarget>());
                timeSinceLastSawPlayer = 0;
            }
            else if (timeSinceLastSawPlayer < chaseTime)
            {
                fighterController.Cancel();
            }
            else
            {
                PatrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void ProcessScout()
        {
            Collider[] coliders = Physics.OverlapSphere(this.transform.position, chaseDistance, (1 << 8));
            foreach (Collider c in coliders)
            {
                target = c.transform;
                if (Vector3.Distance(transform.position, target.position) < chaseDistance)
                {
                    print("target found");
                    break;
                }
                else
                {
                    target = null;
                }
            }
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (path != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArriveAtWaypoint = 0;
                    CycleWaypoint();
                    
                }
                nextPosition = GetCurrentWaypoint();
                timeSinceArriveAtWaypoint += Time.deltaTime;


            }
            if (timeSinceArriveAtWaypoint > dwellTime)
            {
                moverController.StartMoveAction(nextPosition,patrolSpeedFraction);
            }
            
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = path.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return path.GetWaypoint(currentWaypointIndex);
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.DrawSphere(guardPosition, .3f);
        }
    }
}
