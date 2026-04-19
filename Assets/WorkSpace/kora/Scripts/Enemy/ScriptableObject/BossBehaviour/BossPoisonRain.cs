using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossPoisonRain", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossPoisonRain")]
public class BossPoisonRain : BossBehaviourBaseSO
{
    [Serializable]
    private class RainWave
    {
        public float[] pointPercents = {10, 50, 80};
    }
    
    [Header("弾")]
    [SerializeField] private GameObject prefab;
    
    [SerializeField] private float rainInterval = 5f;
    [SerializeField] private float waveInterval = 0.5f;
    [SerializeField] private float startTime = 2f;
    [SerializeField] private float upToLane = 10f;
    [SerializeField] private List<RainWave> rainWaves;

    private float _rainTimer;
    private float _waveTimer;
    private int _index;
    
    protected override void OnInit()
    {
        _rainTimer = rainInterval - startTime;
        _waveTimer = waveInterval;
        _index = 0;
    }

    public override void Tick(float dt)
    {
        if (_rainTimer < rainInterval)
        {
            _rainTimer += dt;
            return;
        }

        if (_waveTimer < waveInterval)
        {
            _waveTimer += dt;
            return;
        }
        
        Rain();
        _waveTimer = 0f;
        _index++;
        if (_index >= rainWaves.Count)
        {
            _index = 0;
        _rainTimer = 0f;
        }

    }

    private void Rain()
    {
        var pos = SceneContext.Instance.lanes[4].transform.position;
        pos.y += upToLane;
        
        foreach (var xPer in rainWaves[_index].pointPercents)
        {
            pos.x = GetXOnCamera(xPer/100);

            var obj = Context.Instantiate(prefab, pos);
            obj.GetComponent<EnemyProjectile>().Init(Vector3.down);
        }

    }
    
    float GetXOnCamera(float xRatio)
    {
        Vector3 viewport = new Vector3(xRatio, 0, 0);
        Vector3 world = Camera.main.ViewportToWorldPoint(viewport);
        return world.x;
    }
}
