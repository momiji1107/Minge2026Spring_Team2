using UnityEngine;

[CreateAssetMenu(fileName = "BossShotSingle", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossShotSingle")]
public class BossShotSingle : BossBehaviourBaseSO
{
    [Header("弾の設定")]
    [Header("Prefab")]public GameObject projectile;
    
    [Header("発射間隔")]public float shotRate = 1.5f;
    [Header("発射方向")]public Vector3 direction = Vector3.left;
    [Header("最初の詠唱開始までの時間")]public float startTime = 0f;
    
    private GameObject shotPoint;

    private Vector3 _shotPos;
    private float _waitTimer = 0f;
    private float _rateTimer = 0f;
    
    private bool _isFirst = false;
    private bool _isNullShotPosition = false;
    
    protected override void OnInit()
    {
        //Debug.Log("Init shot");
        
        startTime += shotRate;
        Direction = direction;
        
        if (shotPoint == null) _isNullShotPosition = true;
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
        }
        
        _rateTimer += dt;
        
        if (!_isNullShotPosition)
        {
            _shotPos = shotPoint.transform.position;
        }
        else _shotPos = Context.Transform.position;

        if (_rateTimer >= shotRate)
        {
            _rateTimer = 0f;
            Shot();
        }
    }

    private void Shot()
    {
        if (projectile == null) return;
     
        //Debug.Log("Shot");
        var obj = Context.Instantiate(projectile, _shotPos);
        var proj = obj.GetComponent<EnemyProjectile>();
        proj.Init(Direction);
    }

    protected override void OnSetIsRight()
    {
    }
}
