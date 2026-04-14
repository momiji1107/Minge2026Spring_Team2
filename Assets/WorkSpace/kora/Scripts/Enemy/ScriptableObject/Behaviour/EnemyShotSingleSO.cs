using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShotSingleSO", menuName = "ScriptableObjects/Enemy/BehaviourSO/ShotSingleSO")]
public class EnemyShotSingleSO : EnemyBehaviourSO
{
    public override EnemyBehaviourBase Create()
    {
        return new EnemyShotSingle();
    }
}
