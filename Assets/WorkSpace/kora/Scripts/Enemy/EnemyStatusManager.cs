using System.Collections;
using UnityEngine;

public class EnemyStatusManager
{
    private EnemyController _controller;
    
    private bool _isStun;
    private bool _isSlow;
    private bool _isSpawnMove;
    
    private float _stunTimer;
    private float _slowTimer;
    private float _spawnMoveTimer;

    private float _stunTime;
    private float _slowTime;
    private float _spawnMoveTime;
    
    private Vector3 _spawnPosition;
    private Vector3 _spawnMoveDelta;

    private float _slowPer;

    public bool IsStun => _isStun;
    public bool IsSlow => _isSlow;
    public bool IsSpawnMove => _isSpawnMove;

    public float SlowPer => _slowPer;
    
    public void Init(EnemyController controller)
    {
        _controller = controller;
    }

    public void Tick()
    {
        if (_isStun) RunStun();
        
        if (_isSlow) RunSlow();
        
        if (_isSpawnMove) RunSpawnMove();
    }
    
    public void Slow(float time, float per)
    {
        if (per >= 100f || time == 0) return;
        
        _isSlow = true;
        _slowPer = (100f - per) / 100f;
        _slowTimer = 0f;
        _slowTime = time;
    }
    
    public void Stun(float time)
    {
        if (time == 0) return;

        _stunTimer = 0f;
        _isStun = true;
        _stunTime = time;
    }

    public void SpawnMove(float time, Vector3 vector)
    {
        //Local座標からworld座標に変換
        var targetPos = _controller.Context.Transform.position + vector;
        //Debug.Log(targetPos);

        _spawnPosition = targetPos;
        _spawnMoveTimer = 0f;
        
        if (time == 0)
        {
            _controller.Context.SetPosition(_spawnPosition);
            return;
        }

        _spawnMoveTime = time;
        _isSpawnMove = true;
        
        var currentPos = _controller.Context.Transform.position;
        var direction = (_spawnPosition - currentPos).normalized;
        float speed = Vector3.Distance(currentPos, _spawnPosition) / _spawnMoveTime;
        
        _spawnMoveDelta = direction * speed;
    }
    
    private void RunSlow()
    {
        //Debug.Log("Slow");
        if (_slowTimer < _slowTime)
        {
            _slowTimer += Time.deltaTime;
        }
        else _isSlow = false;
    }
    
    private void RunStun()
    {
        //Debug.Log("Stun");
        if (_stunTimer < _stunTime)
        {
            _stunTimer += Time.deltaTime;
        }
        else _isStun = false;
    }

    private void RunSpawnMove()
    {
        if (_spawnMoveTimer < _spawnMoveTime)
        {
            _spawnMoveTimer += Time.deltaTime;
            
            var delta = _spawnMoveDelta * Time.deltaTime;
            _controller.Context.Move(delta);
        }
        else _isSpawnMove = false;
    }
}
