using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponEquip;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Fighter fighter = other.gameObject.GetComponent<Fighter>();
        fighter.EquipWeapon(weaponEquip);
        Destroy(this.gameObject);
    }
}
