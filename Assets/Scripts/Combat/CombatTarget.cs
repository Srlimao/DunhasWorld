﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        
        public bool IsAlive()
        {
            return !transform.GetComponent<Health>().IsDead();
        }
    }
}
