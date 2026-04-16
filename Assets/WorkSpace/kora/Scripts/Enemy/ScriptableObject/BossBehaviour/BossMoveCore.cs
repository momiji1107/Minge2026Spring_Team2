using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossMoveCore", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossMoveCore")]
public class BossMoveCore : BossBehaviourBaseSO
{
    [SerializeField] private int[] roundLanes = {0,1,2,3,4};
    [SerializeField] private float roundTime = 3f;
    [SerializeField] private float upToLane = 1f;

    private float _timer;
    private List<GameObject> _lanes;
    private int _currentLaneIndex;
    
    protected override void OnInit()
    {
        _timer = 0f;
        _currentLaneIndex = 0;
        _lanes = new List<GameObject>();
        
        if (SceneContext.Instance == null) return;
        _lanes = SceneContext.Instance.lanes;
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
        var posY = _lanes[_currentLaneIndex].transform.position.y;
        pos.y = posY + upToLane;
        _currentLaneIndex++;
        if (_currentLaneIndex >= roundLanes.Length) _currentLaneIndex = 0;
        
        Context.SetCorePosition(pos);
    }
}
