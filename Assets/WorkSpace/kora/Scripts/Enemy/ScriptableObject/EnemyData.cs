using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    public int maxHp;
    public int exp;
    public int score;

    public bool isBoss;
}

public abstract class EnemyBehaviourSO : ScriptableObject
{
    public abstract EnemyBehaviourBase Create();
}