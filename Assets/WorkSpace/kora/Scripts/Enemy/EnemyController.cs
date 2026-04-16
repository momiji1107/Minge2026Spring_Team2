using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData data;

    [SerializeField] private EnemyCore core;
    [SerializeField] private GameObject enemy;
    
    private EnemyCore _core;
    private EnemyContext _context;
    
    public Action<bool> OnSetIsRight;

    private float _slowPer;
    
    public EnemyContext Context => _context;
    
    public void Destroy() { Destroy(enemy); }
    public GameObject Instantiate(GameObject obj, Vector3 pos) { return Instantiate(obj, pos, obj.transform.rotation); }
    
    void Start()
    {
        _context = GetComponent<EnemyContext>();
        
        if (core == null) _core = GetComponent<EnemyCore>();
        else
        {
            _core = core;
            _context.SetCoreObject(_core.gameObject);
        }

        if (enemy == null) enemy = gameObject;

        _core.Init(data, this);
        _context.Init(this, enemy);
    }

    void Update()
    {
        _core.Tick();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other);
    }

    public void OnTrigger(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _core.OnHitPlayer(other);
        }
    }
}