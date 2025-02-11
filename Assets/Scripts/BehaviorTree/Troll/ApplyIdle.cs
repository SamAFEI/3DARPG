using UnityEngine;

public class ApplyIdle : BTNode
{
    protected CharacterSettings _settings;
    public ApplyIdle(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.IdleTime > 0)
        {
            if (_settings.CharacterState != CharacterStateEnum.IdleInProgress)
            {
                _settings.CharacterState = CharacterStateEnum.IdleStarted;
                _settings.Animator.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            _settings.StopMoveTime = 1f;
            _settings.MovementAxis = Vector2.zero;
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}
