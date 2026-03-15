using System;
using UnityEngine;

/// <summary>
/// Enemyの特性を追加するときは、基本的にこれを継承
/// </summary>
public abstract class EnemyBehaviourBase : MonoBehaviour
{
    /// <summary>
    /// EnemyControllerで制御しているUpdate処理
    /// </summary>
    public virtual void Tick(float deltaTime){}
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