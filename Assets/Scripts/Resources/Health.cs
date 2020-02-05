
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        float baseHealth;
        float currentHealth;
        bool isDead = false;

        private void Awake()
        {
            baseHealth = GetComponent<BaseStats>().GetHealth();
            currentHealth = baseHealth;
        }
        private void Start()
        {
            TriggerDeath();
        }

        public void TakeDamage(float damage,GameObject instigator)
        {
            currentHealth = Mathf.Max(currentHealth-damage,0);
            TriggerDeath(instigator);
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void TriggerDeath(GameObject instigator=null)
        {
            if (isDead) return;
            if (currentHealth > 0) return;
            AwardExperience(instigator);
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Collider>().enabled = false;
            isDead = true;

        }

        private void AwardExperience(GameObject instigator)
        {
            if (instigator != null)
            {
                instigator.TryGetComponent(out Experience xpComp);
                xpComp.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
            }
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float GetCurrentHealthPercent()
        {
            return Mathf.RoundToInt((currentHealth/baseHealth)*100);
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
