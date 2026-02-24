using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    
    [Header("攻撃方法")]
    [SerializeField] private EquipmentBase basicAttack;
    [SerializeField] private List<EquipmentBase> skills;
    
    public List<EquipmentBase> Skills => skills; //getter
    
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
        basicAttackTimer += Time.deltaTime;
        skill1Timer += Time.deltaTime;
        skill2Timer += Time.deltaTime;
        skill3Timer += Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space) && basicAttackTimer >= basicAttack.coolTime)
        {
            basicAttack.Activate(model);
            basicAttackTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.X) && skill1Timer >= Skills[0].coolTime)
        {
            Skills[0].Activate(model);
            skill1Timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.C) && skill2Timer >= Skills[1].coolTime)
        {
            Skills[1].Activate(model);
            skill2Timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.V) && skill3Timer >= Skills[2].coolTime)
        {
            Skills[2].Activate(model);
            skill3Timer = 0f;
        }
    }
    
}
