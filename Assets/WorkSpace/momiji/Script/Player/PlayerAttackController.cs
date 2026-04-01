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
    
    //クールタイムを計測する用のタイマー、timer[0]は通常攻撃用、それ以外はスキル用
    private float[] timers;

    void Start()
    {
        timers = new float[skills.Count + 1];
        
        for(int i = 0; i < timers.Length; i++)
        {
            timers[i] = 0f;
        }
    }
    void Update()
    {
        //クールタイム計測用タイマーに時間を加算する
        for(int i = 0; i < timers.Length; i++)
        {
            timers[i] += Time.deltaTime;
        }
        
        //Spaceキーを押すと通常攻撃
        if (Input.GetKey(KeyCode.Space) && timers[0] >= model.RapidFireSpeed + basicAttack.coolTime)
        {
            basicAttack.Activate(model);
            timers[0] = 0f;
        }

        //Xキーを押すとスキル１を使用する
        if (Input.GetKeyDown(KeyCode.X) && timers[1] >= model.RapidFireSpeed + Skills[0].coolTime)
        {
            Skills[0].Activate(model);
            timers[1] = 0f;
        }

        //Cキーを押すとスキル２を使用する
        if (Input.GetKeyDown(KeyCode.C) && timers[2] >= model.RapidFireSpeed + Skills[1].coolTime)
        {
            Skills[1].Activate(model);
            timers[2] = 0f;
        }

        //Vキーを押すとスキル３を使用する
        if (Input.GetKeyDown(KeyCode.V) && timers[3] >= model.RapidFireSpeed + Skills[2].coolTime)
        {
            Skills[2].Activate(model);
            timers[3] = 0f;
        }
    }

    public bool HasSkill(string skillName)
    {
        foreach (var skill in skills)
        {
            if(skill.name == skillName) return true;
        }
        
        return false;
    }
}
