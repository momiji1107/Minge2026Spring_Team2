using UnityEngine;

public abstract class UpgradeBase : ScriptableObject
{
    public string name;
    
    //アップグレードに表示できるかどうか
    public abstract bool CanAppear(PlayerEquipmentManager equipmentManager);
    
    //アップグレードを反映させる
    public abstract void Apply(PlayerEquipmentManager equipmentManager);
}
