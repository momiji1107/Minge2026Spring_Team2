using UnityEngine;

public interface IEnemyView
{
}

/// <summary>
/// AttackBehaviour, MoveBehaviourのそれぞれ最大一つのみアタッチされている敵に使えるView
/// </summary>
public class EnemyBasicView : MonoBehaviour, IEnemyView
{
    [SerializeField] protected Animator animator = null;

    protected EnemyAttackBehaviourBase Attack;
    protected EnemyMoveBehaviourBase Move;
    
    protected static class AnimParam
    {
        public const string IsMove = "IsMove";
        public const string IsAttack = "IsAttack";
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        Attack = GetComponent<EnemyAttackBehaviourBase>();
        Move = GetComponent<EnemyMoveBehaviourBase>();

        Attack.ActiveAttackAnim += PlayAttackAnim;
        Move.ActiveMoveAnim += PlayMoveAnim;
    }

    protected virtual void PlayAttackAnim()
    {
        if (ReferenceEquals(animator, null)) return;
        
        animator.SetBool(AnimParam.IsAttack, true);
    }

    protected virtual void PlayMoveAnim()
    {
        if (ReferenceEquals(animator, null)) return;
        
        animator.SetBool(AnimParam.IsMove, true);
    }
}