﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Make sure this script is attached to the player
 */
public class PlayerInfo : MonoBehaviour, Entity
{
    [SerializeField] [Range (250f, 1000f)]
    private float playerHealth = 300f;

    private float m_maxHP;
    private float m_health;

    private PlayerMove m_playerMove;

/*    ---- DEPRECATED ----
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject sniper;
    public GameObject machineGun;
    public static int ammo = 100;
    private float shotgunDist = 0.25f;
    public static int grenadeAmount = 3;
*/

    // Getters
    public float   MaxHP { get { return m_maxHP; } }
    public float   HP    { get { return m_health; } }
    public Vector3 pos   { get { return transform.position; } }
    public Vector3 dir   { get { return transform.forward; } }
    public float   Speed { get { return m_playerMove.GetMoveSpeed(); } }

    // Broadcast this entity's death
    // (Caleb's SceneManager needs to subscribe to this)
    // And bring us back to the main menu
    public static event Action OnDeath;

    // param - How much damage the player took
    public static event Action<float> OnPlayerDamaged;

    private void Awake()
    {
        // Set player starting position
        transform.position = new Vector3(0f, transform.localScale.y, 0f);

        m_maxHP  = playerHealth;
        m_health = playerHealth;

        m_playerMove = this.gameObject.GetComponent<PlayerMove>();
    }

    public float GetMaxHP()
    {
        return MaxHP;
    }

    public float GetCurrentHP()
    {
        return m_health;
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;
        OnPlayerDamaged?.Invoke( dmg );

        if (m_health <= 0f)
        {
            OnDeath?.Invoke();
            return;
        }
    }

    public void GainHP(float hp)
    {
        m_health += hp;
        m_health = Mathf.Min( m_health, MaxHP );
    }
}
