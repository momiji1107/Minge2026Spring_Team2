using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMoveRoundSO", menuName = "ScriptableObjects/Enemy/BehaviourSO/MoveRoundSO")]
public class EnemyMoveRoundSO : EnemyBehaviourSO
{
    public override EnemyBehaviourBase Create()
    {
        return new EnemyMoveRound();
    }
}