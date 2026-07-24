using System;
using UnityEngine;

public class PoisonHitCollider : MonoBehaviour
{
    [SerializeField] private PoisonController controller;
    
    void OnTriggerStay2D(Collider2D other)
    {
        controller.OnHit(other);
    }
}