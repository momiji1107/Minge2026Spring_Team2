using UnityEngine;

public abstract class EquipmentBase : ScriptableObject
{
    public string name; //技の名前
    public int level; //強化レベル
    [Tooltip("ステータスの連射速度に加算するクールタイム")]
    public float coolTime;
    public abstract void Activate(PlayerModel model);
}
