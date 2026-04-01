using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageNegate", menuName = "ScriptableObjects/Skill/DamageNegate")]
public class DamageNegate : EquipmentBase
{
    [Header("ƒ_ƒ[ƒW–³Œø‰»ŽžŠÔ")]
    [SerializeField] private int duration;
    [SerializeField] private bool isActive;

    public bool IsActive => isActive;

    public override void Activate(PlayerModel model)
    {
        model.StartCoroutine(ActiveDamageNegate());
    }

    private IEnumerator ActiveDamageNegate()
    {
        isActive = true;
        yield return new WaitForSeconds(duration);
        isActive = false;
    }
}
