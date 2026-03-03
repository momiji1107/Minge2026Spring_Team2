using UnityEngine;

public abstract class EnemyBehaviourBase : MonoBehaviour
{
    /// <summary>
    /// EnemyControllerで制御しているUpdate処理
    /// </summary>
    public virtual void Tick(float deltaTime){}
}
