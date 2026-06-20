using UnityEngine;

[CreateAssetMenu(fileName = "HPrecover", menuName = "ScriptableObjects/Skill/HPrecover")]

public class HPrecover : EquipmentBase

{
    [Header("�񕜗�")]
    [SerializeField] private int healAmount;
  
    public override void Activate(PlayerModel model)
    {
        model.HpRecovery(healAmount);
    }
}
