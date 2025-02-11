using UnityEngine;

public class ApplyDodging : BTNode
{

    protected CharacterSettings _settings;
    public ApplyDodging(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (Time.time < _settings.LastDodgeButtonTime + _settings.JumpTimeout && !_settings.IsSprinting &&
            _settings.CharacterState != CharacterStateEnum.DodgeInProgress)
        {
            _settings.CharacterState = CharacterStateEnum.DodgeStarted;
            _settings.IsDodging = true; 
            _settings.StopMoveTime = 0f;
            _settings.ForwardAxis = _settings.MovementAxis;
        }
        if (_settings.IsDodging)
        {
            if (_settings.CharacterState == CharacterStateEnum.DodgeInProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName(_settings.DodgingAnimationName) &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.MoveSpeed = 0;
                _settings.StopMoveTime = 0f;
                _settings.IsDodging = false;
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}