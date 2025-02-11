using UnityEngine;

public class ApplyJumping : BTNode
{
    protected CharacterSettings _settings;
    public ApplyJumping(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (Time.time < _settings.LastJumpButtonTime + _settings.JumpTimeout)
        {
            _settings.VerticalSpeed = CalculateJumpVerticalSpeed(_settings.JumpHeight);
            _settings.CharacterState = CharacterStateEnum.JumpStarted;
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }

    private float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        return Mathf.Sqrt(2 * targetJumpHeight * _settings.Gravity);
    }

}