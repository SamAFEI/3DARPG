using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBehaviour
{
    //Behaviors
    private Selector rootState;
    private Selector OnGroundedState;
    private Selector ApplyHit;

    public PlayerBehaviour(CharacterSettings settings) : base(settings)
    {
    }

    public override void InitBehaviors()
    {
        OnGroundedState = new Selector(new List<BTNode>
        {
            new ApplyGetUp(_settings),
            new ApplyLanding(_settings),
            new ApplyAttacking(_settings),
            new ApplyDodging(_settings),
            new ApplyJumping(_settings),
            new ApplyMoving(_settings),

        });

        ApplyHit = new Selector(new List<BTNode> 
        { 
            new ApplyDeath(_settings),
            new ApplyKnockDown(_settings),
            new ApplyHit(_settings),
        });

        rootState = new Selector(new List<BTNode>
        {
            ApplyHit,
            new ApplyInAir(_settings),
            OnGroundedState,
        });
    }

    public override void Update()
    {
        _settings.NextAttackTime -= Time.deltaTime;
        _settings.StopMoveTime -= Time.deltaTime;
        _settings.KnockDownTime -= Time.deltaTime;
        _settings.LastFootStepTime -= Time.deltaTime;
        if (!_settings.IsAttacking)
        { _settings.AttackIndex = 1; }
        rootState.Evaluate();
    }
}