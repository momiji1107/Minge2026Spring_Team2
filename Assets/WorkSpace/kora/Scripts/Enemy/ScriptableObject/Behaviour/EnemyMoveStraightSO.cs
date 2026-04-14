using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMoveStraightSO", menuName = "ScriptableObjects/Enemy/BehaviourSO/MoveStraightSO")]
public class EnemyMoveStraightSO : EnemyBehaviourSO
{
    public override EnemyBehaviourBase Create()
    {
        return new EnemyMoveStraight();
    }
}