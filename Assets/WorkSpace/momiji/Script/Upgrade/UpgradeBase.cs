using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeBase : ScriptableObject
{
    public string titleName;
    [Multiline] public string infoSentence;
    public Sprite icon;
    [Range(0, 10)] public int rarity; //出現度、0(表示されにくい)<--->10(表示されやすい)
    
    //アップグレードに表示できるかどうか
    public abstract bool CanAppear(PlayerEquipmentManager equipmentManager);
    
    //アップグレードを反映させる
    public abstract void Apply(PlayerEquipmentManager equipmentManager);
}
