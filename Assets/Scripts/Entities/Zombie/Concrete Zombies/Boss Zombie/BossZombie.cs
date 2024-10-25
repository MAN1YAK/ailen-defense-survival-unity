﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Controller script for the Boss zombie
 *  Attach this script to the enemy model
 */
[RequireComponent(typeof(NavMeshAgent))]
public class BossZombie : MonoBehaviour, Zombie, Entity
{
    [Header("References")]
    [SerializeField] private GameObject fireball;

    [Header("Stats")]

    [SerializeField] [Range(50f, 300f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

/*    [SerializeField] [Range(3f, 20f)]
    [Tooltip("Distance at which they can detect the player")]
    private float detectionRange = 10f;*/

    [Header("Attack")]

    [SerializeField] [Range(5f, 50f)]
    private float attackDamage;

    [SerializeField] [Range(12f, 30f)]
    private float attackRange;

    [SerializeField] [Range(0.1f, 1.5f)] 
    [Tooltip ("Time between attacks (in seconds)")]
    private float attackSpeed;

    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;

    public float HP             { get { return m_health; } }
    public float MoveSpeed      { get { return moveSpeed; } }
    public float Damage         { get { return attackDamage; } }
    public float AttackRange    { get { return attackRange; } }
    public float AttackSpeed    { get { return attackSpeed; } }
    // public float DetectionRange { get { return detectionRange; } }

    // Broadcast this entity's position at time of death
    public static event Action<Vector3> OnDeath;

    /*
     * Broadcasts the info about the zombie
     * when he gets damaged
     * 
     * @param Vector3 - Spawn position 
     * @param float   - Damage 
     */
    public static event Action<Vector3, float> OnDamaged;

    private void Start()
    {
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("BossZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.stoppingDistance = this.attackRange * 0.25f;

        // Nav mesh agent speed
        float speed = moveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(3f, speed);
        m_navMeshAgent.speed = speed;

        // Add states here
        stateMachine = new StateMachine();
        // stateMachine.AddState(new StateBossZombiePatrol(this, m_playerInfo));
        stateMachine.AddState(new StateBossZombieChase(this, m_playerInfo));
        stateMachine.AddState(new StateBossZombieAttack(this, m_playerInfo));
        stateMachine.ChangeState("BossZombieChase");

        m_health = health;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void Attack()
    {
        Vector3 pos = transform.position + transform.forward * 0.8f;
        pos += new Vector3(0f, 1.5f, 0f);
        Instantiate(fireball, pos, transform.rotation);
    }

    public void TakeDamage(float dmg)
    {
        if (m_health <= 0f)
            return;

        if (m_health - dmg <= 0f)
        {
            m_health = -Mathf.Epsilon;
            OnDeath?.Invoke(transform.position);
            Destroy(this.gameObject);
        }
        else
        {
            m_health -= dmg;
            OnDamaged?.Invoke(transform.position, dmg);
        }
    }

    public float GetMaxHP()
    {
        return health;
    }

    public float GetCurrentHP()
    {
        return m_health;
    }

    public GameObject GetGameObject()
    {
        if (this != null)
            return this.gameObject;

        return null;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Vector3 pos = transform.position;
        pos += new Vector3(0f, 3.8f, 0);

        if (stateMachine.GetCurrentState() == "BossZombiePatrol")
        {
            Gizmos.color = Color.green;
        }
        else if (stateMachine.GetCurrentState() == "BossZombieChase")
        {
            Gizmos.color = Color.yellow;
        }
        else if (stateMachine.GetCurrentState() == "BossZombieAttack")
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(pos, 0.4f);
    }
}
