using System;
using UnityEngine;

[Serializable]
public class EnemyMoveRoundParam
{
    public float speed = 3f;
    public float distance = 5f;
    public float startDistance = 4f;
    public bool isMoveRight = false;
}

public class EnemyMoveRound : EnemyBehaviourBase
{
    private float _speed = 3f;
    private float _distance = 5f;
    private float _startDistance = 4f;
    private bool _isMoveRight = false;

    private float _currentDistance;
    private Vector3 _direction;
    
    public override void OnInit()
    {
        var param = Data.moveRound;
        _speed = param.speed;
        _distance = param.distance;
        _startDistance = param.startDistance;
        _isMoveRight = param.isMoveRight;
        
        if (_startDistance > _distance) _startDistance = _distance;
        
        _currentDistance = _startDistance;
        _direction = _isMoveRight ? Vector3.right : Vector3.left;
        
        Direction = _direction;
    }
    
    public override void Tick(float dt)
    {
        _direction = Direction;
        MoveRound(dt);
    }

    private void MoveRound(float dt)
    {
        var d = _direction * (_speed * dt);
        Context.Move(d);
        _currentDistance += d.x;
        
        if (_currentDistance < 0f)
        {
            _currentDistance = 0f;
            _direction.x *= -1;
        }
        else if (_currentDistance > _distance)
        {
            _currentDistance = _distance;
            _direction.x *= -1;
        }
    }
}
