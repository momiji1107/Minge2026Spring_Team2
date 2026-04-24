using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossShot", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossShot")]
public class BossShot : BossBehaviourBaseSO
{
    public enum ShotType
    {
        [Tooltip("直線に一つずつ発射する")] Single,
        [Tooltip("縦と斜めに三つずつ発射する")] Triple
    }

    [Tooltip("発射するレーン\n" +
             "配列の上から順に発射する")] [SerializeField, Range(0,4)]
    public int[] laneIndex = null;

    [Tooltip("発射位置をレーンから浮かせる距離")] [SerializeField, Range(0,2)]
    private float upToLane = 1f;
    
    [Tooltip("位置の調整")] [SerializeField]
    private Vector3 adjustPosition = Vector3.zero;

    [Tooltip("弾のPrefab")] [SerializeField]
    private GameObject projectile;

    [Tooltip("発射タイプ")] [SerializeField]
    private ShotType shotType = ShotType.Single;

    [Tooltip("発射間隔")] [SerializeField]
    private float shotRate = 1.5f;

    [Tooltip("発射方向")] [SerializeField]
    private Vector3 direction = Vector3.left;

    [Tooltip("最初の詠唱開始までの時間")] [SerializeField]
    private float startTime = 0f;
    
    [Tooltip("x座標を真ん中にする")] [SerializeField]
    private bool isSetCenter = false;

    [SerializeField] private bool isForecast = false;
    [SerializeField] private bool isSetSenterForecast = false;
    [SerializeField] private GameObject forecastObject;
    [SerializeField] private float forecastTime = 3f;

    private int _index;
    private Vector3 _shotPos;
    private float _waitTimer = 0f;
    private float _rateTimer = 0f;

    private bool _isFirst = false;
    private bool _isNullShotPosition = false;
    private bool _isForecast = false;

    protected override void OnInit()
    {
        //Debug.Log("Init shot");
        _index = 0;
        _shotPos = Vector3.zero;
        _waitTimer = 0f;
        _rateTimer = 0f;
        _isFirst = false;
        _isNullShotPosition = false;
        _isForecast = false;

        Direction = direction;
        _isForecast = isForecast;
        _rateTimer = (shotRate - forecastTime);

        if (laneIndex == null) _isNullShotPosition = true;
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
        //Debug.Log("RateTimer:" + _rateTimer);

        if (_isForecast && (_rateTimer >= shotRate - forecastTime))
        {
            _isForecast = false;
            SetShotPos(isSetSenterForecast);
            Forecast();
        }

        if (_rateTimer >= shotRate)
        {
            _rateTimer = -forecastTime;
            if(isForecast) _isForecast = true;
            SetShotPos(isSetCenter);
            _index++;
            if (_index >= laneIndex.Length)
            {
                _index = 0;
            }
            Shot();
        }
    }

    private void Shot()
    {
        if (projectile ==null) return;

        //Debug.Log("Shot:" + _shotPos);
        switch (shotType)
        {
            case ShotType.Single:
                CreateProjectile(Direction);
                break;

            case ShotType.Triple:
                List<Vector3> dirs = new List<Vector3>
                {
                    Direction,
                    (Direction + Vector3.up).normalized,
                    (Direction + Vector3.down).normalized
                };
                foreach (var dir in dirs)
                {
                    CreateProjectile(dir);
                }

                break;
        }
    }

    private void Forecast()
    {
        var obj = Context.Instantiate(forecastObject, _shotPos);
        obj.GetComponent<ForecastAttack>().Init(forecastTime);
    }
    
    private void CreateProjectile(Vector3 dir)
    {
        var obj = Context.Instantiate(projectile, _shotPos);
        var proj = obj.GetComponent<EnemyProjectile>();
        proj.Init(dir);
    }

    private void SetShotPos(bool isCenter)
    {
        var index = laneIndex[_index];
        
        // init shotPosition
        if (isCenter)
        {
            _shotPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.5f));
            _shotPos.z = Context.Transform.position.z;
        }
        else if (_isNullShotPosition)
        {
            _shotPos = Context.Transform.position;
        }
        else
        {
            _shotPos = Context.shotPoint.transform.position;
        }

        //shotPosをずらす
        var adjustPos = adjustPosition;
        if (IsRight) adjustPos.x = -adjustPos.x;
        _shotPos += adjustPos;

        _shotPos.y = upToLane + SceneContext.Instance.lanes[index].transform.position.y;
    }
}
