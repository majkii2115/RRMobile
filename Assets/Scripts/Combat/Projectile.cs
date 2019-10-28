using UnityEngine;
using RPG.Resources;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] bool isHoming = true;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float maxLifeTime = 6f;
    [SerializeField] UnityEvent onHit;
    Health target = null;
    GameObject instigator = null;
    float damage = 0;

    private void Start() 
    {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if (target == null) return;
        if(isHoming && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);      
    }
    public void SetTarget(GameObject instigator, Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
        this.instigator = instigator;

        Destroy(gameObject, maxLifeTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Health>() != target) return;
        if(target.IsDead()) return;
        onHit.Invoke();
        target.TakeDamage(instigator ,damage);
        speed = 0f;
        if (hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);            
        }
        Destroy(gameObject);

    }

}
}