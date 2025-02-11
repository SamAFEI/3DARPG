using UnityEngine;

public class ApplyTrollAttack2 : BTNode
{
    protected CharacterSettings _settings;
    public ApplyTrollAttack2(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.AttackingState == AttackStateEnum.None &&
            _settings.LastAttack2Time < 0 &&
            _settings.DistanceToTarget < 5f)
        {
            if (_settings.CharacterState != CharacterStateEnum.Attack2InProgress)
            {
                _settings.CharacterState = CharacterStateEnum.Attack2Started;
                _settings.AttackingState = AttackStateEnum.Attack2;
                _settings.IsAttacking = true;
            }
        }
        if (_settings.AttackingState == AttackStateEnum.Attack2)
        {
            Vector3 _targetDirection = (_settings.Target.transform.position - _settings.Transform.position).normalized;
            _settings.ForwardAxis.y = _targetDirection.z;
            _settings.ForwardAxis.x = _targetDirection.x;
            _settings.IsWalking = true;
            if (_settings.CharacterState == CharacterStateEnum.Attack2InProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.IsWalking = false;
                _settings.MovementAxis = Vector2.zero;
                _settings.IsAttacking = false;
                _settings.StopMoveTime = 0.5f;
                _settings.AttackingState = AttackStateEnum.None;
                _settings.IdleTime = Random.Range(0f, 2f);
                _settings.LastAttack2Time = Random.Range(1f, 5f);
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}
