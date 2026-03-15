using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private Animator animator = null;

    private void Start()
    {
        if (!player.TryGetComponent(out animator)) Debug.Log("Player has no animator");
    }

    private void Update()
    {
        if (ReferenceEquals(animator, null)) return;
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }
}
