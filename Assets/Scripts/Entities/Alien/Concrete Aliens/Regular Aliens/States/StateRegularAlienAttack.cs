using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularAlienAttack : State
{
    private RegularAlien m_AlienController;
    private NavMeshAgent  m_navMeshAgent;
    private PlayerInfo    m_playerInfo;
    private float         m_chaseDistance;
    private float         m_timePerHit;
    private float         m_attackTimer;
    private float         m_rotationSpeed;

    public StateRegularAlienAttack(RegularAlien AlienController,
                                    PlayerInfo    playerInfo)
    {
        m_AlienController = AlienController;
        m_navMeshAgent     = AlienController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_chaseDistance = m_AlienController.AttackRange;
        m_timePerHit = m_AlienController.AttackSpeed;
        m_attackTimer = 0f;

        m_rotationSpeed = m_navMeshAgent.angularSpeed;
        m_navMeshAgent.isStopped = true;
        m_navMeshAgent.autoBraking = true;
    }

    public override void OnStateUpdate()
    {
        // State transition
        bool PlayerOutOfRange = DistFromPlayer() > m_chaseDistance;
        if (PlayerOutOfRange)
            m_AlienController.stateMachine.ChangeState("RegularAlienChase");

        // Attack
        m_attackTimer -= Time.deltaTime;
        if (m_attackTimer <= 0f)
        {
            m_attackTimer = m_timePerHit;
            m_AlienController.Attack();
        }

        // Rotate towards player
        Vector3 targetDir = m_playerInfo.pos - m_AlienController.transform.position;
        targetDir.y = 0;
        targetDir.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Quaternion currentRotation = m_AlienController.transform.rotation;
        float rotationStep = m_rotationSpeed * Time.deltaTime;

        Quaternion newRotation = Quaternion.RotateTowards(currentRotation,
                                                          targetRotation,
                                                          rotationStep);

        m_AlienController.transform.rotation = newRotation;
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "RegularAlienAttack";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_AlienController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
