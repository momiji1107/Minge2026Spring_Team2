using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOzyamaView : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private Animator animator;
    [SerializeField] private float waitTime = 1f;
    [SerializeField, Range(0f,1f)] private float fadeSpeed = 0.2f;
    [SerializeField] private float fadeDuration = 5f;
    [SerializeField, Range(0f,1f)] private float destroyAlpha = 0.8f;

    public void StartFadeOut()
    {
        //Debug.Log("StartFadeOut");
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = -waitTime;
        var alpha = panel.color.a;
        while (alpha >= destroyAlpha || timer < fadeDuration)
        {
            timer += Time.deltaTime;
            if (timer < 0)
            {
                yield return null;
                continue;
            }
            alpha -= fadeSpeed * Time.deltaTime;
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
