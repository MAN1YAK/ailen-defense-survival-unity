﻿// Deprecated, remove this during merge

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularAlienPatrol : State
{
    private const float     SET_DEST_BUFFER = 0.75f;

    private RegularAlien   m_AlienController;
    private NavMeshAgent    m_navMeshAgent;
    private WaypointManager m_waypointManager;
    private PlayerInfo      m_playerInfo;
    private Vector3         m_currWaypoint;
    private float           m_setDestBuffer;

    public StateRegularAlienPatrol(RegularAlien AlienController,
                                    PlayerInfo    playerInfo)
    {
        m_AlienController = AlienController;
        m_navMeshAgent     = AlienController.GetComponent<NavMeshAgent>();
        m_waypointManager  = WaypointManager.Instance;
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        Vector3 initialWaypoint = m_waypointManager.GetRandomWaypoint();
        m_currWaypoint = initialWaypoint;
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.SetDestination( initialWaypoint );

        float speed = m_AlienController.MoveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(1.25f, speed);
        m_navMeshAgent.speed = speed;

        m_setDestBuffer = SET_DEST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        if (DistFromPlayer() <= m_AlienController.DetectionRange)
        {
            m_navMeshAgent.SetDestination(m_playerInfo.pos);
            m_AlienController.stateMachine.ChangeState("RegularAlienChase");
        }

        // Behavior
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer < 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;
            m_navMeshAgent.SetDestination( m_currWaypoint );
        }

        if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            Vector3 nextWaypoint = m_waypointManager.GetRandomWaypoint();
            m_navMeshAgent.SetDestination( nextWaypoint );
            m_currWaypoint = nextWaypoint;
        }
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "RegularAlienPatrol";
    }

    // Helper
    private float DistFromPlayer()
    {
        Vector3 myPos = m_AlienController.transform.position;
        Vector3 playerPos = m_playerInfo.pos;
        myPos.y = playerPos.y = 0f;

        return Vector3.Distance(myPos, playerPos);
    }
}*/