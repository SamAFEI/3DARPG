using UnityEngine;

public class ApplyAttacking : BTNode
{
    protected CharacterSettings _settings;
    public ApplyAttacking(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.IsDodging) { return BTNodeStates.FAILURE; }
        if (Time.time < _settings.LastAttackButtonTime + _settings.JumpTimeout)
        {
            _settings.NextAttackTime = 0.3f;
        }
        if (_settings.NextAttackTime > 0 || _settings.IsAttacking)
        {
            _settings.IsAttacking = true;
            if (_settings.CharacterState != CharacterStateEnum.AttackInProgress)
            {
                _settings.ForwardAxis = _settings.MovementAxis;
                //if (_settings.MoveSpeed >= (_settings.SprintSpeed - 1) && _settings.IsSprinting)
                //{
                //    _settings.AttackIndex = 4;
                //    _settings.ForwardAxis = Vector2.zero;
                //}
                _settings.CharacterState = CharacterStateEnum.AttackStarted;
            }
            if (_settings.CharacterState == CharacterStateEnum.AttackInProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName(_settings.AttackingAnimationName 
                                                                        + _settings.AttackIndex) &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.StopMoveTime = 0.2f;
                if (_settings.NextAttackTime > 0)
                {
                    NextAttackIndex();
                    _settings.NextAttackTime = 0;
                    _settings.ForwardAxis = _settings.MovementAxis;
                    _settings.CharacterState = CharacterStateEnum.AttackStarted;
                }
                else
                {
                    _settings.StopMoveTime = 0f;
                    _settings.MoveSpeed = 0f;
                    _settings.ForwardAxis = Vector2.zero;
                    _settings.IsAttacking = false; 
                }
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }

    private void NextAttackIndex()
    {
        _settings.AttackIndex++;
        if (_settings.AttackIndex > 3)
        {
            _settings.AttackIndex = 1;
        }
    }
}