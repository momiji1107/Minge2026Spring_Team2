using UnityEngine;

[CreateAssetMenu(fileName = "BossMoveCoreOnce", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossMoveCoreOnce")]
public class BossMoveCoreOnce : BossBehaviourBaseSO
{
    [SerializeField] private int laneIndex = 0;
    
    private bool _isFire = false;

    protected override void OnInit()
    {
        _isFire = false;
    }

    public override void Tick(float dt)
    {
        if (_isFire) return;
        
        var obj = SceneContext.Instance.lanes[laneIndex];
        var y = obj.transform.position.y;
        
        var pos = Context.Transform.position;
        pos.y = y;
        
        Context.SetCorePosition(pos);
        
        _isFire = true;
        Debug.Log("Move Core!");
    }
}
