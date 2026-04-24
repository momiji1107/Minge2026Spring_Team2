using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] public List<GameObject> lanes;
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject gameManager;

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
