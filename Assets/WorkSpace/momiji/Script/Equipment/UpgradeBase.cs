using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeBase", menuName = "ScriptableObjects/UpgradeBase")]
public abstract class UpgradeBase : ScriptableObject
{
    public string name;
    public abstract bool CanAppear(PlayerModel model);
    public abstract void Apply(PlayerModel model);
}
