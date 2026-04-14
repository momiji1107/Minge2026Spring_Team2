using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("初期ステータス")]
    [SerializeField] private int firstMaxHp;
    [SerializeField] private int firstAttack;
    [SerializeField] private float firstMoveSpeed;
    [SerializeField] private float firstShootSpeed;
    [SerializeField] private float firstRapidFireSpeed;
    [SerializeField] private EquipmentBase basicAttack;

    [Header("見た目")] 
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private RuntimeAnimatorController animator;
    
    [Header("サウンド")]
    [SerializeField] private AudioClip attackClip;
    
    //getter
    public int FirstMaxHp => firstMaxHp;
    public int FirstAttack => firstAttack;
    public float FirstMoveSpeed => firstMoveSpeed;
    public float FirstShootSpeed => firstShootSpeed;
    public float FirstRapidFireSpeed => firstRapidFireSpeed;

    public EquipmentBase BasicAttack => basicAttack;

    public Sprite PlayerSprite => playerSprite;
    public RuntimeAnimatorController Animator => animator;

    public AudioClip AttackClip => attackClip;
}
