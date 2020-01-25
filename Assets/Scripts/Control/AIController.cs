using UnityEngine;
using System.Collections;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float chaseTime = 5f;

        Transform target;
        Fighter fighterController;
        Mover moverController;
        Vector3 guardPosition;
        float timeSinceLastSawPlayer;

        // Use this for initialization
        void Start()
        {
            fighterController = this.GetComponent<Fighter>();
            moverController = this.GetComponent<Mover>();
            guardPosition = transform.position;
            timeSinceLastSawPlayer = Mathf.Infinity;
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
                fighterController.Cancel();
                moverController.StartMoveAction(guardPosition);
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

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
