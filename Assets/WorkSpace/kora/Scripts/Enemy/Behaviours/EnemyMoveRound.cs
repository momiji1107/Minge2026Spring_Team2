using UnityEngine;

public class EnemyMoveRound : EnemyMoveBehaviourBase
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float startDistance = 4f;
    [SerializeField] private bool isMoveRight = false;

    private float _currentDistance;
    private Vector3 _direction;
    
    void Start()
    {
        if (startDistance > distance) startDistance = distance;
        
        _currentDistance = startDistance;
        _direction = isMoveRight ? Vector3.right : Vector3.left;
        
        Direction = _direction;
    }
    
    public override void Tick(float dt)
    {
        _direction = Direction;
        MoveRound(dt);
    }

    private void MoveRound(float dt)
    {
        var d = _direction * (speed * dt);
        transform.position += d;
        _currentDistance += d.x;
        
        if (_currentDistance < 0f)
        {
            _currentDistance = 0f;
            _direction.x *= -1;
        }
        else if (_currentDistance > distance)
        {
            _currentDistance = distance;
            _direction.x *= -1;
        }
    }
}
