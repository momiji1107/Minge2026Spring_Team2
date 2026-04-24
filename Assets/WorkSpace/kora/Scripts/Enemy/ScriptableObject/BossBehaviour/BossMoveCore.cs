using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossMoveCore", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossMoveCore")]
public class BossMoveCore : BossBehaviourBaseSO
{
    [SerializeField, Range(0,4)] private int[] roundLanes;
    [SerializeField] private float roundTime = 3f;
    [SerializeField] private float startTime = 1f;
    [SerializeField] private float upToLane = 1f;

    private float _timer;
    private int _currentLaneIndex;
    
    protected override void OnInit()
    {
        _timer = roundTime - startTime;
        _currentLaneIndex = 0;
        
        //Debug.Log("LaneLength " + _lanes.Count);
    }

    public override void Tick(float dt)
    {
        _timer += dt;

        if (_timer >= roundTime)
        {
            _timer = 0f;
            MoveCoreToLaneY();
        }
    }

    private void MoveCoreToLaneY()
    {
        //if (SceneContext.Instance == null) Debug.Log("null");
        //Debug.Log("Index: " + _currentLaneIndex);
        var pos = Context.CoreTransform.position;
        var posY = SceneContext.Instance.lanes[_currentLaneIndex].transform.position.y;
        pos.y = posY + upToLane;
        _currentLaneIndex++;
        if (_currentLaneIndex >= roundLanes.Length) _currentLaneIndex = 0;
        
        Context.SetCorePosition(pos);
    }
}
