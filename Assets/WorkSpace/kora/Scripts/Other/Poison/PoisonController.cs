using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : MonoBehaviour
{
    [SerializeField] private GameObject bottle;
    [SerializeField] private GameObject gas;

    [SerializeField, Range(0f, 1f)] private float explosionableRate;

    [SerializeField, Range(0f, 1f)] private float upToLaneAtExplosive;

    
    [SerializeField, Range(0f, 1f)] private float stopGasClipPer;
    private enum State
    {
        Throwing,
        Gas
    }

    private AnimationCurve _arcCurve;
    private AnimationCurve _widthCurve;

    private float _distance;
    private float _height;
    private float _throwingDuration;
    private float _duration;
    private float _damage;
    private float _hitInterval;

    private Vector3 _direction;
    private Vector3 _startPos;

    private bool _isInit;
    private float _throwingTimer;
    private float _gasTimer;

    private State _state;

    private float _xRate;
    private List<GameObject> _hitEnemies;

    public event Action<float, float> ActivePoisonEvent; //(duration)

    public void Init(
        float distance, float height,
        float duration, float damage, float hitInterval,
        Vector3 direction,
        float throwingDuration,
        AnimationCurve arcCurve,
        AnimationCurve widthCurve)
    {
        _distance = distance;
        _height = height;
        _throwingDuration = throwingDuration;
        _duration = duration;
        _damage = damage;
        _hitInterval = hitInterval;
        _direction = direction;
        _arcCurve = arcCurve;
        _widthCurve = widthCurve;

        _state = State.Throwing;
        _throwingTimer = 0f;
        _gasTimer = 0f;
        _startPos = transform.position;
        _xRate = 0f;
        _hitEnemies = new List<GameObject>();

        bottle.gameObject.SetActive(true);
        gas.gameObject.SetActive(false);

        _isInit = true;
    }

    private void Update()
    {
        if (!_isInit)
        {
            return;
        }

        if (!gameObject.activeSelf)
        {
            return;
        }

        float dt = Time.deltaTime;

        switch (_state)
        {
            case State.Throwing:
                Throwing(dt);
                break;
            case State.Gas:
                //Debug.Log("gas state!");
                Gas(dt);
                break;
        }
    }

    private void Throwing(float dt)
    {
        _throwingTimer += dt;

        float tRate = _throwingTimer / _throwingDuration;
        _xRate = _widthCurve.Evaluate(tRate);
        float yRate = _arcCurve.Evaluate(_xRate);

        float posX = _startPos.x + _distance * _xRate * _direction.x;
        float posY = _startPos.y + _height * yRate;

        transform.position = new Vector3(posX, posY, transform.position.z);

        //Debug.Log($"Throwing - tRate: {tRate:F2}, xRate: {_xRate:F2}, pos: ({posX:F2}, {posY:F2})");

        if (_throwingTimer >= _throwingDuration)
        {
            //Debug.Log("Throw time out");
            Explosive();
        }
    }

    private void Gas(float dt)
    {
        _gasTimer += dt;
        //Debug.Log($"Gas state - timer: {_gasTimer:F2}, duration: {_duration:F2}");
        if (_gasTimer > _duration)
        {
            //Debug.Log("Gas time out");
            Destroy(gameObject);
        }
    }

    public void OnHit(Collider2D other)
    {
        //Debug.Log($"OnHit called - _xRate: {_xRate}, explosionableRate: {explosionableRate}, state: {_state}");


        if (_xRate <= explosionableRate)
        {
            //Debug.Log("OnHit ignored - xRate too low");
            return;
        }

        var obj = other.gameObject;

        switch (_state)
        {
            case State.Throwing:
                for (int i = 0; i < SceneContext.Instance.lanes.Count; i++)
                {
                    if (obj != SceneContext.Instance.lanes[i]) continue;

                    //Debug.Log("Hit Lane");
                    var posY = upToLaneAtExplosive + SceneContext.Instance.lanes[i].transform.position.y;
                    Vector3 pos = new Vector3(transform.position.x, posY, transform.position.z);
                    transform.position = pos;
                    Explosive();
                    return;
                }

                break;

            case State.Gas:
                
                if (!obj.CompareTag("Enemy")) return;
                if (_hitEnemies.Contains(obj))
                {
                    //Debug.Log("Hit already hit Enemy");
                    return;
                }
                
                StartCoroutine(HitEnemy(obj));
                break;
        }
    }

    private void Explosive()
    {
        //Debug.Log("Explosive called!");
        _state = State.Gas;
        bottle.gameObject.SetActive(false);
        gas.gameObject.SetActive(true);
        ActivePoisonEvent?.Invoke(_duration, stopGasClipPer);
        //Debug.Log($"Explosive completed - Gas activated, duration: {_duration}");
    }

    private IEnumerator HitEnemy(GameObject enemy)
    {
        if (!enemy.TryGetComponent<IDamageable>(out IDamageable damageable)) yield return null;

        _hitEnemies.Add(enemy);

        Debug.Log("TakeDamage: " + _damage);
        damageable.TakeDamage(_damage);
        yield return new WaitForSeconds(_hitInterval);
       
        _hitEnemies.Remove(enemy);
        yield return null;
    }
}