using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
        [Tooltip("生成する敵のPrefab")] public GameObject enemyPrefab;
        [Tooltip("生成するSpawnPoint")] public Transform spawnPoint;
        [Tooltip("左側から生成するならtrue")] public bool onLeft;
}

[System.Serializable]
public class Wave
{ 
        [Tooltip("生成する時間")] public float spawnTime;
        public List<EnemySpawnData> enamydatas;
}
