using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private PlayerAttackController attack;
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private SelectedPlayer select;

    [Tooltip("dead再生終了からGameOver画面表示までの時間")] [SerializeField]
    private float waitDuration = 0.5f;
    
    private Animator _animator = null;
    private PlayerData _data = null;

    private const string IsMove = "IsMove";
    private const string IsAttack = "Attack";
    private const string OnDead = "OnDead";
    
    private const string DeadClipName = "Dead";

    private void Start()
    {
        if (!player.TryGetComponent(out _animator)) Debug.Log("Player has no animator");
        if (attack == null) Debug.Log("Null PlayerAttackController");
        else
        {
            attack.BasicAttackAnim += ActiveBasicAttackAnim;
        }

        _data = select?.PlayerData;
        _animator.runtimeAnimatorController = _data?.Animator;
        
        var spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _data?.PlayerSprite;

        playerModel.GameOverEvent += ActiveGameOver;
    }

    private void Update()
    {
        if (ReferenceEquals(_animator, null)) return;
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetBool(IsMove, true);
        }
        else
        {
            _animator.SetBool(IsMove, false);
        }
    }

    private void ActiveBasicAttackAnim()
    {
        _animator.SetTrigger(IsAttack);
    }

    private void ActiveGameOver()
    {
        ActiveDeadAnim();

        float clipLength = 0f;
        
        foreach (var clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == DeadClipName)
            {
                clipLength = clip.length;
            }
        }
        
        gameManager.GameOver(clipLength + waitDuration);
    }
    
    private void ActiveDeadAnim()
    {
        _animator.SetTrigger(OnDead);
    }
}
