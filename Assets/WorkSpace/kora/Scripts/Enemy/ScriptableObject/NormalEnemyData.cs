using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalEnemyData", menuName = "ScriptableObjects/Enemy/NormalEnemyData")]
public class NormalEnemyData : EnemyData
{
    public List<EnemyBehaviourSO> behaviours;

    public EnemyContactEfectParam contactEffect;
    public EnemyMoveRoundParam moveRound;
    public EnemyMoveStraightParam moveStraight;
    public EnemyShotSingleParam shotSingle;
}