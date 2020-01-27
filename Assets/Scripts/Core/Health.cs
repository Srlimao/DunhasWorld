
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float maxHealth = 100f;

        float currentHealth;
        bool isDead = false;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            TriggerDeath();
        }


        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Max(currentHealth-damage,0);
            print("hp: " + currentHealth);
            

        }

        public bool IsDead()
        {
            return isDead;
        }

        private void TriggerDeath()
        {
            if (isDead) return;
            if (currentHealth > 0) return;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            isDead = true;

        }
        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            currentHealth = (float)state;
        }
    }

}
