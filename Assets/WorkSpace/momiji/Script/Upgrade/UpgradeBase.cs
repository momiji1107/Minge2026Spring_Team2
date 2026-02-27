using UnityEngine;

public abstract class UpgradeBase : ScriptableObject
{
    public string name;
    public abstract bool CanAppear(PlayerEquipmentManager equipmentManager);
    public abstract void Apply(PlayerEquipmentManager equipmentManager);
}
