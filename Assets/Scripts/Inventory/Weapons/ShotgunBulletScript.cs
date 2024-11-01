using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        float yRotation = Random.Range(-50, 50);
        float xRotation = Random.Range(-4, 4);

        Quaternion bulletRotatio = Quaternion.Euler(xRotation, yRotation, 0);
        dir = bulletRotatio * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 10f; // Damage should be 10f
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 0.1f) // dist should be 0.1f
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
            RegularAlien regularAlien = other.gameObject.GetComponent<RegularAlien>();
            if (regularAlien != null)
            {
                if (regularAlien.GetCurrentHP() > 0f)
                {
                    regularAlien.TakeDamage(dmg);
                }
            }

            RunnerAlien runnerAlien = other.gameObject.GetComponent<RunnerAlien>();
            if (runnerAlien != null)
            {
                if (runnerAlien.GetCurrentHP() > 0f)
                {
                    runnerAlien.TakeDamage(dmg);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
