using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponNum;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (weaponNum == 1)
            {
                WeaponInfo.SGAccess = true;
                Destroy(this.gameObject);
                PopUpText_Movement.pickUp = 3;
                PopUpText_Spawn.show = true;
            }
            else if (weaponNum == 2)
            {
                WeaponInfo.MGAccess = true;
                Destroy(this.gameObject);
                PopUpText_Movement.pickUp = 5;
                PopUpText_Spawn.show = true;
            }
            AudioManager.instance.Play("PickUpSound_Weapons");
        }
    }
}
