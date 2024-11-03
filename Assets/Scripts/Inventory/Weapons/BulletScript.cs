using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed; // speed of bullet
    public float maxDistance; // max dist before bullet is deleted
    private Vector3 dir;
    private float dmg;

    void Start()
    {
        float yRotation = Random.Range(-1, 1);

        Quaternion bulletRotation = Quaternion.Euler(0, yRotation, 0);
        dir = bulletRotation * transform.forward;

        transform.localRotation *= Quaternion.Euler(90, 0, 0);
        dmg = 25f; // Damage should be 18f
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        maxDistance += Time.deltaTime;

        if (maxDistance >= 0.9f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.Play("Alien Hurt");
            // Updated dmg given
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
