using UnityEngine;

public class ApplyMoving : BTNode
{
    protected CharacterSettings _settings;
    public ApplyMoving(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.MoveSpeed > 0.01f)
        {
            if (_settings.CharacterState != CharacterStateEnum.IsMoving)
            {
                _settings.CharacterState = CharacterStateEnum.IsMovingStarted;
                _settings.LastFootStepTime = 0.2f;
                if (_settings.IsSprinting)
                {
                    _settings.LastFootStepTime = 0.15f;
                }
                else if (_settings.IsWalking)
                {
                    _settings.LastFootStepTime = 0.3f;
                }
            }
            if (_settings.LastFootStepTime < 0)
            {
                _settings.LastFootStepTime = 0.45f;
                if (_settings.IsSprinting)
                {
                    _settings.LastFootStepTime = 0.33f;
                }
                else if (_settings.IsWalking)
                {
                    _settings.LastFootStepTime = 0.63f;
                }
                AudioManager.PlayOnPoint(AudioManager.SESource, _settings.SE_FootSetpRun, _settings.Transform.position);
            }
            //return BTNodeStates.SUCCESS;
        }
        if (_settings.Controller.velocity.sqrMagnitude < 0.05f &&
                _settings.CharacterState != CharacterStateEnum.IdleInProgress)
        {
            _settings.CharacterState = CharacterStateEnum.IdleStarted;
            _settings.MoveDirection = Vector3.zero;
        }

        if (_settings.CharacterState != CharacterStateEnum.IdleLongStared &&
            _settings.TimeSinceLastMove > _settings.NextLongIdleAnimationTime)
        {
            _settings.NextLongIdleAnimationTime = Random.Range(4, 8);
            _settings.CharacterState = CharacterStateEnum.IdleLongStared;
            _settings.TimeSinceLastMove = 0;
        }
        return BTNodeStates.FAILURE;
    }

}