using UnityEngine;

public class EnemyContext : MonoBehaviour
{
    [Header("接触判定")][SerializeField] public Collider2D damageCollider;
    [Header("発射位置")][SerializeField] public GameObject shotPoint;
    [Header("ダメージ時に点滅するsprite")] public SpriteRenderer sr;
    [SerializeField] private GameObject scorePrefab;
    public GameObject ScorePrefab => scorePrefab;
    
    private EnemyController _controller;
    private GameObject _gameObject;
    private GameObject _coreObject;
    
    public void Init(EnemyController controller, GameObject gameObject)
    {
        _controller = controller;
        _gameObject = gameObject;
    }

    public void SetCoreObject(GameObject coreObject)
    {
        _coreObject = coreObject;
    }
    
    //Getter
    public GameObject GameObject => _gameObject;
    public Transform Transform => _gameObject.transform;
    public float DeltaTime => Time.deltaTime;
    public Transform CoreTransform => _coreObject.transform;

    public void Move(Vector3 delta)
    {
        _gameObject.transform.position += delta;
    }

    public void SetPosition(Vector3 position)
    {
        _gameObject.transform.position = position;
    }

    //Coreの操作
    public void MoveCore(Vector3 delta)
    {
        if (_coreObject == null) return;
        _coreObject.transform.position += delta;
    }

    public void SetCorePosition(Vector3 position)
    {
        if (_coreObject == null) return;
        Debug.Log("Position: " + position);
        _coreObject.transform.position = position;
    }
    
    public void Destroy()
    {
        _controller.Destroy();
    }

    public GameObject Instantiate(GameObject obj, Vector3 pos)
    {
        return _controller.Instantiate(obj, pos);
    }
}