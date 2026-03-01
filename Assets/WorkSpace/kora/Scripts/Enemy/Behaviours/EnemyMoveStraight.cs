using UnityEngine;

public class EnemyMoveStraight : EnemyBehaviourBase
{
    [Header("移動速度")][SerializeField] private float speed = 5f;
    [Header("進行方向")][SerializeField] private Vector3 direction = Vector3.left;
    
    public void Tick(float dt)
    {
        Move(dt);
    }

    private void Move(float dt)
    {
        transform.position += direction * (speed * dt);
    }
}
