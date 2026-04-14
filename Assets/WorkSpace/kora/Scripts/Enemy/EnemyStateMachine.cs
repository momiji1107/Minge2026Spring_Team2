using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IEnemyStateMachine
{
    public void Init(EnemyData data, EnemyCore core, EnemyContext context);
    public void Tick(float dt);
    public void OnHitPlayer(Collider2D other);
    public void SetIsRignt(bool isRight);
}

public class EnemyStateMachine : IEnemyStateMachine
{
    private EnemyState currentState;
    
    public virtual void Init(EnemyData data, EnemyCore core, EnemyContext context)
    {
        var normal = (NormalEnemyData)data;
        currentState = new EnemyState();

        foreach (var so in normal.behaviours)
        {
            var b = so.Create();
            b.Init(normal, core, context);
            currentState.Behaviours.Add(b);
        }
    }
    
    public virtual void Tick(float dt)
    {
        foreach (var b in currentState.Behaviours)
        {
            b.Tick(dt);
        }
    }

    public virtual void OnHitPlayer(Collider2D other)
    {
        foreach (var b in currentState.Behaviours)
        {
            b.OnHitPlayer(other);
        }
    }

    public virtual void SetIsRignt(bool isRight)
    {
        foreach (var b in currentState.Behaviours)
        {
            b.SetIsRight(isRight);
        }
    }
    
    public class EnemyState
    {
        public List<EnemyBehaviourBase> Behaviours = new List<EnemyBehaviourBase>();
    }
}

public class EnemyBossStateMachine : IEnemyStateMachine
{
    public class BossState
    {
        public float ChangeHpPer = 0f;
        public List<BossBehaviourBaseSO> BossBehaviours = new List<BossBehaviourBaseSO>();
    }

    private List<BossState> _states = new List<BossState>();
    private BossState _currentState = new BossState();
    private EnemyCore _core;
    
    private int _maxHp;
    
    public void Init(EnemyData data, EnemyCore core, EnemyContext context)
    {
        BossData boss = (BossData)data;
        //phases, statesをhpPercentの降順で持つ
        var phases = boss.phases
            .OrderByDescending(p => p.hpPercent)
            .ToList();
        _core = core;
        
        _maxHp = _core.GetMaxHp();

        foreach (var phase in phases)
        {
            var state = new BossState();
            
            foreach (var so in phase.behaviours)
            {
                so.Init(core, context);
                state.BossBehaviours.Add(so);
            }

            // ％から割合に変換
            state.ChangeHpPer = phase.hpPercent / 100f;
            
            _states.Add(state);
        }

        //降順なのでindex=0を割り当て
        _currentState = _states[0];
    }

    public void Tick(float dt)
    {
        var hp = _core.GetHp();
        
        foreach (var state in _states)
        {
            var changeHp = _maxHp * state.ChangeHpPer;
            if (hp < changeHp
                && _currentState.ChangeHpPer > state.ChangeHpPer)
            {
                Debug.Log("Change state! hp < " +  changeHp);
                _currentState = state;
                foreach (var so in _currentState.BossBehaviours)
                {
                    Debug.Log("SO: " + so);
                }
                break;
            }
        }

        foreach (var so in _currentState.BossBehaviours)
        {
            so.Tick(dt);
        }
    }

    public void OnHitPlayer(Collider2D other)
    {
        foreach (var so in _currentState.BossBehaviours)
        {
            so.OnHitPlayer(other);
        }
    }

    public void SetIsRignt(bool isRight)
    {
        foreach (var so in _currentState.BossBehaviours)
        {
            so.SetIsRight(isRight);
        }
    }
}
