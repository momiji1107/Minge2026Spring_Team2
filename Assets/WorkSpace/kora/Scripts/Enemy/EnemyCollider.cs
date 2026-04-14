using System;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public EnemyController Controller;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) Debug.Log("Contact Player");
        Controller.OnTrigger(other);
    }
}
