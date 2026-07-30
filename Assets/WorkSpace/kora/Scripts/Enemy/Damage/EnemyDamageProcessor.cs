using System;
using UnityEngine;

public interface IDamageProcessor
{
    public void Init(EnemyCore core);
    public void Tick();
    public void ReceiveDamage(float damage);
}

public class EnemyDamageProcessor : IDamageProcessor
{
    private EnemyCore _core;
    
    public void Init(EnemyCore core)
    {
        _core = core;
    }
    
    public void Tick(){}

    public void ReceiveDamage(float damage)
    {
        _core.TakeDamage(damage);
    }
}

public class BossDamageProcessor : IDamageProcessor
{
    private EnemyCore _core;
    private BossData _data;
    
    private bool _isBuffered = false;
    private float _bufferedDamage = 0;
    private bool _isTookCore; 

    public void Init(EnemyCore core)
    {
        _core = core;
    }

    public void InitData(BossData data)
    {
        _data = data;
    }
    
    public void Tick()
    {
        if (!_isBuffered) return;
        
        _core.TakeDamage(_bufferedDamage);

        _bufferedDamage = 0;
        _isBuffered = false;
        _isTookCore = false;
    }

    public void ReceiveDamage(float damage) { Debug.Log("Set Normal EnemyHitBox on Boss.\n please set BossHitBox");}

    public void ReceiveDamage(float damage, BossPartType type)
    {
        _isBuffered = true;

        if (_isTookCore) return;
        if (type == BossPartType.Core)
        {
            _bufferedDamage = damage;
            _isTookCore = true;
            return;
        }
        
        foreach (var t in _data.partDatas)
        {
            if (t.type != type) continue;
            
            //Debug.Log("type multiplier is " + t.multiplier);
            damage = (float)Math.Ceiling(damage * t.multiplier);
            break;
        }
        
        //Debug.Log("previous damage: " + _bufferedDamage + ", input damage: " + damage);
        if (_bufferedDamage < damage) _bufferedDamage = damage;
    }
}