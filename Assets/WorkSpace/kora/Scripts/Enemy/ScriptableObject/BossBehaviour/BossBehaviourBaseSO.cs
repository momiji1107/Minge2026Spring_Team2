using UnityEngine;


public abstract class BossBehaviourBaseSO : ScriptableObject
{
    protected bool IsRight;
    protected Vector3 Direction = Vector3.left;

    protected NormalEnemyData Data;
    protected EnemyCore Core;
    protected EnemyContext Context;

    public virtual void Init(EnemyCore core, EnemyContext context)
    {
        Core = core;
        Context = context;
        
        OnInit();
    }
    public virtual void Tick(float dt){}
    public virtual void OnHitPlayer(Collider2D other){}

    public void SetIsRight(bool isRight)
    {
        this.IsRight = isRight;
        Direction = InitDirection(Direction);
        OnSetIsRight();
    }
    
    protected virtual void OnInit(){}
    protected virtual void OnSetIsRight(){}
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