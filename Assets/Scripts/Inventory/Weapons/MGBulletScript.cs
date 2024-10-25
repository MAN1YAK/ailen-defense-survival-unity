﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        float yRotation = Random.Range(-10, 10); // Spread should be 10

        Quaternion bulletRotatio = Quaternion.Euler(0, yRotation, 0);
        dir = bulletRotatio * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 15f; // Damage should be 15f
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 2f) // Dist should be 2f
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Unit Cube Wall(Clone)")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // Updated dmg given
            RegularZombie regularZombie = other.gameObject.GetComponent<RegularZombie>();
            if (regularZombie != null)
            {
                if (regularZombie.GetCurrentHP() > 0f)
                {
                    regularZombie.TakeDamage(dmg);
                }
            }

            SuicideBomberZombie suicideBomberZombie = other.gameObject.GetComponent<SuicideBomberZombie>();
            if (suicideBomberZombie != null)
            {
                if (suicideBomberZombie.GetCurrentHP() > 0f)
                {
                    suicideBomberZombie.TakeDamage(dmg);
                }
            }

            RunnerZombie runnerZombie = other.gameObject.GetComponent<RunnerZombie>();
            if (runnerZombie != null)
            {
                if (runnerZombie.GetCurrentHP() > 0f)
                {
                    runnerZombie.TakeDamage(dmg);
                }
            }

            BossZombie bossZombie = other.gameObject.GetComponent<BossZombie>();
            if (bossZombie != null)
            {
                if (bossZombie.GetCurrentHP() > 0f)
                {
                    bossZombie.TakeDamage(dmg);
                }
            }

            //RegularZombie regularZombie = other.gameObject.GetComponent<RegularZombie>();
            //if (regularZombie != null)
            //{
            //    regularZombie.TakeDamage(dmg);
            //}

            //SuicideBomberZombie suicideBomberZombie = other.gameObject.GetComponent<SuicideBomberZombie>();
            //if (suicideBomberZombie != null)
            //{
            //    suicideBomberZombie.TakeDamage(dmg);
            //}

            //RunnerZombie runnerZombie = other.gameObject.GetComponent<RunnerZombie>();
            //if (runnerZombie != null)
            //{
            //    runnerZombie.TakeDamage(dmg);
            //}

            //BossZombie bossZombie = other.gameObject.GetComponent<BossZombie>();
            //if (bossZombie != null)
            //{
            //    bossZombie.TakeDamage(dmg);
            //}
            Destroy(this.gameObject);
        }
    }
}
