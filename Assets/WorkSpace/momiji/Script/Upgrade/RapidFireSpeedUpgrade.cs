using UnityEngine;

[CreateAssetMenu(fileName = "RapidFireSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/RapidFireSpeedUpgrade")]
public class RapidFireSpeedUpgrade : UpgradeBase
{
    [SerializeField] private float rapidFireSpeed;
    
    void OnEnable()
    {
        titleName = "クールタイム短縮";
        infoSentence = $"通常こうげきの\nクールタイムが\n{rapidFireSpeed}s 早くなる";
    }
    
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(PlayerSelection.selectedCharacter == CharacterName.PLAYER_TWO) return false;
        else if(equipmentManager.Model.RapidFireSpeed > rapidFireSpeed) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.RapidFireSpeedUp(rapidFireSpeed);
    }
}
