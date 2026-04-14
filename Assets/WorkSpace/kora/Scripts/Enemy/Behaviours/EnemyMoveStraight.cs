using System;
using UnityEngine;

[Serializable]
public class EnemyMoveStraightParam
{
    [Header("移動速度")] public float speed = 5f;
    [Header("進行方向")] public Vector3 direction = Vector3.left;
}

public class EnemyMoveStraight : EnemyBehaviourBase
{
    private float _speed = 5f;
    private Vector3 _direction = Vector3.left;

    private bool _isFirst = true;

    public override void OnInit()
    {
        var param = Data.moveStraight;
        _speed = param.speed;
        _direction = param.direction;
        
        base.Direction = _direction;
    }
    
    // Update
    public override void Tick(float dt)
    {
        if (_isFirst)
        {
            _isFirst = false;
            Core.InvokeMoveAnim();
        }
        
        CheckVisible();
        
        Move(dt);
    }

    private void Move(float dt)
    {
        var delta = Direction * (_speed * dt);
        Context.Move(delta);
    }
    
    private void CheckVisible()
    {
        var pos = Camera.main.WorldToViewportPoint(Context.Transform.position);
        if (pos.x < 0 || 1 < pos.x || pos.y < 0 || 1 < pos.y)
        {
            Debug.Log("Out of Camera and Destroy:" + Context.GameObject.name);
            Context.Destroy();
        }
    }
}
