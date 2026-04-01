using UnityEngine;

public class EraseBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
