using UnityEngine;

public class ApplyTrollAttack3 : BTNode
{
    protected CharacterSettings _settings;
    public ApplyTrollAttack3(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.AttackingState == AttackStateEnum.None &&
            _settings.LastAttack3Time < 0 &&
            _settings.DistanceToTarget < 5f)
        {
            if (_settings.CharacterState != CharacterStateEnum.Attack3InProgress)
            {
                _settings.CharacterState = CharacterStateEnum.Attack3Started;
                _settings.AttackingState = AttackStateEnum.Attack3;
                _settings.IsAttacking = true;
            }
        }
        if (_settings.AttackingState == AttackStateEnum.Attack3)
        {
            _settings.IsSprinting = true;
            if (_settings.CharacterState == CharacterStateEnum.Attack3InProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.MovementAxis = Vector2.zero;
                _settings.IsSprinting = false;
                _settings.IsAttacking = false;
                _settings.StopMoveTime = 0.5f;
                _settings.AttackingState = AttackStateEnum.None;
                _settings.IdleTime = Random.Range(0f, 3f);
                _settings.LastAttack3Time = Random.Range(10f, 20f);
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}
