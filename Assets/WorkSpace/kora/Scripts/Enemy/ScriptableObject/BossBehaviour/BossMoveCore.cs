using UnityEngine;
[CreateAssetMenu(fileName = "BossMoveCore", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossMoveCore")]
public class BossMoveCore : BossBehaviourBaseSO
{
    [Tooltip("レーンの移動順")][SerializeField, Range(0,4)] private int[] roundLanes;
    [Tooltip("1レーンに留まる時間")][SerializeField] private float roundTime = 3f;
    [Tooltip("起動してから移動し始めるまでの時間")][SerializeField] private float startTime = 1f;
    [Tooltip("コアの移動時間")] [SerializeField] private float moveTime = 1f;
    [Tooltip("移動速度")] [SerializeField] private float moveSpeed = 1.0f;
    [Tooltip("レーンから浮かせる距離")][SerializeField] private float upToLane = 1f;

    [SerializeField] public AnimationCurve moveCurve;
    
    private enum State
    {
        Wait,
        Move
    }
    
    private State _state;
    
    private float _waitTimer;
    private float _moveTimer;
    private int _currentLaneIndex;
    
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    protected override void OnInit()
    {
        
        _state = State.Wait;
        _waitTimer = roundTime - startTime;
        _moveTimer = 0;
        _currentLaneIndex = 0;
    }

    public override void Tick(float dt)
    {
        switch (_state)
        {
            case State.Wait:
                _waitTimer += dt;

                if (_waitTimer >= roundTime)
                {
                    _waitTimer = 0f;
                    SetPos();
                    _state = State.Move;
                }
                break;
            
            case State.Move:
                _moveTimer += dt;
                Move();
                if (_moveTimer >= moveTime)
                {
                    _moveTimer = 0f;
                    _state = State.Wait;
                }
                break;
        }

    }

    protected override void OnSetIsRight()
    {
        if (_state != State.Move) return;
        var viewPortX = GetXWorldToCameraPoint(_startPosition.x);
        
        Debug.Log("viewportX :"+ viewPortX + " to " + (1f-viewPortX));
        viewPortX = 1f-viewPortX;

        var posX = GetXOnCameraToWorldPoint(viewPortX);
        _startPosition.x = posX;
        _endPosition.x = posX;
    }
    
    private void SetPos()
    {
        //if (SceneContext.Instance == null) Debug.Log("null");
        //Debug.Log("Index: " + _currentLaneIndex);
        //Debug.Log("Index:" + _currentLaneIndex + " length:" + roundLanes.Length);
        var index = roundLanes[_currentLaneIndex];
        var pos = Context.CoreTransform.position;
        var posY = SceneContext.Instance.lanes[index].transform.position.y;
        pos.y = posY + upToLane;
        _currentLaneIndex++;
        if (_currentLaneIndex >= roundLanes.Length) _currentLaneIndex = 0;
        
        _startPosition = Context.CoreTransform.position;
        
        Context.SetCorePosition(_startPosition);
        
        _endPosition = pos;
        //Context.SetCorePosition(pos);
    }

    private void Move()
    {
        var rate = _moveTimer/moveTime;
        rate = moveCurve.Evaluate(rate);
        var delta = Vector3.Lerp(_startPosition, _endPosition, rate);
        //Debug.Log("startPos:" + _startPosition + " endPos:" + _endPosition);
        //Debug.Log("Rate:" + rate + " delta: " + delta);
        Context.SetCorePosition(delta);
    }
}