using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1 : EnemyBase
{
    [Header("移動速度")][SerializeField] private float speed = 5f;
    
    protected override void OnActive()
    {
        Move();
    }

    private void Move()
    {
        gameObject.transform.position += Vector3.left * (speed * Time.deltaTime);
    }
}