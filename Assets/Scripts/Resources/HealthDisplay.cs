using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] Text healthDisplay;
        [SerializeField] Text xpDisplay;
        [SerializeField] Text targetHealthDisplay;
        
        Fighter playerFighter;
        Health playerHealth;
        Experience playerExp;


        private void Awake()
        {
            playerFighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            playerExp = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            

        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            healthDisplay.text = playerHealth.GetCurrentHealth()+" - "+playerHealth.GetCurrentHealthPercent() + "%";
            xpDisplay.text = playerExp.GetCurrentExp()+"";
            Transform target = playerFighter.GetTarget();
            if (target != null)
            {
                target.TryGetComponent(out Fighter targetFighter);
                targetHealthDisplay.text = targetFighter.GetComponent<Health>().GetCurrentHealthPercent() + "%";
            }
            else
            {
                targetHealthDisplay.text = "N/A";
            }
        }
    }
}
