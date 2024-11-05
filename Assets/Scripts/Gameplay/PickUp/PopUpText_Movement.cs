using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText_Movement : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public static int pickUp = 0;
    private float del = 3f;
    private float MaxTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        if (pickUp == 1)
        {
            textMesh.text = "Ammo picked up!";
        }
        else if (pickUp == 2)
        {
            textMesh.text = "Health picked up!";
        }
        else if (pickUp == 3)
        {
            textMesh.text = "ShotGun Picked Up!";
        }
        else if (pickUp == 5)
        {
            textMesh.text = "MiniGun Picked Up!";
        }
        else if (pickUp == 6)
        {
            textMesh.text = "Shotgun Spawned. Find it!";
        }
        else if (pickUp == 7)
        {
            textMesh.text = "Minigun Spawned. Find it!";
        }
        else
        {
            textMesh.text = "_";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PopUpText_Spawn.showNum >= 2)
        {
            MaxTime = 0.5f;
        }
        else
        {
            MaxTime = 1.5f;
        }
        LeanTween.moveLocalY(gameObject, -400f, MaxTime);
        if (del <= 0)
        {
            Destroy(gameObject);
            PopUpText_Spawn.showNum--;
        }
        del -= Time.deltaTime;
        if (PopUpText_Spawn.showNum >= 2)
        {
            del -= Time.deltaTime;
        }
    }
}
