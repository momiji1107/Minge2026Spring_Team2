using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private PlayerAttackController attackController;
    [SerializeField] private int maxSkillnum = 3;

    public void AddSkill(EquipmentBase newSkill)
    {
        if (attackController.Skills.Count >= maxSkillnum) return;
        attackController.Skills.Add(newSkill);
    }
}
