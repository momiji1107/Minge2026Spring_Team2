using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeBase : ScriptableObject
{
    public string name;
    public Sprite icon;
    
    //アップグレードに表示できるかどうか
    public abstract bool CanAppear(PlayerEquipmentManager equipmentManager);
    
    //アップグレードを反映させる
    public abstract void Apply(PlayerEquipmentManager equipmentManager);
}
