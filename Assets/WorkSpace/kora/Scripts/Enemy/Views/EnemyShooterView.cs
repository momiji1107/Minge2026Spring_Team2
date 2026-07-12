using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyShooterView : EnemyBasicView
{
    [SerializeField] private float fireFlame = 30;

    private float _startAttackLength;
    private float _clipFlameRate;

    private readonly string _attackClip = "Attack";
    private readonly string _attackSpeed = "AttackSpeed";
    
    protected override void OnAwake()
    {

        foreach (var clip in Animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == _attackClip)
            {
                _startAttackLength = clip.length;
                _clipFlameRate = clip.frameRate;
            }
        }
    }

    protected override void PlayAttackAnim()
    {
        if (ReferenceEquals(Animator, null)) return;

        var rate = ((NormalEnemyData)Core.GetData()).shotSingle.shotRate;
        var speedMultiplier = _startAttackLength / rate;
        
        Animator.SetFloat(_attackSpeed, speedMultiplier);
        
        // Shot -> Endまでの時間待つ
        var fireTime = fireFlame / _clipFlameRate;
        var t = (_startAttackLength - fireTime) / speedMultiplier;
        StartCoroutine(StartShotAnim(t));
    }

    private IEnumerator StartShotAnim(float time)
    {
        //Debug.Log("Play Attack Animation");
        yield return new WaitForSeconds(time);
        Animator.SetBool(AnimParam.IsAttack, true);
    }
}
