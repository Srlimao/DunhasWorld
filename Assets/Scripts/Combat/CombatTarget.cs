using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool IsAlive()
        {
            return !transform.GetComponent<Health>().IsDead();
        }
    }
}
