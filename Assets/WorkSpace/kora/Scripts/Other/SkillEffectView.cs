using UnityEngine;

public class SkillEffectView : MonoBehaviour
{
    private Animator _animator;
    private bool _isActivated = false;
    private float _duration = 0f;
    private float _timer = 0f;
    private State _state = State.Activate;

    private enum State
    {
        Activate,
        DisActivate
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!TryGetComponent<Animator>(out _animator))
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActivated)
        {
            var info = _animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            switch (_state)
            {
                case State.Activate:
                    _timer += Time.deltaTime;
                    if (_timer <= _duration) return;
                    
                    _state = State.DisActivate;
                    _animator.SetTrigger("OnDisActive");
                    break;
                
                case State.DisActivate:
                    var info = _animator.GetCurrentAnimatorStateInfo(0);
                    if (info.normalizedTime >= 1)
                    {
                        Destroy(gameObject);
                    }
                    break;
            }
        }

    }

    private void NextState()
    {
        if(!_isActivated) Destroy(gameObject);
    }

    public void Activate(float duration)
    {
        _isActivated = true;
        _duration = duration;
    }
}
