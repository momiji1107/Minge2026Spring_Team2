using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EnemyShotSingleParam
{
    [Header("弾の設定")]
    [Header("Prefab")]public GameObject projectile;
    
    [Header("発射間隔")]public float shotRate = 1.5f;
    [Header("発射方向")]public Vector3 direction = Vector3.left;
    [Header("最初の詠唱開始までの時間")]public float startTime = 0f;
}

public class EnemyShotSingle : EnemyBehaviourBase
{
    private GameObject _projectile;
    
    private float _shotRate = 1.5f;
    private Vector3 _direction = Vector3.left;
    private GameObject _shotPoint;
    private float _startTime = 0f;

    public float ShotRate => _shotRate;

    private Vector3 _shotPos;
    private float _waitTimer = 0f;
    private float _rateTimer = 0f;
    
    private bool _isFirst = false;
    private bool _isNullShotPosition = false;
    
    public override void OnInit()
    {
        //Debug.Log("Init shot: " + Data.shotSingle.shotRate);
        var param = Data.shotSingle;
        _projectile = param.projectile;
        _shotRate = param.shotRate;
        _direction = param.direction;
        _startTime = param.startTime;
        _shotPoint = Context.shotPoint;
        
        _startTime += _shotRate;
        Direction = _direction;
        
        if (_shotPoint == null) _isNullShotPosition = true;
    }
    
    // Update
    public override void Tick(float dt)
    {
        if (_waitTimer < _startTime) _waitTimer += dt;
        else Shooter(dt);
    }
    
    private void Shooter(float dt)
    {
        if (_isFirst == false)
        {
            _isFirst = true;
            Core.InvokeAttackAnim();
        }
        
        _rateTimer += dt;
        
        //shotPointがnullなら、enemyの中心から弾を生成する
        if (!_isNullShotPosition)
        {
            _shotPos = _shotPoint.transform.position;
        }
        else _shotPos = Context.Transform.position;

        if (_rateTimer >= _shotRate)
        {
            _rateTimer = 0f;
            Shot();
        }
    }

    private void Shot()
    {
        if (_projectile == null) return;
     
        var obj = Context.Instantiate(_projectile, _shotPos);
        var proj = obj.GetComponent<EnemyProjectile>();
        proj.Init(Direction);
    }

    protected override void OnSetIsRight()
    {
    }
}
