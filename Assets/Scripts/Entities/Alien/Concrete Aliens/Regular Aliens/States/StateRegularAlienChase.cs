using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularAlienChase : State
{
    // Don't wanna keep recalculating pathfinding every frame
    private const float   SET_DEST_BUFFER = 1.8f;

    private RegularAlien m_AlienController;
    private NavMeshAgent  m_navMeshAgent;
    private PlayerInfo    m_playerInfo;
    private float         m_setDestBuffer;

    private Animator m_animator;

    public StateRegularAlienChase(RegularAlien AlienController,
                                   PlayerInfo    playerInfo)
    {
        m_AlienController = AlienController;
        m_navMeshAgent     = AlienController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;

        m_animator = AlienController.GetComponent<Animator>();
    }
    public override void OnStateEnter()
    {
        m_navMeshAgent.ResetPath();
        m_navMeshAgent.isStopped      = false;
        m_navMeshAgent.autoBraking    = true;
        m_navMeshAgent.updatePosition = true;
        m_navMeshAgent.updateRotation = true;

        m_setDestBuffer  = 0f;

        m_animator.SetTrigger("isRunning");
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        bool PlayerWithinRange = (DistFromPlayer() <= m_navMeshAgent.stoppingDistance);
        if (PlayerWithinRange)
            m_AlienController.stateMachine.ChangeState("RegularAlienAttack");

        // Set destination buffer
        m_setDestBuffer += Time.deltaTime;
        if (m_setDestBuffer >= SET_DEST_BUFFER)
        {
            m_setDestBuffer = 0f;
            m_navMeshAgent.SetDestination(m_playerInfo.pos);

            m_animator.SetTrigger("isRunning");
        }

        // Set random speed every few seconds
        /*        m_setSpeedBuffer -= Time.deltaTime;
                if (m_setSpeedBuffer <= 0f)
                {
                    m_setSpeedBuffer = UnityEngine.Random.Range(2f, 6f);
                    float newSpeed = m_AlienController.MoveSpeed + UnityEngine.Random.Range(-3f, 3f);
                    m_navMeshAgent.speed = Mathf.Max(newSpeed, m_navMeshAgent.speed);
                }*/
    }

    public override void OnStateExit()
    {
        
    }

    public override string GetStateID()
    {
        return "RegularAlienChase";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_AlienController.transform.position;

        float distanceFromPlayer = (playerPos - myPos).magnitude;
        return distanceFromPlayer;
    }
}
