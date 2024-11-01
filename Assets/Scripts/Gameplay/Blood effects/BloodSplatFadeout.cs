using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatFadeout : MonoBehaviour
{
    
    private float fadeOutTimeMin = 20f;
    private float fadeOutTimeMax = 30f;

    private float m_fadeOutTime;

    private void Start()
    {
        m_fadeOutTime = Random.Range( fadeOutTimeMin, fadeOutTimeMax );
        StartCoroutine( this.BloodSplatFadeOut(m_fadeOutTime) );
    }

    private IEnumerator BloodSplatFadeOut(float _fadeOutTime)
    {
        float timeElapsed = 0f;
        Color originalColor = GetComponent<Renderer>().material.color;
        float originalAlpha = originalColor.a;
        float targetAlpha = 0f;

        while (timeElapsed < _fadeOutTime)
        {
            timeElapsed += Time.deltaTime;

            float ratio = timeElapsed / _fadeOutTime;
            float newAlpha = Mathf.Lerp(originalAlpha, targetAlpha, ratio);
            Color newColor = originalColor;  // Use the original color to avoid altering it directly.
            newColor.a = newAlpha;
            GetComponent<Renderer>().material.color = newColor;

            if (ratio >= 0.95f)
            {
                newColor.a = targetAlpha; // Ensure alpha reaches 0
                GetComponent<Renderer>().material.color = newColor;
                break;
            }

            yield return null;
        }
        Destroy(gameObject);
    }
}
