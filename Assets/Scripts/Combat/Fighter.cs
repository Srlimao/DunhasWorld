using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using TMPro;

namespace RPG.Combat
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] Transform handTransformR;
        [SerializeField] Transform handTransformL;
        [SerializeField] Weapon defaultWeaponConfig = null;

        Transform target;
        float timeSinceLastAttack;
        Weapon currentWeapon = null;

        private void Start()
        {
            currentWeapon = defaultWeaponConfig;
            EquipWeapon(currentWeapon);
            
        }

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
                if (timeSinceLastAttack> currentWeapon.weaponCooldown)
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
            return Vector3.Distance(transform.position, target.position) < currentWeapon.weaponRange;
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            currentWeapon.Spawn(handTransformR, handTransformL, GetComponent<Animator>());
            
        }

        //Animation Events
        void Hit()
        {
            if (target == null) return;
            target.GetComponent<Health>().TakeDamage(currentWeapon.weaponDamage);
        }
        void Shoot()
        {
            if (target == null)
            {
                Cancel();
                return;
            }
            GameObject arrow = Instantiate(currentWeapon.GetProjectile(), handTransformL.position,Quaternion.identity);
            target.gameObject.TryGetComponent<Collider>(out Collider targetCol);
            //arrow.transform.LookAt(target.position+(Vector3.up*(targetCol.bounds.size.y/2)));
            arrow.GetComponent<Projectile>().SetTarget(target);
            arrow.GetComponent<Projectile>().SetDamage(currentWeapon.weaponDamage);
        }

    }

}