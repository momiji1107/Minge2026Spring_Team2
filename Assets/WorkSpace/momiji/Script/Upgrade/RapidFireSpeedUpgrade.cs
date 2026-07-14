using UnityEngine;

[CreateAssetMenu(fileName = "RapidFireSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/RapidFireSpeedUpgrade")]
public class RapidFireSpeedUpgrade : UpgradeBase
{
    [SerializeField] private float rapidFireSpeed;
    
    void OnEnable()
    {
        titleName = "こうげき速度UP";
        infoSentence = $"こうげきの\nクールタイムが\n{rapidFireSpeed}秒 早くなる";
    }
    
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(PlayerSelection.selectedCharacter == CharacterName.PLAYER_TWO) return false;
        else if(equipmentManager.Model.RapidFireSpeed > 0.5f) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.RapidFireSpeedUp(rapidFireSpeed);
    }
}
