using UnityEngine;

public class ApplyDeath : BTNode
{
    private CharacterSettings _settings;
    public ApplyDeath(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.CurrentHp <= 0)
        {
            if (_settings.CharacterState != CharacterStateEnum.DeathInProgress)
            {
                _settings.CharacterState = CharacterStateEnum.DeathStarted;
            }
            _settings.StopMoveTime = 0.3f;
            _settings.MovementAxis = Vector2.zero;
            _settings.Controller.excludeLayers = _settings.characterLayer;
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}