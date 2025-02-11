
using UnityEngine;

public class ApplyLanding : BTNode
{
    private CharacterSettings _settings;
    public ApplyLanding(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.LandingSpeed < -3f)
        {
            if (_settings.distanceToGround <= 0.3f && _settings.IsJumpingReachedApex &&
                _settings.CharacterState == CharacterStateEnum.InAir)
            {
                _settings.CharacterState = CharacterStateEnum.LandStarted;
                AudioManager.PlayOnPoint(AudioManager.SESource, _settings.SE_FootSetpLand, _settings.Transform.position);
                return BTNodeStates.SUCCESS;
            }

            if (_settings.CharacterState == CharacterStateEnum.LandInProgress)
            {
                if (_settings.LandingSpeed < -10 && _settings.InGround)
                {
                    _settings.StopMoveTime = 0.3f;
                }
                if (_settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump_EndBlend") &&
                    _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
                {
                    _settings.LandingSpeed = 0;
                }
                return BTNodeStates.SUCCESS;
            }
        }
        return BTNodeStates.FAILURE;
    }
}