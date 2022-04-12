using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //patroling
    public Vector3 walkPoint;
    Vector3 prevWalkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool hit;

    public Animator anim;
    public RaycastHit hitt;
    private void Awake()
    {
        if(GameObject.Find("Player") != null)
            player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        walkPointSet = true;
        prevWalkPoint = walkPoint;
        anim = transform.GetChild(0).GetComponent<Animator>();
        hit = false;
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(hit == true)
        {
            MoveBack();
        } else
        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        else if (playerInSightRange && !playerInAttackRange)
            Chase();
        else if (playerInSightRange && playerInAttackRange)
            Attack();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            anim.SetBool("IsRunning", false);
            SearchWalkPoint();
        }
        else if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            anim.SetBool("IsRunning", true);
        }
        float distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);
        if (distanceToWalkPoint < agent.stoppingDistance + 1)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
        anim.SetBool("IsRunning", true);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        float distanceToWalkPoint = Vector3.Distance(transform.position, player.position);
        if (distanceToWalkPoint < agent.stoppingDistance + 1)
            anim.SetBool("IsRunning", false);
        if (!alreadyAttacked)
        {
            //Attack code here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void MoveBack()
    {
        anim.SetTrigger("Hurt");
        StartCoroutine(TakeBack());
    }

    IEnumerator TakeBack()
    {
        Rigidbody rig = gameObject.GetComponent<Rigidbody>();
        /*
        Vector3 pos = -transform.forward * 10f;

        agent.SetDestination(pos);*/
        rig.isKinematic = false;
        rig.AddForce(-hitt.normal * 100);
        
        yield return new WaitForSeconds(0.1f);
        rig.isKinematic = true;
        hit = false;
    }
}
