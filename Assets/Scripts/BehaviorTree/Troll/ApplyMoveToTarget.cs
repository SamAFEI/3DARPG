using UnityEngine;

public class ApplyMoveToTarget : BTNode
{
    protected CharacterSettings _settings;
    public ApplyMoveToTarget(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.DistanceToTarget > 3)
        {
            Vector3 _targetDirection = (_settings.Target.transform.position - _settings.Transform.position).normalized;
            _settings.MovementAxis.y = _targetDirection.z;
            _settings.MovementAxis.x = _targetDirection.x;
            if (_settings.CharacterState != CharacterStateEnum.IsMoving)
            {
                _settings.CharacterState = CharacterStateEnum.IsMovingStarted;
                _settings.LastFootStepTime = 0.7f;
            }
            ApplySE_FootStep();
            return BTNodeStates.SUCCESS;
        }
        /*
        if (_settings.CharacterState != CharacterStateEnum.IsMoving)
        {
            _settings.CharacterState = CharacterStateEnum.IsMovingStarted; 
            _settings.LastFootStepTime = 0.45f;
        }*/
        return BTNodeStates.FAILURE;
    }

    private void ApplySE_FootStep()
    {
        if (_settings.LastFootStepTime < 0)
        {
            _settings.LastFootStepTime = 0.7f;
            AudioManager.PlayOnPoint(AudioManager.SESource, _settings.SE_FootSetpRun, _settings.Transform.position);
        }
    }
}
