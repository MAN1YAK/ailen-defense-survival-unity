﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Controller script for the regular Alien
 *  Attach this script to the enemy model
 */
[RequireComponent (typeof(NavMeshAgent))]
public class RegularAlien : MonoBehaviour, Alien, Entity
{
    [Header("Stats")]

    [SerializeField] [Range(50f, 300f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

    [SerializeField] [Range(5f, 20f)] 
    [Tooltip ("Distance at which they can detect the player")]
    private float detectionRange = 10f;

    [Header ("Attack")]

    [SerializeField] [Range(3f, 20f)] 
    private float dmgPerHit;

    [SerializeField] [Range(2.5f, 5f)]
    private float attackRange;

    [SerializeField] [Range(0.2f, 1.5f)]
    [Tooltip ("Time between hits (in seconds)")]
    private float attackSpeed;

    //Difficulty Multiplier based on chosen difficulty
    private DifficultyPicker difficultyPicker;


    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;

    // Getterss
    public float HP             { get { return m_health; } }
    public float MoveSpeed      { get { return moveSpeed; } }
    public float Damage         { get { return dmgPerHit; } }
    public float AttackRange    { get { return attackRange; } }
    public float AttackSpeed    { get { return attackSpeed; } }
    public float DetectionRange { get { return detectionRange; } }

    // ----- Events ------

    // param - Damage done to player
    public static event Action<float> OnAttackPlayer;

    // param - Position at time of death
    public static event Action<Vector3> OnDeath;

    /*
     * Broadcasts the info about the Alien
     * when he gets damaged
     * 
     * @param Vector3 - Position 
     * @param float   - Damage 
     */
    public static event Action<Vector3, float> OnDamaged;

    private void Awake()
    {
        // Initialize stats in Awake so it's ready before other scripts access it
        m_health = health * GetDifficultyMultiplier();
        dmgPerHit = dmgPerHit * GetDifficultyMultiplier();
        moveSpeed = moveSpeed * GetDifficultyMultiplier();
    }

    private void Start()
    {

        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("RegularAlien Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.stoppingDistance = (attackRange * 0.85f);

        // Nav mesh agent speed
        float speed = moveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(3f, speed);
        m_navMeshAgent.speed = speed;

        // Add states here
        stateMachine = new StateMachine();
        // stateMachine.AddState(new StateRegularAlienPatrol(this, m_playerInfo));
        stateMachine.AddState(new StateRegularAlienChase(this, m_playerInfo));
        stateMachine.AddState(new StateRegularAlienAttack(this, m_playerInfo));
        stateMachine.ChangeState("RegularAlienChase");
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private float GetDifficultyMultiplier()
    {
        if (difficultyPicker == null)
            difficultyPicker = FindObjectOfType<DifficultyPicker>(); // Ensure we get the reference

        return difficultyPicker.GetDifficultyMultiplier(); // Get the multiplier from DifficultyPicker
    }

    public void Attack()
    {
        float difficultyMultiplier = GetDifficultyMultiplier();
        OnAttackPlayer?.Invoke( dmgPerHit );
    }

    public void TakeDamage(float dmg)
    {
        if (m_health <= 0f)
            return;

        if (m_health - dmg <= 0f)
        {
            AudioManager.instance.Play("AlienRegular_Death");
            m_health = -Mathf.Epsilon;
            OnDeath?.Invoke(transform.position);
            Destroy(this.gameObject);
        }
        else
        {
            AudioManager.instance.Play("AlienRegular_Hurt");
            m_health -= dmg;
            OnDamaged?.Invoke( transform.position, dmg );
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

        if (stateMachine.GetCurrentState() == "RegularAlienPatrol")
        {
            Gizmos.color = Color.green;
        }
        else if (stateMachine.GetCurrentState() == "RegularAlienChase")
        {
            Gizmos.color = Color.yellow;
        }
        else if (stateMachine.GetCurrentState() == "RegularAlienAttack")
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(pos, 0.4f);
    }
}
