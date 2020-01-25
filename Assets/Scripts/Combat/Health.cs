using UnityEngine;
using System.Collections;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;

        float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }


        public void TakeDamage(float damage)
        {
            if (currentHealth == 0) return;
            currentHealth = Mathf.Max(currentHealth-damage,0);
            print("hp: " + currentHealth);
            if(currentHealth<= 0)
            {
                TriggerDeath();
            }

        }

        public bool IsDead()
        {
            return (currentHealth <= 0);
        }

        private void TriggerDeath()
        {
            GetComponent<Animator>().SetTrigger("Death");

        }


    }

}
