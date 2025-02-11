using UnityEngine;

public class ApplyTrollAttack1 : BTNode
{
    protected CharacterSettings _settings;
    public ApplyTrollAttack1(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.AttackingState == AttackStateEnum.None &&
            _settings.LastAttack1Time < 0 &&
            _settings.DistanceToTarget < 3f)
        {
            if (_settings.CharacterState != CharacterStateEnum.Attack1InProgress)
            {
                _settings.CharacterState = CharacterStateEnum.Attack1Started;
                _settings.AttackingState = AttackStateEnum.Attack1;
                _settings.IsAttacking = true;
                _settings.FaceTargetTime = 1f;
            }
        }
        if (_settings.AttackingState == AttackStateEnum.Attack1)
        {
            Vector3 _targetDirection = (_settings.Target.transform.position - _settings.Transform.position).normalized;
            if (_settings.FaceTargetTime > 0f)
            {
                _settings.ForwardAxis.y = _targetDirection.z;
                _settings.ForwardAxis.x = _targetDirection.x;
            }
            else
            {
                _settings.StopMoveTime = 1.5f;
                _settings.MovementAxis = Vector2.zero;
            }
            if (_settings.CharacterState == CharacterStateEnum.Attack1InProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.MovementAxis = Vector2.zero;
                _settings.IsAttacking = false;
                _settings.AttackingState = AttackStateEnum.None;
                _settings.IdleTime = Random.Range(0f, 1f);
                _settings.LastAttack1Time = Random.Range(3f, 8f);

            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}
