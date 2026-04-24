using UnityEngine;

public class SkillEffectView : MonoBehaviour
{
    private Animator _animator;
    
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
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}
