using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent deathEvent;

    public int maxHealth = 100;
    public int health;

    private void Awake()
    {
        health = maxHealth;
    }
  
    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        DeathCheck();
    }

    private void DeathCheck()
    {
        if(health <= 0)
        {
            deathEvent.Invoke();
        }
    }

    public void DestroyDeath()
    {
        Destroy(gameObject);
    }
}
