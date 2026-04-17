using System;
using UnityEngine;

[Serializable]
public class EnemyContactEfectParam
{
    public bool isDamageable = true;
    public bool isDestroy = false;
    public int damage = 1;
}

public class EnemyContactEffect : EnemyBehaviourBase
{
    private Collider2D _damageCollider;
    
    private bool _isDamageable = true;
    private bool _isDestroy = false;
    private int _damage = 1;
    
    //readonly string PlayerTag = "Player";

    public override void OnInit()
    {
        var param = Data.contactEffect;
        _isDamageable = param.isDamageable;
        _isDestroy = param.isDestroy;
        _damage = param.damage;
        _damageCollider = Context.damageCollider;
    }
    
    public override void OnHitPlayer(Collider2D other)
    {
        if (!_damageCollider.IsTouching(other)) return;
        
        if (Core.GetIsDead()) return;
        
        if (_isDamageable)
        {
            Debug.Log("EnemyContactEffect: " + other.name +"に" + _damage + "ダメージを与える");
            // playerにダメージを与える処理を追記
            other.gameObject.GetComponentInChildren<PlayerModel>().Damage(_damage);
        }

        if (_isDestroy)
        {
            Context.Destroy();
        }
    }
}
