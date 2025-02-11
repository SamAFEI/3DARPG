using UnityEngine;

public class ApplyTrollAttack4 : BTNode
{
    protected CharacterSettings _settings;
    public ApplyTrollAttack4(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (_settings.AttackingState == AttackStateEnum.None &&
            _settings.LastAttack4Time < 0 &&
            _settings.SimpleDirectionToActionTarget.z < 0f &&
            Mathf.Abs(_settings.SimpleDirectionToActionTarget.x) < 2f &&
            _settings.DistanceToTarget < 3f)
        {
            if (_settings.CharacterState != CharacterStateEnum.Attack4InProgress)
            {
                _settings.CharacterState = CharacterStateEnum.Attack4Started;
                _settings.AttackingState = AttackStateEnum.Attack4;
                _settings.IsAttacking = true;
                _settings.MovementAxis = Vector2.zero;
                _settings.Animator.applyRootMotion = true;
            }
        }
        if (_settings.AttackingState == AttackStateEnum.Attack4)
        {
            _settings.StopMoveTime = 0.3f;
            if (_settings.CharacterState == CharacterStateEnum.Attack4InProgress &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4") &&
                _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.90f)
            {
                _settings.MovementAxis = Vector2.zero;
                _settings.MoveDirection = Vector3.zero;
                _settings.Transform.Rotate(0, 180, 0);
                _settings.Animator.transform.Rotate(0, 180, 0);
                _settings.Animator.applyRootMotion = false;
                _settings.IsAttacking = false;
                _settings.StopMoveTime = 0.5f;
                _settings.AttackingState = AttackStateEnum.None;
                _settings.IdleTime = Random.Range(0f, 1f);
                _settings.LastAttack4Time = Random.Range(1f, 3f);
            }
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}
