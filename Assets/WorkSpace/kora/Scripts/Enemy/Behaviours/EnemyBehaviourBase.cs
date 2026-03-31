using System;
using UnityEngine;

/// <summary>
/// Enemyの特性を追加するときは、基本的にこれを継承
/// </summary>
public abstract class EnemyBehaviourBase : MonoBehaviour
{
    protected bool IsRight;
    protected Vector3 Direction = Vector3.left;
    
    protected Vector3 InitDirection(Vector3 dir)
    {
        dir.Normalize();
        if (IsRight)
        {
            dir.x = -dir.x;
        }

        return dir;
    }
    
    /// <summary>
    /// EnemyControllerで制御しているUpdate処理
    /// </summary>
    public virtual void Tick(float deltaTime){}
    
    public void SetIsRight(bool isRight)
    {
        this.IsRight = isRight;
        Direction = InitDirection(Direction);
        Debug.Log("Direction: " + Direction);
        OnSetIsRight();
    }

    protected virtual void OnSetIsRight() {}
}

/// <summary>
/// Attack処理のBehaviourはこれを継承
/// </summary>
public abstract class EnemyAttackBehaviourBase : EnemyBehaviourBase
{
    public Action ActiveAttackAnim;
    public Action DisActiveAttackAnim;
}

/// <summary>
/// Move処理のBehaviourはこれを継承
/// </summary>
public abstract class EnemyMoveBehaviourBase : EnemyBehaviourBase
{
    public Action ActiveMoveAnim;
    public Action DisActiveMoveAnim;
}