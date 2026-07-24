using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct AmountData
{
    public float amount;
    public int weight;
}

public abstract class UpgradeBase : ScriptableObject
{
    public string titleName;
    [Multiline] public string infoSentence;
    public Sprite icon;
    [Range(0, 10)] public int rarity; //出現度、0(表示されにくい)<--->10(表示されやすい)

    //アップグレード内容の更新
    public virtual void UpdateUpgrade(){}
    
    //アップグレードに表示できるかどうか
    public abstract bool CanAppear(PlayerEquipmentManager equipmentManager);
    
    //アップグレードを反映させる
    public abstract void Apply(PlayerEquipmentManager equipmentManager);
}
