using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyPicker : MonoBehaviour
{
    public float GetDifficultyMultiplier()
    {
        switch (StaticData.valueToKeep)
        {
            case 0: return 0.5f;  // 50% damage
            case 1: return 1f; // 100% damage
            case 2: return 2f;   // 200% damage
            default: return 1.0f; // Fallback
        }
    }
}