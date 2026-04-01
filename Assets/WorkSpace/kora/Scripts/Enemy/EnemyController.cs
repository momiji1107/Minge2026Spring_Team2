using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<EnemyBehaviourBase> _behaviours;
    private EnemyCore _core;

    public Action<bool> OnSetIsRight;
    
    private bool _isStop = false;
    private bool _isSlow = false;

    private float _slowPer;
    
    void Awake()
    {
        _behaviours = GetComponents<EnemyBehaviourBase>().ToList();

        _core = GetComponent<EnemyCore>();

        _core.OnDead += DisActiveEnemy;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        var isSkip = false;
        
        if (_isStop)
        {
            isSkip = true;
        }
        else if (_isSlow)
        {
            dt *= _slowPer;
        }
        
        if (!isSkip)
        {
            foreach (var b in _behaviours)
            {
                b.Tick(dt);
            }
        }
    }

    public void SetIsRight(bool right)
    {
        foreach (var b in _behaviours)
        {
            b.SetIsRight(right);
        }
        OnSetIsRight?.Invoke(right);
    }
    
    public void Slow(float time, float per)
    {
        if (per >= 100f || time == 0) return;
        StartCoroutine(RunSlow(time, per));
    }
    
    public void Stun(float time)
    {
        if (time == 0) return;
        StartCoroutine(RunStun(time));
    }

    public void SpawnMove(float time, Vector3 vector)
    {
        //Local座標からworld座標に変換
        var targetPos = gameObject.transform.position + vector;
        //Debug.Log(targetPos);
        
        StartCoroutine(RunSpawnMove(time, targetPos));
    }
    
    private IEnumerator RunSlow(float time, float per)
    {
        //Debug.Log("Slow");
        _isSlow = true;
        _slowPer = (100f - per) / 100f;
        
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        _isSlow = false;
    }
    
    private IEnumerator RunStun(float time)
    {
        //Debug.Log("Stun");
        _isStop = true;

        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        _isStop = false;
    }

    private IEnumerator RunSpawnMove(float time, Vector3 targetPos)
    {
        if (time == 0)
        {
            transform.position = targetPos;
        }
        
        _isStop = true;
        
        var currentPos = transform.position;
        var direction = (targetPos - currentPos).normalized;
        float speed = Vector3.Distance(currentPos, targetPos) / time;

        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            transform.position += direction * (speed * Time.deltaTime);
            yield return null;
        }
        
        _isStop = false;
    }

    private void DisActiveEnemy()
    {
        foreach (var b in _behaviours)
        {
            b.enabled = false;
        }
        
        this.enabled = false;
    }
}
