using UnityEngine;

public class EnemyMoveStraight : EnemyMoveBehaviourBase
{
    [Header("移動速度")][SerializeField] private float speed = 5f;
    [Header("進行方向")][SerializeField] private Vector3 direction = Vector3.left;

    private bool isFirst = true;

    private void Awake()
    {
        direction = direction.normalized;
    }
    
    // Update
    public override void Tick(float dt)
    {
        if (isFirst)
        {
            isFirst = false;
            ActiveMoveAnim?.Invoke();
        }
        Move(dt);
    }

    private void Move(float dt)
    {
        transform.position += direction * (speed * dt);
    }
}
