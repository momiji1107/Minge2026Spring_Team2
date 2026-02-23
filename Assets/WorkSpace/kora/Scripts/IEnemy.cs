using UnityEngine;

public interface IEnemy
{
    float Hp { get; }
    Vector3 Position { get; }
    
    void TakeDamage(float damage);
}
