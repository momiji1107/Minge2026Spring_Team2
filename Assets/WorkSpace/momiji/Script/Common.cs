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
}



