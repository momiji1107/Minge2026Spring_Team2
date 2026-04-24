using UnityEngine;

public class TestSetIsRight : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private bool isFunc1;

    private EnemyCore _core;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _core = enemy.GetComponent<EnemyCore>();
        if (isFunc1) Invoke(nameof(Func2), 3f);
    }

    private void Func1()
    {
        _core.SetIsRight(true);
    }

    private void Func2()
    {
        _core.SpawnMove(3, new Vector3(-3, 0, 0));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
