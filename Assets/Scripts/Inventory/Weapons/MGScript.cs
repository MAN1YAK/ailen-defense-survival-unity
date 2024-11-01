﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGScript : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public float wT;
    public GameObject bullet;
    private PlayerMove m_playerMove;

    // Start is called before the first frame update
    void Start()
    {
        wT = waitTime;
        m_playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        // Shooting - G
        if (Input.GetMouseButton(0) && WeaponInfo.ammo > 0 && wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            Shoot();
            AudioManager.instance.Play("Minigun");
            WeaponInfo.ammo--;
            wT = waitTime;
        }

        if (Input.GetMouseButtonDown(0) && WeaponInfo.ammo <= 0)
        {
            AudioManager.instance.Play("EmptyGun");
        }
#endif

        wT -= 1 * Time.deltaTime;
    }

    // Shooting function - G
    public void Shoot()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                switch (Input.GetTouch(i).phase)
                {
                    case TouchPhase.Moved:
                        {
                            if (wT <= 0 && WeaponInfo.ammo > 0 && wT <= 0 && !WeaponInfo.reloadAffirm)
                            {
                                Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                                WeaponInfo.ammo--;
                                wT = waitTime;
                                AudioManager.instance.Play("Minigun");
                            }

                            if (WeaponInfo.ammo <= 0)
                            {
                                AudioManager.instance.Play("EmptyGun");
                            }

                            break;
                        }
                }
            }
        }
#endif

#if UNITY_STANDALONE
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
#endif
    }
}
