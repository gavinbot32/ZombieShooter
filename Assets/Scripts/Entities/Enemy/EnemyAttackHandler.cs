using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{

    [Header("Attacking")]
    public int damage;
    [Header("Hit Properties")]
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float maxHitCooldown;
    [SerializeField] private float hitCooldown;
    [Header("Components")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = this.SafeGetComponent(anim);
    }

    private void FixedUpdate()
    {
        if(hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
        }
        Ray rayCheck = new Ray(transform.position, transform.forward);
  
        
        if (hitCooldown < 0)
        {
            hitCooldown = maxHitCooldown;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray,hitRange, hitMask))
            {
                anim.SetTrigger("Attack");
            }
        }
    }
    public void Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hitRange, hitMask))
        {
            hit.collider.GetComponentInParent<PlayerController>().h_health.TakeDamage(damage);
        }
    }


}
