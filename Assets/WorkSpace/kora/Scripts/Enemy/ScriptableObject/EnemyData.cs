using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    public int maxHp;
    public int exp;
    public int score;

    [Header("Bossかどうか")]
    public bool isBoss;

    [Header("damage時の点滅")]
    public float interval = 0.1f;

    public float time = 1f;
}

public abstract class EnemyBehaviourSO : ScriptableObject
{
    public abstract EnemyBehaviourBase Create();
}