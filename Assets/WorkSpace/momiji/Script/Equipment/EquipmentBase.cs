using UnityEngine;

public abstract class EquipmentBase : ScriptableObject
{
    public string name; //技の名前
    public int level; //強化レベル
    public float coolTime; //クールタイム
    public abstract void Activate(PlayerModel model);
}
