using UnityEngine;

[CreateAssetMenu(fileName = "HPrecover", menuName = "ScriptableObjects/Skill/HPrecover")]

public class HPrecover : EquipmentBase

{
    [Header("�񕜗�")]
    [SerializeField] private int healAmount;
    [SerializeField] private AudioClip healClip;
    private AudioSource audioSource;
  
    public override void Activate(PlayerModel model)
    {
        audioSource = model.GetComponent<AudioSource>();
        audioSource.PlayOneShot(healClip);
        model.HpRecovery(healAmount);
    }
}
