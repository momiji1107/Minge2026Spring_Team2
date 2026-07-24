using System.Collections;
using UnityEngine;

public class PoisonEffectView : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Animator bottleAnimator;
    [SerializeField] private Animator gasAnimator;

    private PoisonController _controller;
    
    void Awake()
    {
        _controller = GetComponent<PoisonController>();
        _controller.ActivePoisonEvent += SetAnimationSpeed;
    }

    private void SetAnimationSpeed(float duration, float stopGasClipPer)
    {
        var info = gasAnimator.GetCurrentAnimatorStateInfo(0);
        var length = info.length;

        var stopTime = duration - length;

        var playDuration = duration - stopTime;

        var multiplier = length / (playDuration);
        
        StartCoroutine(Explosive(
            playDuration * stopGasClipPer, stopTime, multiplier));
    }

    private IEnumerator Explosive(float waitTime, float stopTime, float multiplier)
    { 
        gasAnimator.SetFloat("SpeedMultiplier", multiplier);
        
        yield return new WaitForSeconds(waitTime);
        
        // stop animation
        gasAnimator.SetFloat("SpeedMultiplier", 0);
        
        yield return new WaitForSeconds(stopTime-waitTime);
        
        gasAnimator.SetFloat("SpeedMultiplier", multiplier);
    }
}