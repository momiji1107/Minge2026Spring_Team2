using UnityEngine;


[CreateAssetMenu(fileName = "EnemyContactEffectSO", menuName = "ScriptableObjects/Enemy/BehaviourSO/ContactEffectSO")]
public class EnemyContactEffectSO : EnemyBehaviourSO
{
    public override EnemyBehaviourBase Create()
    {
        return new EnemyContactEffect();
    }
}
