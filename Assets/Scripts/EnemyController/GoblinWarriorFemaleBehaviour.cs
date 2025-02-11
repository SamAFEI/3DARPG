using System.Collections.Generic;
using UnityEngine;

public class GoblinWarriorFemaleBehaviour : MonoBehaviour
{
    private CharacterSettings _settings;

    //Behaviors
    private Selector rootState;
    private Selector OnGroundedState;

    public GoblinWarriorFemaleBehaviour(CharacterSettings settings)
    {
        _settings = settings;
        InitBehaviors();
    }

    public void InitBehaviors()
    {
        OnGroundedState = new Selector(new List<BTNode>
        {
            new ApplyLanding(_settings),
            new ApplyAttacking(_settings),
            new ApplyDodging(_settings),
            new ApplyJumping(_settings),
            new ApplyMoving(_settings),

        });

        rootState = new Selector(new List<BTNode>
        {
            new ApplyInAir(_settings),
            OnGroundedState,
        });
    }

    public void Update()
    {
        _settings.NextAttackTime -= Time.deltaTime;
        _settings.StopMoveTime -= Time.deltaTime;
        if (!_settings.IsAttacking)
        { _settings.AttackIndex = 1; }
        rootState.Evaluate();
    }
}
