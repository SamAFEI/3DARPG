using UnityEngine;

public class ApplyTrollJumpAttack : BTNode
{
    protected CharacterSettings _settings;
    public ApplyTrollJumpAttack(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.AttackingState == AttackStateEnum.None &&
            _settings.LastJumpAttackTime < 0 &&
            _settings.DistanceToTarget < 10f)
        {
            if (_settings.CharacterState != CharacterStateEnum.JumpAttackInProgress)
            {
                _settings.CharacterState = CharacterStateEnum.JumpAttackStarted;
                _settings.AttackingState = AttackStateEnum.JumpAttack;
                _settings.IsAttacking = true;
            }
        }
        if (_settings.AttackingState == AttackStateEnum.JumpAttack)
        {
            _settings.IsSprinting = true;
            if (_settings.CharacterState == CharacterStateEnum.JumpAttackInProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.MovementAxis = Vector2.zero;
                _settings.IsSprinting = false;
                _settings.IsAttacking = false;
                _settings.StopMoveTime = 0.5f;
                _settings.AttackingState = AttackStateEnum.None;
                _settings.IdleTime = Random.Range(0f, 2f);
                _settings.LastJumpAttackTime = Random.Range(30f, 50f);
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}
