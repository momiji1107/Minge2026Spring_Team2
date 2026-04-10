using UnityEngine;

[CreateAssetMenu(fileName = "ShootSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/ShootSpeedUpgrade")]
public class ShootSpeedUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("弾速の増加量")] private float shootSpeed;

    void OnEnable()
    {
        titleName = "だんそくUP";
        infoSentence = $"だんそくが\n{shootSpeed} 上がる";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(PlayerSelection.selectedCharacter == CharacterName.PLAYER_ONE) return false;
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.ShootSpeedUp(shootSpeed);
    }
}
