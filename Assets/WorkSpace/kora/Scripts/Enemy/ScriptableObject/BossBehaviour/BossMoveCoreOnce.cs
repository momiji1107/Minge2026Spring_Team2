using UnityEngine;

[CreateAssetMenu(fileName = "BossMoveCoreOnce", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossMoveCoreOnce")]
public class BossMoveCoreOnce : BossBehaviourBaseSO
{
    [SerializeField] private int targetLaneIndex = 0;
    [SerializeField] private float upToLane = 1f;
    
    private bool _isFire = false;

    protected override void OnInit()
    {
        _isFire = false;

        if (targetLaneIndex < 0) targetLaneIndex = 0;
        else if (targetLaneIndex > 4) targetLaneIndex = 4;
    }

    public override void Tick(float dt)
    {
        if (_isFire) return;
        var obj = SceneContext.Instance.lanes[targetLaneIndex].transform;
        var y = obj.position.y;
        
        var pos = Context.CoreTransform.position;
        pos.y = y + upToLane;
        
        Context.SetCorePosition(pos);
        
        _isFire = true;
        //Debug.Log("Move Core!");
    }
}
