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

    [SerializeField] protected GameObject enemy;
    [SerializeField] protected GameObject coreObject;
    
    protected EnemyCore Core;
    protected EnemyController Controller;
    
    private bool _isDead = false;
    private bool _isNullAnimController = false;
    private bool _previousIsRight = false;
    
    protected static class AnimParam
    {
        public const string IsMove = "IsMove";
        public const string IsAttack = "IsAttack";
        public const string OnDead = "OnDead";
        
        public const string DeadClip = "Dead";
        public const int DeadLayerIndex = 0;
    }

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();

        if (Animator.runtimeAnimatorController == null) _isNullAnimController = true;
        
        if (enemy==null) enemy = this.gameObject; 
        if (coreObject == null) coreObject = this.gameObject;
        
        Core = coreObject.GetComponent<EnemyCore>();
        Controller = coreObject.GetComponent<EnemyController>();
        
        if (Controller != null) Controller.OnSetIsRight += OnSetIsRight;

        if (Core != null)
        {
            Core.OnDead += PlayDeadAnim;
            Core.OnAttack += PlayAttackAnim;
            Core.OnMove += PlayMoveAnim;
        }
        
        OnAwake();
    }

    protected virtual void Update()
    {
        
        if (_isDead)
        {
            if (_isNullAnimController) ; //{ Core.ActiveDestroy(); }
            else
            {
                var info = Animator.GetCurrentAnimatorStateInfo(AnimParam.DeadLayerIndex);
                if (info.normalizedTime >= 1 && info.IsName(AnimParam.DeadClip))
                {
                    //Debug.Log("Animation Clip: " + info.IsName("Dead"));
                    Core.ActiveDestroy();
                }
            }
        }
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

    protected virtual void PlayDeadAnim()
    {
        if (Animator == null)
        {
            Core.ActiveDestroy();
            return;
        }
        Animator.SetTrigger(AnimParam.OnDead);
        
        _isDead = true;
    }

    protected virtual void OnSetIsRight(bool isRight)
    {
        //Debug.Log(isRight);
        if (isRight != _previousIsRight)
        {
            _previousIsRight = isRight;
            enemy.transform.Rotate(0,180,0);
        }
    }
}