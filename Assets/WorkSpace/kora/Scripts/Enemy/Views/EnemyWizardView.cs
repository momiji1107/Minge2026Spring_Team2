using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyWizardView : EnemyBasicView
{
    [SerializeField] private float fireTime = 1.15f;

    private float _startAttackLength;
    private EnemyShotSingle _shot;

    private readonly string _attackClip = "Wizard_Attack";
    private readonly string _attackSpeed = "AttackSpeed";
    
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        _shot = GetComponent<EnemyShotSingle>();

        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == _attackClip)
            {
                _startAttackLength = clip.length;
            }
        }
        
        //Resister
        _shot.ActiveAttackAnim += PlayAttackAnim;
    }

    protected override void PlayAttackAnim()
    {
        if (ReferenceEquals(animator, null) || ReferenceEquals(_shot, null)) return;
        
        var speedMultiplier = _startAttackLength / _shot.ShotRate;
        
        animator.SetFloat(_attackSpeed, speedMultiplier);
        
        // Shot -> Endまでの時間待つ
        var t = (_startAttackLength - fireTime) / speedMultiplier;
        StartCoroutine(StartShotAnim(t));
    }

    private IEnumerator StartShotAnim(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(AnimParam.IsAttack, true);
    }
}
