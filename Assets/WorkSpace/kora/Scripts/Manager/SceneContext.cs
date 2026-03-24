using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] public List<GameObject> bounceLanes;

    public static SceneContext Instance;

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
