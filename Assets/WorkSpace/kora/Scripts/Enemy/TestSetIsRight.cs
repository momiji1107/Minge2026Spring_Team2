using UnityEngine;

public class TestSetIsRight : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SetIsRight), 3f);
    }

    private void SetIsRight()
    {
        enemy.GetComponent<EnemyCore>().SetIsRight(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
