using UnityEngine;

// 毒瓶を投げるスキル
[CreateAssetMenu(fileName = "Poison", menuName = "ScriptableObjects/Skill/Poison")]
public class Poison : EquipmentBase
{
    [Header("毒Prefab")][SerializeField] private GameObject prefab;

    [Header("投てき設定")]
    [Tooltip("飛距離"),Range(0f,5f)] [SerializeField] private float distance;
    [Tooltip("高さ"),Range(0f,2f)] [SerializeField] private float height;
    [Tooltip("飛行時間")] [SerializeField, Min(0.01f)] private float throwingDuration = 0.6f;
    [Tooltip("放物線の高さ変化")] [SerializeField] private AnimationCurve arcCurve = new AnimationCurve(
        new Keyframe(0f, 0f),
        new Keyframe(0.5f, 1f),
        new Keyframe(1f, 0f)
    );
    [Tooltip("速度変化。縦軸が速度倍率")] [SerializeField] private AnimationCurve widthCurve = new AnimationCurve(
        new Keyframe(0f, 1f),
        new Keyframe(1f, 1f)
    );
    
    [Header("毒ガス設定")]
    [Tooltip("毒ガス残留時間")] [SerializeField] private float duration;
    [Tooltip("ダメージ")] [SerializeField] private float damage;
    [Tooltip("ダメージを受ける間隔")] [SerializeField] private float hitInterval;

    [Header("scaleをx倍にする")] [SerializeField] private float scaleMultiplier;
    
    public override void Activate(PlayerModel model)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefabが設定されていません");
            return;
        }

        var obj = Instantiate(prefab, model.transform.position, Quaternion.identity);
        var direction = model.GetDirection ? Vector3.right : Vector3.left;

        if (!obj.TryGetComponent(out PoisonController controller))
        {
            Debug.LogWarning("PrefabにPoisonControllerがアタッチされていません");
            return;
        }

        obj.transform.localScale *= scaleMultiplier;

        controller.Init(
            distance, height, 
            duration, damage, hitInterval, direction, throwingDuration,
            arcCurve, widthCurve
            );
    }
}
