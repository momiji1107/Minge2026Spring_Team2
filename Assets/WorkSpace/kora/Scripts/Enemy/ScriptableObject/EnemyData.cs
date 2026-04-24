using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    [Tooltip("最大Hp")]public int maxHp;
    [Tooltip("経験値")] public int exp;
    [Tooltip("スコア")]public int score;

    [Tooltip("Bossかどうか")]
    public bool isBoss;

    [Header("damage時の点滅")]
    [Tooltip("点滅の間隔")]public float interval = 0.1f;

    [Tooltip("点滅時間")]public float time = 1f;
}

public abstract class EnemyBehaviourSO : ScriptableObject
{
    public abstract EnemyBehaviourBase Create();
}