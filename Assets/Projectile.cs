using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] bool isHoming = false;

    private Transform target;
    private float damage;
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

    public void SetTarget(Transform target)
    {
        this.target = target;
        SetLookAt();
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.TryGetComponent<Health>(out Health h)) return;
        h.TakeDamage(damage);
        Destroy(this.gameObject);
    }

    private void SetLookAt()
    {
        target.gameObject.TryGetComponent<Collider>(out Collider targetCol);
        this.transform.LookAt(targetCol.bounds.center);
    }
}
