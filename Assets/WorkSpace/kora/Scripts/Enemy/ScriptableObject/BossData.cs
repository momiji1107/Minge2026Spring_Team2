using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/Enemy/BossData")]
public class BossData : EnemyData
{
    public List<Phase> phases;
    
    [Serializable]
    public class Phase
    {
        public int hpPercent;
        public List<BossBehaviourBaseSO> behaviours;
    }
}