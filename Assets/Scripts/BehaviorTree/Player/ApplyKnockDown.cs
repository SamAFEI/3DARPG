using UnityEngine;

public class ApplyKnockDown : BTNode
{
    private CharacterSettings _settings;
    public ApplyKnockDown(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.IsHitting && _settings.KnockDownTime > 0)
        {
            if (_settings.CharacterState != CharacterStateEnum.KnockDownInProgress)
            {
                _settings.CharacterState = CharacterStateEnum.KnockDownStarted;
                _settings.IsKnockDowning = true;
                _settings.StopMoveTime = 0.3f;
            }
            _settings.MovementAxis = Vector2.zero;
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}