using UnityEngine;

public class TestPoisonSkillPrefab : MonoBehaviour
{
    [Header("投てき設定")]
    [Tooltip("飛距離"),Range(0f,5f)] [SerializeField] private float distance;
    [Tooltip("高さ"),Range(0f,2f)] [SerializeField] private float height;
    [Tooltip("飛行時間")] [SerializeField, Min(0.01f)] private float throwingDuration = 0.6f;
    [Tooltip("放物線の高さ変化")] [SerializeField] private AnimationCurve arcCurve;
    [Tooltip("速度変化。縦軸が速度倍率")] [SerializeField] private AnimationCurve widthCurve;
    
    [Header("毒ガス設定")]
    [Tooltip("毒ガス残留時間")] [SerializeField] private float duration;
    [Tooltip("ダメージ")] [SerializeField] private float damage;
    [Tooltip("ダメージを受ける間隔")] [SerializeField] private float hitInterval;
    
    void Start()
    {
        var direction = Vector3.right;

        if (!TryGetComponent(out PoisonController controller))
        {
            Debug.LogWarning("PrefabにPoisonControllerがアタッチされていません");
            return;
        }

        controller.Init(
            distance, height, 
            duration, damage, hitInterval, direction, throwingDuration,
            arcCurve, widthCurve);
    }
}