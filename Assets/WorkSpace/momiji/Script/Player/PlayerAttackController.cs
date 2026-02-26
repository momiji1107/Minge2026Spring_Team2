using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    
    [Header("攻撃方法")]
    [SerializeField] private EquipmentBase basicAttack;
    [SerializeField] private List<EquipmentBase> skills;

    //getterとsetter
    public EquipmentBase BasicAttack { get { return basicAttack; } set { basicAttack = value; } }
    public List<EquipmentBase> Skills{ get { return skills; } set { skills = value; } }
    
    //クールタイムを計測する用のタイマー
    private float basicAttackTimer;
    private float skill1Timer;
    private float skill2Timer;
    private float skill3Timer;

    void Start()
    {
        basicAttackTimer = 0f;
        skill1Timer = 0f;
        skill2Timer = 0f;
        skill3Timer = 0f;
    }
    void Update()
    {
        //クールタイム計測用タイマーに時間を加算する
        basicAttackTimer += Time.deltaTime;
        skill1Timer += Time.deltaTime;
        skill2Timer += Time.deltaTime;
        skill3Timer += Time.deltaTime;
        
        //Spaceキーを押すと通常攻撃
        if (Input.GetKey(KeyCode.Space) && basicAttackTimer >= basicAttack.coolTime)
        {
            basicAttack.Activate(model);
            basicAttackTimer = 0f;
        }

        //Xキーを押すとスキル１を使用する
        if (Input.GetKeyDown(KeyCode.X) && skill1Timer >= Skills[0].coolTime)
        {
            Skills[0].Activate(model);
            skill1Timer = 0f;
        }

        //Cキーを押すとスキル２を使用する
        if (Input.GetKeyDown(KeyCode.C) && skill2Timer >= Skills[1].coolTime)
        {
            Skills[1].Activate(model);
            skill2Timer = 0f;
        }

        //Vキーを押すとスキル３を使用する
        if (Input.GetKeyDown(KeyCode.V) && skill3Timer >= Skills[2].coolTime)
        {
            Skills[2].Activate(model);
            skill3Timer = 0f;
        }
    }
    
}
