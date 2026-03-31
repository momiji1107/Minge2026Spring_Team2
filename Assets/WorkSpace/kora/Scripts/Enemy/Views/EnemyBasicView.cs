using UnityEngine;

public interface IEnemyView
{
}

/// <summary>
/// AttackBehaviour, MoveBehaviourのそれぞれ最大一つのみアタッチされている敵に使えるView
/// </summary>
public class EnemyBasicView : MonoBehaviour, IEnemyView
{
    protected Animator Animator = null;

    protected EnemyController Controller;
    protected EnemyAttackBehaviourBase Attack;
    protected EnemyMoveBehaviourBase Move;
    
    protected static class AnimParam
    {
        public const string IsMove = "IsMove";
        public const string IsAttack = "IsAttack";
    }

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();

        Controller = GetComponent<EnemyController>();
        Attack = GetComponent<EnemyAttackBehaviourBase>();
        Move = GetComponent<EnemyMoveBehaviourBase>();

        if (Controller != null) Controller.OnSetIsRight += OnSetIsRight;
        if (Attack != null) Attack.ActiveAttackAnim += PlayAttackAnim;
        if (Move != null) Move.ActiveMoveAnim += PlayMoveAnim;
        
        OnAwake();
    }

    protected virtual void OnAwake(){}
    
    protected virtual void PlayAttackAnim()
    {
        if (Animator == null) return;
        
        Animator.SetBool(AnimParam.IsAttack, true);
    }

    protected virtual void PlayMoveAnim()
    {
        if (Animator == null) return;
        
        Animator.SetBool(AnimParam.IsMove, true);
    }

    protected virtual void OnSetIsRight(bool isRight)
    {
        if (isRight)
        {
            gameObject.transform.Rotate(0,180,0);
        }
    }
}