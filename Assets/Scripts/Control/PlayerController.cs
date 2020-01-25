using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {
        Mover moverController;
        Fighter fighter;

        void Start()
        {
            moverController = this.GetComponent<Mover>();
            fighter = this.GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.GetComponent<CombatTarget>().IsAlive()) return;
            if (InteractWithCombat())
            {
                print("OnTarget");
                return;
            }

            if (InteractWithMovement())
            {
                print("OnTerrain");
                return;
            }
            print("Nothing to do");


        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if (Mouse.current.leftButton.isPressed)
                {
                    moverController.StartMoveAction(hit.point);
                    return true;
                }
                return true;
                
            }return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            CombatTarget target = null;
            foreach (RaycastHit hit in hits)
            {
                target = hit.transform.GetComponent<CombatTarget>();
                if (target != null && target.IsAlive()) { break; }
            }
            if (target == null) { return false; }
            if (Mouse.current.leftButton.isPressed)
            {
                
                fighter.Attack(target);
            }            
            return true;
        }
    }
}
