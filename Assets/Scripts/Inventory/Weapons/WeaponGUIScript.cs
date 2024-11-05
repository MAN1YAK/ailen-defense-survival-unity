using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGUIScript : MonoBehaviour
{
    public static WeaponGUIScript instance;

    public GameObject pistolGO;
    public GameObject shotgunGO;
    public GameObject MGGO;

    public GameObject TptI; // Translucent Pistol Image
    public GameObject TsgI; // Translucent Shotgun Image
    public GameObject TmgI; // Translucent MiniGun Image
    public GameObject RI; // Reloading Image
    public GameObject Ifin; // Infinity Image

    public float time;
    public bool t;
    public float waitTime = 0.05f;
    public float wT = 0.05f;
    private bool isReloading = false; // track reloading state

    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        TptI.SetActive(true);
        TsgI.SetActive(false);
        TmgI.SetActive(false);
        RI.SetActive(false);
        Ifin.SetActive(true);

        time = 3f;
        t = false;

        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pistolGO.activeSelf)
        {
            TptI.SetActive(true);
            TsgI.SetActive(false);
            TmgI.SetActive(false);
            Ifin.SetActive(true);
        }
        else if (shotgunGO.activeSelf)
        {
            TptI.SetActive(false);
            TsgI.SetActive(true);
            TmgI.SetActive(false);
            Ifin.SetActive(false);
        }
        else if (MGGO.activeSelf)
        {
            TptI.SetActive(false);
            TsgI.SetActive(false);
            TmgI.SetActive(true);
            Ifin.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && WeaponInfo.ammo < 100 && WeaponInfo.MaxAmmo > 0)
        {
            if (!isReloading)
            {
                // Start reloading
                m_AudioSource.loop = true;
                m_AudioSource.Play();
                Reload();
                isReloading = true;
            }
            else
            {
                // Stop reloading
                WeaponInfo.reloadAffirm = false;
                RI.SetActive(false);
                m_AudioSource.Stop();
                isReloading = false;
            }
        }

        if (WeaponInfo.ammo <= 0)
        {
            WeaponInfo.ammo = 0;
        }

        if (WeaponInfo.reloadAffirm) // Reload function
        {
            if (wT <= 0)
            {
                WeaponInfo.ammo++;
                WeaponInfo.MaxAmmo--;
                wT = waitTime;
            }
            wT -= 1 * Time.deltaTime;
            RI.SetActive(true);

            if (WeaponInfo.ammo >= 100)
            {
                WeaponInfo.ammo = 100;
                EndReload();
            }
            else if (WeaponInfo.MaxAmmo <= 0)
            {
                EndReload();
            }
        }
    }

    public void Reload()
    {
        WeaponInfo.reloadAffirm = true;
    }

    private void EndReload()
    {
        RI.SetActive(false);
        WeaponInfo.reloadAffirm = false;
        m_AudioSource.Stop();
        isReloading = false;
    }
}
