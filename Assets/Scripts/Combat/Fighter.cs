using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponCooldown = 1f;
        [SerializeField] float weaponDamage = 15f;

        Transform target;
        float timeSinceLastAttack;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null || target.CompareTag(this.tag) || !target.GetComponent<CombatTarget>().IsAlive()) return;
            
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position,1);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                if (timeSinceLastAttack> weaponCooldown)
                {
                    AttackBehaviour();
                    
                }
                
            }
        }

        private void AttackBehaviour()
        {
            if (target.GetComponent<Health>().IsDead()) return;
            this.transform.LookAt(target.position);
            TriggerAnimAttack();
            timeSinceLastAttack = 0;
        }

        private void TriggerAnimAttack()
        {
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }


        public void Attack(CombatTarget combatTarget)
        {

            this.GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetTrigger("CancelAttack");
            GetComponent<Mover>().Cancel();
        }

        //Animation Event
        void Hit()
        {
            if (target == null) return;
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }

    }

}