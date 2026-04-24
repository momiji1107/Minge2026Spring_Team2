using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSpawnSlime", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossSpawnSlime")]
public class BossSpawnSlime : BossBehaviourBaseSO
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private float interval = 15f;
    [SerializeField] private float startTime = 3f;
    [SerializeField] private int[] spawnLanes = {0,2,4};
    [SerializeField] private float upToLane = 1f;

    private float _timer;

    protected override void OnInit()
    {
        _timer = interval - startTime;
    }
    
    public override void Tick(float dt)
    {
        _timer += Context.DeltaTime;

        if (_timer >= interval)
        {
            _timer = 0f;

            foreach (var index in spawnLanes)
            {
                var pos = SceneContext.Instance.lanes[index].transform.position;
                pos.y += upToLane;
                pos.x = Context.Transform.position.x;

                Context.Instantiate(slimePrefab, pos);
            }
        }
    }
}
