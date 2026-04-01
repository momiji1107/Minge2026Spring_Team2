using UnityEngine;

public class EnemyMoveStraight : EnemyMoveBehaviourBase
{
    [Header("移動速度")][SerializeField] private float speed = 5f;
    [Header("進行方向")][SerializeField] private Vector3 direction = Vector3.left;

    private bool _isFirst = true;

    private void Awake()
    {
        base.Direction = direction;
    }
    
    // Update
    public override void Tick(float dt)
    {
        if (_isFirst)
        {
            _isFirst = false;
            ActiveMoveAnim?.Invoke();
        }
        
        CheckVisible();
        
        Move(dt);
    }

    private void Move(float dt)
    {
        transform.position += Direction * (speed * dt);
    }
    
    private void CheckVisible()
    {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || 1 < pos.x || pos.y < 0 || 1 < pos.x)
        {
            Debug.Log("Out of Camera and Destroy:" + gameObject.name);
            Destroy(gameObject);
        }
    }
}
