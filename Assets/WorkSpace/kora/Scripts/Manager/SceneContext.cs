using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] public List<GameObject> bounceLanes;
    [SerializeField] public Transform coreMovePoint;

    public static SceneContext Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
