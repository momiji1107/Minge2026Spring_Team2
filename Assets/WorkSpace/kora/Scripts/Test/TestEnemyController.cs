using UnityEngine;

public class TestEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private EnemyCore _core;
    
    Vector3 _pos; 
    float _time;
    Vector3 _targetPos; 
    float _slowPer;
    void Start()
    {
        _core = GetComponent<EnemyCore>();
        
        _pos = enemy.transform.position;
        _time = 10f;
        _targetPos = new Vector3(_pos.x - 5, _pos.y + 10, _pos.z);
        _slowPer = 80f;
        
        Invoke(nameof(Test1), 1f);
        Invoke(nameof(Test2), 3f);
    }

    private void Test1()
    {
        Debug.Log("Test");

        _pos.x -= 5f;
        
        _core.Slow(_time, _slowPer);
        //_core.Stun(_time);
        //_core.SpawnMove(time, targetPos);
    }

    private void Test2()
    {
        _core.Stun(3f);
    }
}
