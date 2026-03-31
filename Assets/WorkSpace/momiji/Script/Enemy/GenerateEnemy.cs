using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    private float timer = 0;
    private int idx;

    [Header("敵の生成ウェーブ")]
    public List<Wave> waves;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        idx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (idx > waves.Count - 1) return;
        timer += Time.deltaTime;
        
        if (timer >= waves[idx].spawnTime)
        {
            Spawn(waves[idx]);
            idx++;
        }
    }

    private void Spawn(Wave wave)
    {
        foreach (EnemySpawnData data in wave.enamydatas)
        {
            var enemy = Instantiate(data.enemyPrefab, data.spawnPoint.position, Quaternion.identity);
            var core = enemy.GetComponent<EnemyCore>();
            core.SpawnMove(data.spawnMoveTime, data.targetPosition);
            core.SetIsRight(data.isRight);
        }
    }
}
