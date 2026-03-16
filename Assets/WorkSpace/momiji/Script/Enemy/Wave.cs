using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
        [Tooltip("生成する敵のPrefab")] public GameObject enemyPrefab;
        [Tooltip("生成するSpawnPoint")] public Transform spawnPoint;
}

[System.Serializable]
public class Wave
{ 
        [Tooltip("生成する時間")] public float spawnTime;
        public List<EnemySpawnData> enamydatas;
}
