using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Base Alien class for all concrete Alien types to inherit from 
 */
public interface Alien
{
    void TakeDamage(float dmg);
    void Attack();

    GameObject GetGameObject();
}
