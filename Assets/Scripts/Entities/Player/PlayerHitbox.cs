using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private PlayerInfo m_playerInfo;

    private void Start()
    {
        m_playerInfo = transform.root.gameObject.GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
