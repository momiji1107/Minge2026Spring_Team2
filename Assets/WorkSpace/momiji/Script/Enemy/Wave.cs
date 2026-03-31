using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
        [Tooltip("生成する敵のPrefab")] public GameObject enemyPrefab;
        [Tooltip("生成するSpawnPoint")] public Transform spawnPoint;
        [Tooltip("生成直後歩く時間")] public float spawnMoveTime; 
        [Tooltip("生成後向かう座標")]public Vector3 targetPosition;
        public bool isRight;
}

[System.Serializable]
public class Wave
{ 
        [Tooltip("生成する時間")] public float spawnTime;
        public List<EnemySpawnData> enamydatas;
}
