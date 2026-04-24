using UnityEngine;

[CreateAssetMenu(fileName = "BossSwitchLR", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossSwitchLR")]
public class BossSwitchLR : BossBehaviourBaseSO
{
    [SerializeField] private bool isOnce;
    [SerializeField] private float interval = 15f;
    [SerializeField] private float startTime = 2f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float adjustXRatio = 0.1f;

    private bool _isFire;
    private float _moveTime;
    private float _waitTimer;
    private float _moveTimer;
    private MoveState _moveState;
    
    private double _startXRatio;
    
    private enum MoveState
    {
        WaitInterval,
        Moving1,
        Moving2
    }
    
    protected override void OnInit()
    {
        _isFire = false;
        _moveTime = duration/2;
        
        _waitTimer = interval - startTime;
        _moveTimer = _moveTime;
        
        _moveState = MoveState.WaitInterval;
    }

    public override void Tick(float dt)
    {
        if (_isFire) return;
        
        dt = Context.DeltaTime;
        
        switch (_moveState)
        {
            case MoveState.WaitInterval:
                _waitTimer += dt;
                if (_waitTimer >= interval)
                {
                    _waitTimer = 0;
                    SwitchMove1();
                    _moveState = MoveState.Moving1;
                }
                break;
            
            case MoveState.Moving1:
                _moveTimer += dt;
                if (_moveTimer >= _moveTime)
                {
                    _moveTimer = 0;
                    SwitchMove2();
                    _moveState = MoveState.Moving2;
                }
                break;
            
            case MoveState.Moving2:
                _moveTimer += dt;
                if (_moveTimer >= _moveTime)
                {
                    _moveTimer = _moveTime;
                    if (isOnce) _isFire = true;
                    _moveState = MoveState.WaitInterval;
                }
                break;
        }
    }

    private void SwitchMove1()
    {
        //Debug.Log("SwitchMove:" + _moveState);
        
        _startXRatio = GetXWorldToCameraPoint(Context.Transform.position.x);

        float xRatio = !IsRight ? 1-adjustXRatio : 0+adjustXRatio;
        var vecX = GetXOnCameraToWorldPoint(xRatio) - Context.Transform.position.x;
        
        //Debug.Log("move1 xRatio:" + xRatio);
        
        Core.SpawnMove(_moveTime, new Vector3(vecX, 0, 0));
    }

    private void SwitchMove2()
    {
        var pos = Context.Transform.position;
        float xRatio = !IsRight ? 0+adjustXRatio : 1-adjustXRatio;
        pos.x = GetXOnCameraToWorldPoint(xRatio);
        Context.SetPosition(pos);
        Core.SetIsRight(!IsRight);
        
        double targetXRatio = 1 - _startXRatio;
        //Debug.Log("move2 xRatio:" + targetXRatio + "\nIsRight:" + Core.GetIsRight());
        var vecX = GetXOnCameraToWorldPoint((float)targetXRatio);
        vecX -= Context.Transform.position.x;
        //Debug.Log("move2 vecX:" + vecX);
        Core.SpawnMove(_moveTime, new Vector3(vecX, 0, 0));
    }
}
