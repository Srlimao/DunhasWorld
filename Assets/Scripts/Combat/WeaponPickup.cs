using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponEquip = null;
    [SerializeField] float respawnTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Fighter fighter = other.gameObject.GetComponent<Fighter>();
        fighter.EquipWeapon(weaponEquip);
        StartCoroutine(DisablePickup(respawnTime));

    }
    
    private IEnumerator DisablePickup(float timeout)
    {
        HidePickup();
        yield return new WaitForSeconds(timeout);
        ShowPickup();
    }

    private void ShowPickup()
    {
        SetEnabled(true);
    }

    private void HidePickup()
    {
        SetEnabled(false);
    }
    private void SetEnabled(bool b)
    {
        GetComponent<Collider>().enabled = b;
        GetComponentInChildren<Light>().enabled = b;
        MeshRenderer[] childs = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer c in childs)
        {
            c.enabled = (b);
        }
    }
}
