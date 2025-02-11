using System.Collections.Generic;
using UnityEngine;

public class TrollBehaviour
{
    private CharacterSettings _settings;

    //Behaviors
    private Selector rootState;
    private Sequence doChase;
    private Selector doAttack;

    public TrollBehaviour(CharacterSettings settings)
    {
        _settings = settings;
        InitBehaviors();
    }

    public void InitBehaviors()
    {
        doChase = new Sequence(new List<BTNode> 
        {
            new GetTarget(_settings),
            new ApplyMoveToTarget(_settings),
        });
        doAttack = new Selector(new List<BTNode>
        {
            new ApplyTrollJumpAttack(_settings),
            new ApplyTrollAttack4(_settings),
            new ApplyTrollAttack3(_settings),
            new ApplyTrollAttack2(_settings),
            new ApplyTrollAttack1(_settings),
        });
        rootState = new Selector(new List<BTNode>
        {
            new ApplyDeath(_settings),
            new ApplyIdle(_settings),
            doAttack,
            doChase,
        });
    }

    public void Update()
    {
        _settings.NextAttackTime -= Time.deltaTime;
        _settings.StopMoveTime -= Time.deltaTime;
        _settings.IdleTime -= Time.deltaTime;
        _settings.FaceTargetTime -= Time.deltaTime;
        _settings.LastAttack1Time -= Time.deltaTime;
        _settings.LastAttack2Time -= Time.deltaTime;
        _settings.LastAttack3Time -= Time.deltaTime;
        _settings.LastAttack4Time -= Time.deltaTime;
        _settings.LastJumpAttackTime -= Time.deltaTime;
        _settings.LastFootStepTime -= Time.deltaTime;
        rootState.Evaluate();
        if (Input.GetKey(KeyCode.Alpha2))
        {
            _settings.IsAttacking = false;
            _settings.AttackingState = AttackStateEnum.None;
        }
    }
}
