using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageReduction", menuName = "ScriptableObjects/Skill/DamageReduction")]
public class DamageReduction : EquipmentBase
{
    [Header("ƒ_ƒ[ƒWŒyŒ¸Œø‰ÊŽžŠÔ")]
    [SerializeField] private int duration;
    [SerializeField] private bool isActive;

    public bool IsActive => isActive;

    public override void Activate(PlayerModel model)
    {
        model.StartCoroutine(ActiveDamageReduce());
    }

    private IEnumerator ActiveDamageReduce()
    {
        isActive = true;
        yield return new WaitForSeconds(duration);
        isActive = false;
    }
}
