using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public Transform target;
    
    [Header("Components")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerManager playerManager;
    private void Start()
    {
        SetComponents();
    }
    private void SetComponents()
    {
        rb = this.SafeGetComponent(rb);
        navAgent = this.SafeGetComponent(navAgent);
    }

    private void FixedUpdate()
    {
        GetTarget();
        navAgent.SetDestination(target.position);
        rb.angularVelocity = Vector3.zero;
    }

    private void GetTarget()
    {
        if (playerManager.players.Count <= 0)
        {
            target = transform; 
            return;
        }

        if (playerManager.players.Count > 1)
        {
            target = GetClosestPlayer();
        }
        else
        {
            
            target = playerManager.players[0].transform;
        }
    }

    private Transform GetClosestPlayer()
    {
        Transform closest = target;
        float distance = Vector3.Distance(transform.position, closest.position);
        foreach (PlayerController player in playerManager.players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < distance)
            {
                closest = player.transform;
            }
        }
        return closest;
        
    }


    public void Initialize(PlayerManager pm)
    {
        if(playerManager == null) {
            playerManager = pm;
        }
        navAgent.speed = speed;
        GetTarget();
    }

}
