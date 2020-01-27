using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] public float weaponRange = 2f;
        [SerializeField] public float weaponCooldown = 1f;
        [SerializeField] public float weaponDamage = 15f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] bool isLeftHanded = false;
        [SerializeField] GameObject projectilePrefab = null;
        
        [SerializeField] AnimatorOverrideController animOverride;

        public void Spawn(Transform rightHand,Transform leftHand,Animator animator)
        {
            if (weaponPrefab == null || rightHand == null) return;
            Instantiate(weaponPrefab, isLeftHanded? leftHand : rightHand);
            animator.runtimeAnimatorController = animOverride;
        }

        public GameObject GetProjectile()
        {
            return projectilePrefab;
        }
    }

}