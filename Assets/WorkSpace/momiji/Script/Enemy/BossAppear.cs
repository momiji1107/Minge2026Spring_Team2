using UnityEngine;

public class BossAppear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneContext.Instance.audioManager.BossAppear();
    }
}
