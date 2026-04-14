using System;
using UnityEngine;

/// <summary>
/// Enemyの特性を追加するときは、基本的にこれを継承
/// </summary>
[System.Serializable]
public abstract class EnemyBehaviourBase
{
    protected bool IsRight;
    protected Vector3 Direction = Vector3.left;

    protected NormalEnemyData Data;
    protected EnemyCore Core;
    protected EnemyContext Context;

    public void Init(NormalEnemyData data, EnemyCore core, EnemyContext context)
    {
        Data = data;
        Core = core;
        Context = context;
        
        OnInit();
    }

    public abstract void OnInit();
    
    /// <summary>
    /// EnemyControllerで制御しているUpdate処理
    /// </summary>
    public virtual void Tick(float deltaTime){}
    
    public virtual void OnHitPlayer(Collider2D other){}
    
    public void SetIsRight(bool isRight)
    {
        this.IsRight = isRight;
        Direction = InitDirection(Direction);
        OnSetIsRight();
    }

    protected virtual void OnSetIsRight() {}

    protected Vector3 InitDirection(Vector3 dir)
    {
        dir.Normalize();
        if (IsRight)
        {
            dir.x = -dir.x;
        }

        return dir;
    }
}