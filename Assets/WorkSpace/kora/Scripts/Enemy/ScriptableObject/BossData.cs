using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/Enemy/BossData")]
public class BossData : EnemyData
{
    [Tooltip("ボスのフェーズ\nHpで変化する")]public List<Phase> phases;
    public List<BossPartData> partDatas;
    
    [Serializable]
    public class Phase
    {
        [Tooltip("このフェーズが始まるHp残量の割合\n百分率で設定する")] public int hpPercent;
        [Tooltip("このフェーズの行動(特性)")]public List<BossBehaviourBaseSO> behaviours;
    }

    [Serializable]
    public class BossPartData
    {
        public BossPartType type = BossPartType.Core;
        public float multiplier = 1.0f;
    }

    
}

public enum BossPartType
{
    Core,
    Body
}