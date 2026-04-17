using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerEquipmentManager equipmentManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SelectedPlayer selectedPlayer;
    
    [Header("攻撃方法")]
    [SerializeField] private EquipmentBase basicAttack;
    [SerializeField] private List<EquipmentBase> skills;
    
    [Header("クールタイム表示関係")]
    [SerializeField] private Slider[] coolTimeBar;
    //クールタイムを計測する用のタイマー、timer[0]は通常攻撃用、それ以外はスキル用
    private float[] timers;

    //getterとsetter
    public EquipmentBase BasicAttack { get { return basicAttack; } set { basicAttack = value; } }
    public List<EquipmentBase> Skills{ get { return skills; } set { skills = value; } }
    
    //Event
    public Action BasicAttackAnim;

    void Start()
    {
        timers = new float[equipmentManager.MaxSkillnum + 1];
        
        for(int i = 0; i < timers.Length; i++)
        {
            timers[i] = 100f;
            coolTimeBar[i].gameObject.SetActive(false);
        }

        basicAttack = selectedPlayer.PlayerData.BasicAttack;
    }
    void Update()
    {
        if (GameManagement.GameState != GAMESTATE.INGAME) return;
        
        //クールタイム計測用タイマーに時間を加算する
        for(int i = 0; i < timers.Length; i++)
        {
            timers[i] += Time.deltaTime;
        }
        
        //Spaceキーを押すと通常攻撃
        if (Input.GetKey(KeyCode.Space) && timers[0] >= model.RapidFireSpeed + basicAttack.coolTime)
        {
            BasicAttackAnim?.Invoke();
            audioManager.Attack(selectedPlayer.PlayerData.AttackClip);
            basicAttack.Activate(model);
            timers[0] = 0f;
        }

        //Xキーを押すとスキル１を使用する
        if (Input.GetKeyDown(KeyCode.X) && timers[1] >= model.RapidFireSpeed + skills[0]?.coolTime)
        {
            skills[0]?.Activate(model);
            timers[1] = 0f;
        }

        //Cキーを押すとスキル２を使用する
        if (Input.GetKeyDown(KeyCode.C) && timers[2] >= model.RapidFireSpeed + skills[1]?.coolTime)
        {
            skills[1]?.Activate(model);
            timers[2] = 0f;
        }

        //Vキーを押すとスキル３を使用する
        if (Input.GetKeyDown(KeyCode.V) && timers[3] >= model.RapidFireSpeed + skills[2]?.coolTime)
        {
            skills[2]?.Activate(model);
            timers[3] = 0f;
        }

        DisplayCoolTimeBar();
    }

    public bool HasSkill(string skillName)
    {
        foreach (var skill in skills)
        {
            if(skill.name == skillName) return true;
        }
        
        return false;
    }

    //クールタイムをSliderに表示させる
    private void DisplayCoolTimeBar()
    {
        for (int i = 0; i < skills.Count + 1; i++)
        {
            if (i == 0)
            {
                coolTimeBar[i].value = timers[i] / (model.RapidFireSpeed + basicAttack.coolTime);
                coolTimeBar[i].gameObject.SetActive(coolTimeBar[i].value < 1);
            }
            else
            {
                coolTimeBar[i].value = 1.0f - (timers[i] / (model.RapidFireSpeed + skills[i-1].coolTime));
                coolTimeBar[i].gameObject.SetActive(coolTimeBar[i].value > 0);
            }
        }
    }
}
