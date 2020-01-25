using UnityEngine;
using System.Collections;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Transform target;
        Mover moverController;
        Fighter fighterController;

        // Use this for initialization
        void Start()
        {
            moverController = this.GetComponent<Mover>();
            fighterController = this.GetComponent<Fighter>();
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
            }
            else
            {
                fighterController.Cancel();
            }
        }

        private void ProcessScout()
        {
            //, LayerMask.NameToLayer("Player")
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
