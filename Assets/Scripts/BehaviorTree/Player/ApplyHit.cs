using UnityEngine;

public class ApplyHit : BTNode
{
    private CharacterSettings _settings;
    public ApplyHit(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.IsHitting && !_settings.IsKnockDowning)
        {
            if (_settings.CharacterState != CharacterStateEnum.HitInProgress)
            {
                _settings.CharacterState = CharacterStateEnum.HitStarted;
                _settings.StopMoveTime = 0.3f;
            }

            _settings.MovementAxis = Vector2.zero;

            if (_settings.CharacterState == CharacterStateEnum.HitInProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.IsHitting = false;
                _settings.StopMoveTime = 0;
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}