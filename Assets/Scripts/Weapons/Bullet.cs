using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private float range;
    [SerializeField] private float moveSpeed;
    [SerializeField] Rigidbody rb;
    public void Initialize(int dmg, float rng, Transform orientation)
    {
        damage = dmg; 
        range = rng;
        rb.velocity = orientation.forward * moveSpeed;
    }

    private void Update()
    {
         range -= Time.deltaTime;
        if(range < 0)
        {
            Destroy(gameObject);
        }
    }

}
