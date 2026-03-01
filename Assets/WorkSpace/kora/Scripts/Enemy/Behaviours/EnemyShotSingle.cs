using Unity.VisualScripting;
using UnityEngine;

public class EnemyShotSingle : EnemyBehaviourBase
{
    [Header("弾の設定")]
    [Header("Prefab")][SerializeField] private GameObject projectile;
    
    [Header("発射間隔")][SerializeField] private float projRate = 1.5f;
    [Header("発射方向")][SerializeField] private Vector3 direction = Vector3.left;
    
    private float _rateTimer = 0f;

    public void Tick(float dt)
    {
        Shooter(dt);
    }
    
    private void Shooter(float dt)
    {
        _rateTimer += dt;

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
        obj.GetComponent<EnemyProjectile>().Init(direction);
    }
}
