using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<EnemyBehaviourBase> _behaviours;

    void Awake()
    {
        _behaviours = GetComponents<EnemyBehaviourBase>().ToList();
    }

    void Update()
    {
        foreach (var b in _behaviours)
        {
            b.Tick(Time.deltaTime);
        }
    }
}
