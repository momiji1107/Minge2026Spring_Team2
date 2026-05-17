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
        IsRight = false;
        
        OnInit();
    }
    public virtual void Tick(float dt){}
    public virtual void OnHitPlayer(Collider2D other){}

    public void SetIsRight(bool isRight)
    {
        //Debug.Log("Invoked SetIsRight=" + isRight);
        if (this.IsRight != isRight) Direction = InverseDirection(Direction);
        this.IsRight = isRight;
        OnSetIsRight();
    }
    
    protected virtual void OnInit(){}
    protected virtual void OnSetIsRight(){}
    protected Vector3 InverseDirection(Vector3 dir)
    {
        dir.x = -dir.x;
        dir.Normalize(); 

        return dir;
    }
    
    /// <summary>
    /// Cameraでのx位置をWorld座標のxで返す
    /// 左端~右端で0~1の値を入れる
    /// </summary>
    protected float GetXOnCameraToWorldPoint(float xRatio)
    {
        Vector3 viewport = new Vector3(xRatio, 0, 0);
        Vector3 world = Camera.main.ViewportToWorldPoint(viewport);
        return world.x;
    }

    protected float GetXWorldToCameraPoint(float worldX)
    {
        Vector3 posX = new Vector3(worldX, 0, 0);
        Vector3 viewport = Camera.main.WorldToViewportPoint(posX);
        return viewport.x;
    }
}