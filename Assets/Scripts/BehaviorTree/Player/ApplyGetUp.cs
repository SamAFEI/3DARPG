using UnityEngine;

public class ApplyGetUp : BTNode
{
    private CharacterSettings _settings;
    public ApplyGetUp(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.IsKnockDowning && _settings.KnockDownTime <= 0)
        {
            if (_settings.CharacterState == CharacterStateEnum.KnockDownInProgress)
            {
                _settings.CharacterState = CharacterStateEnum.GetUpStarted;
            }
            _settings.MovementAxis = Vector2.zero;
            if (_settings.CharacterState == CharacterStateEnum.GetUpInProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("GetUp") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.IsHitting = false;
                _settings.IsKnockDowning = false;
                _settings.IsMovingBack = false;
                _settings.IsSprinting = false;
                _settings.StopMoveTime = 0;
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}