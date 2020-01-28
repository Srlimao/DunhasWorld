using System;
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

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand,Transform leftHand,Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if (weaponPrefab == null || rightHand == null) return;
            GameObject weapon = Instantiate(weaponPrefab, isLeftHanded? leftHand : rightHand);
            weapon.name = weaponName;
            animator.runtimeAnimatorController = animOverride;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform currWeapon;
            currWeapon = rightHand.Find(weaponName);
            if (currWeapon==null)
            {
                currWeapon = leftHand.Find(weaponName);
                if (currWeapon == null) return;
            }
            currWeapon.name = "dEsTroyIng";
            Destroy(currWeapon.gameObject);
        }

        public GameObject GetProjectile()
        {
            return projectilePrefab;
        }


    }

}