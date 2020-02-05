using RPG.Core;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] bool isHoming = false;
    [SerializeField] GameObject impactPrefab;

    private Transform target;
    private GameObject instigator = null;
    private float damage;
    private float aimPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (isHoming) SetLookAt();
    }

    public void SetTarget(Transform target,GameObject instigator)
    {
        this.instigator = instigator;
        this.target = target;
        target.gameObject.TryGetComponent(out Collider targetCol);
        aimPoint = targetCol.bounds.size.y;
        transform.LookAt(target.position + (Vector3.up * aimPoint/2));
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("Player")) return;
        if (other.gameObject.TryGetComponent<Health>(out Health h))
        {
            h.TakeDamage(damage, instigator);
        }
        if (impactPrefab != null) 
        { 
            GameObject projectile=Instantiate(impactPrefab, this.transform.position, this.transform.rotation);
            Destroy(projectile, projectile.GetComponentInChildren<ParticleSystem>().main.duration);
            
        }
        Destroy(this.gameObject);
    }

    private void SetLookAt()
    {   
        target.gameObject.TryGetComponent<Collider>(out Collider targetCol);
        if (targetCol.enabled == false) return;
        print(targetCol.bounds.size.y);
        this.transform.LookAt(targetCol.bounds.center);
    }
}
