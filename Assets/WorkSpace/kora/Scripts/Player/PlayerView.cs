using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerAttackController attack;
    
    [SerializeField] private SelectedPlayer select;
    
    private Animator _animator = null;
    private PlayerData _data = null;
    
    private readonly string _isMove = "IsMove";
    private readonly string _isAttack = "Attack";

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
    }

    private void Update()
    {
        if (ReferenceEquals(_animator, null)) return;
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetBool(_isMove, true);
        }
        else
        {
            _animator.SetBool(_isMove, false);
        }
    }

    private void ActiveBasicAttackAnim()
    {
        _animator.SetTrigger(_isAttack);
    }
}
