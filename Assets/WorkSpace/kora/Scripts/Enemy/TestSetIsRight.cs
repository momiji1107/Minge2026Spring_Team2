using UnityEngine;

public class TestSetIsRight : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private bool isFunc1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isFunc1) Invoke(nameof(Func1), 3f);
    }

    private void Func1()
    {
        var core = enemy.GetComponent<EnemyCore>();
        core.SetIsRight(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
