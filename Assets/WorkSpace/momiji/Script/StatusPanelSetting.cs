using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusPanelSetting : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [Header("Status表示UI")]
    [SerializeField] private Image[] hpHeart;
    [SerializeField] private TextMeshProUGUI basicAttackText;
    [SerializeField] private Image attackPowerBar;
    [SerializeField] private Image rapidFireSpeedBar;
    [SerializeField] private Image moveSpeedBar;
    
    [Header("初期ステータス上限")]
    [SerializeField] private int FirstAttackLimit = 20; //初期攻撃力の上限
    [SerializeField] private float FirstMoveSpeedLimit = 6f; //初期移動速度の上限
    [SerializeField] private float FirstShootSpeedLimit = 6f; //初期弾速の上限
    [SerializeField] private float FirstRapidFireSpeedLimit = 2f; //初期連射速度の上限
    //[SerializeField] private TextMeshProUGUI uniqueText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int hp = playerData.FirstMaxHp;
        for (int i = 0; i < hpHeart.Length; i++)
        {
            hpHeart[i].enabled = i < hp;
        }
        basicAttackText.text = playerData.BasicAttack.name;
        attackPowerBar.fillAmount = (float)playerData.FirstAttack / FirstAttackLimit;
        rapidFireSpeedBar.fillAmount = playerData.FirstRapidFireSpeed / FirstRapidFireSpeedLimit;
        moveSpeedBar.fillAmount = playerData.FirstMoveSpeed / FirstMoveSpeedLimit;
    }
    
}
