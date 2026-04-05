using System;
using System.Collections;
using UnityEngine;

public static class Tags
{
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string EnemyProjectile = "EnemyProjectile";
}

public class Common : MonoBehaviour
{
    public static IEnumerator DelayCall(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    public static IEnumerator BlinkColor(SpriteRenderer sr, float interval, float time)
    {
        Color32 white = new Color32(255, 255, 255, 255);
        Color32 red = new Color32(255, 130, 130, 255);
        
        float timer = 0;
        while (timer < time)
        {
            Debug.Log("Change color");
            sr.color = sr.color == white? red : white;
            timer += interval;
            yield return new WaitForSeconds(interval);
        }
    }
}



