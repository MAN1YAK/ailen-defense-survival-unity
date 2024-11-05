using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRunnerAlienAttack : State
{
    private RunnerAlien m_AlienController;
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_chaseDistance;
    private float        m_timePerHit;
    private float        m_attackTimer;
    private float        m_rotationSpeed;

    private Animator m_animator;

    public StateRunnerAlienAttack(RunnerAlien AlienController,
                                  PlayerInfo playerInfo)
    {
        m_AlienController = AlienController;
        m_navMeshAgent     = AlienController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;

        m_animator = AlienController.GetComponent<Animator>();
    }

    public override void OnStateEnter()
    {
        m_chaseDistance = m_AlienController.AttackRange;
        m_timePerHit = m_AlienController.AttackSpeed;
        m_attackTimer = 0f;

        m_rotationSpeed = m_navMeshAgent.angularSpeed;
        m_navMeshAgent.isStopped = true;

        m_animator.SetTrigger("isAttacking");
    }

    public override void OnStateUpdate()
    {
        // State transition
        bool PlayerOutOfRange = DistFromPlayer() > m_chaseDistance;
        if (PlayerOutOfRange)
            m_AlienController.stateMachine.ChangeState("RunnerAlienChase");

        // Attack
        m_attackTimer -= Time.deltaTime;
        if (m_attackTimer <= 0f)
        {
            m_attackTimer = m_timePerHit;
            m_AlienController.Attack();

            m_animator.SetTrigger("isAttacking");
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
        m_animator.ResetTrigger("isAttacking");
    }

    public override string GetStateID()
    {
        return "RunnerAlienAttack";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_AlienController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
