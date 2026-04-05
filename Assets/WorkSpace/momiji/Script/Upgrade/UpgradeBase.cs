using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeBase : ScriptableObject
{
    public string name;
    //public string titleName;
    //[Multiline] public string infoSentence;
    public Sprite icon;
    
    //アップグレードに表示できるかどうか
    public abstract bool CanAppear(PlayerEquipmentManager equipmentManager);
    
    //アップグレードを反映させる
    public abstract void Apply(PlayerEquipmentManager equipmentManager);
}
