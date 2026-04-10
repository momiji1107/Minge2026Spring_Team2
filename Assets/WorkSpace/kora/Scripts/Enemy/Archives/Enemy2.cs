using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyBase
{
    [Header("弾の設定")]
    [Header("Prefab")][SerializeField] private GameObject projectile;
    
    [Header("速度")][SerializeField] private float projSpeed = 1.5f;
    [Header("発射間隔")][SerializeField] private float projRate = 1.5f;
    [Header("弾が消えるまでの時間")][SerializeField] private float projLifeTime = 20f;
    
    private float _rateTimer = 0f;
    
    protected override void OnActive()
    {
        ShotTimer();
    }

    private void ShotTimer()
    {
        _rateTimer += Time.deltaTime;

        if (_rateTimer >= projRate)
        {
            _rateTimer -= projRate;
            Shot(); 
        }
    }

    private void Shot()
    {
        if (projectile == null) return;
        
        var obj = Instantiate(projectile, transform.position, Quaternion.identity);
        //obj.GetComponent<EnemyProjectile>().Init(Vector3.left, projSpeed, projLifeTime);
    }
}
