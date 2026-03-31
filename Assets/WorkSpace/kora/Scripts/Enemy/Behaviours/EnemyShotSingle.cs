using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShotSingle : EnemyAttackBehaviourBase
{
    [Header("弾の設定")]
    [Header("Prefab")][SerializeField] private GameObject projectile;
    
    [Header("発射間隔")][SerializeField] private float shotRate = 1.5f;
    [Header("発射方向")][SerializeField] private Vector3 direction = Vector3.left;
    [Header("発射位置")] [SerializeField] private GameObject shotPosition;
    [Header("最初の詠唱開始までの時間")][SerializeField] private float startTime = 0f;

    public float ShotRate => shotRate;

    private Vector3 _shotPos;
    private float _waitTimer = 0f;
    private float _rateTimer = 0f;
    
    private bool _isFirst = false;
    private bool _isNullShotPosition = false;
    
    void Awake()
    {
        startTime += shotRate;
        Direction = direction;
        
        if (shotPosition == null) _isNullShotPosition = true;
    }
    
    // Update
    public override void Tick(float dt)
    {
        if (_waitTimer < startTime) _waitTimer += dt;
        else Shooter(dt);
    }
    
    private void Shooter(float dt)
    {
        if (_isFirst == false)
        {
            _isFirst = true;
            ActiveAttackAnim.Invoke();
        }
        
        _rateTimer += dt;

        
        
        if (!_isNullShotPosition)
        {
            _shotPos = shotPosition.transform.position;
        }
        else _shotPos = transform.position;

        if (_rateTimer >= shotRate)
        {
            _rateTimer = 0f;
            Shot();
        }
    }

    private void Shot()
    {
        if (projectile == null) return;
     
        var obj = Instantiate(projectile, _shotPos, Quaternion.identity);
        var proj = obj.GetComponent<EnemyProjectile>();
        proj.Init(Direction);
    }

    protected override void OnSetIsRight()
    {
    }
}
