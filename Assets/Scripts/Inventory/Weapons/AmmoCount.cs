﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<TextMeshProUGUI>();
        txt.text = "" + WeaponInfo.ammo + " / " + WeaponInfo.MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "" + WeaponInfo.ammo + " / " + WeaponInfo.MaxAmmo;
    }
}
