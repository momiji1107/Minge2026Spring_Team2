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

    /*[Header("見た目")] 
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private string moveAnim;
    [SerializeField] private string attackAnim;*/
    
    //getter
    public int FirstMaxHp => firstMaxHp;
    public int FirstAttack => firstAttack;
    public float FirstMoveSpeed => firstMoveSpeed;
    public float FirstShootSpeed => firstShootSpeed;
    public float FirstRapidFireSpeed => firstRapidFireSpeed;
    
    /*public Sprite PlayerSprite => playerSprite;
    public Animator Animator => animator;
    public string MoveAnim => moveAnim;
    public string AttackAnim => attackAnim;*/
}
