using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAnimator
{
    private CharacterSettings _settings;

    public CharacterAnimator(CharacterSettings settings)
    {
        _settings = settings;
        _settings.Animator.CrossFade(_settings.IdleAnimationName, _settings.TransitionTime, 0);
    }

    public void Update()
    {
        if (!_settings.Animator)
            return;

        if (_settings.CharacterState == CharacterStateEnum.LandStarted)
        {
            _settings.Animator.speed = 1;
            _settings.CharacterState = CharacterStateEnum.LandInProgress;
            _settings.Animator.CrossFade(_settings.JumpEndAnimationName, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.InAirStarted)
        {
            _settings.Animator.speed = 1;
            _settings.CharacterState = CharacterStateEnum.InAir;
            _settings.Animator.CrossFade(_settings.JumpPoseAnimationName, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.JumpStarted && !_settings.IsJumpingReachedApex)
        {
            _settings.CharacterState = CharacterStateEnum.InAir;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade(_settings.JumpStartAnimationName, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.DodgeStarted)
        {
            _settings.CharacterState = CharacterStateEnum.DodgeInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade(_settings.DodgingAnimationName, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.AttackStarted)
        {
            _settings.CharacterState = CharacterStateEnum.AttackInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade(_settings.AttackingAnimationName + _settings.AttackIndex, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.IdleLongStared)
        {
            _settings.CharacterState = CharacterStateEnum.IdleInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade(_settings.IdleLongAnimationName, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.IdleStarted)
        {
            _settings.CharacterState = CharacterStateEnum.IdleInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade(_settings.IdleAnimationName, _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.IsMovingStarted)
        {
            _settings.Animator.CrossFade(_settings.MovementAnimationName, _settings.TransitionTime, 0);
            _settings.CharacterState = CharacterStateEnum.IsMoving;
        }

        if (_settings.CharacterState == CharacterStateEnum.IsMoving)
        {
            //var normalize = _settings.MoveSpeed / _settings.SprintSpeed;
            //_settings.Animator.SetFloat(_settings.MovementSpeed, normalize);
            //_settings.Animator.speed = Mathf.Clamp(_settings.Controller.velocity.magnitude,
            //    0.0f,
            //    _settings.MoveSpeed <= _settings.WalkSpeed
            //        ? Mathf.Clamp(_settings.MoveSpeed / _settings.WalkSpeed, 0.5f, 1)
            //        : 1);
        }

        if (_settings.CharacterState == CharacterStateEnum.DeathStarted)
        {
            _settings.CharacterState = CharacterStateEnum.DeathInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("Death", _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.HitStarted)
        {
            _settings.CharacterState = CharacterStateEnum.HitInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("Hit", _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.KnockDownStarted)
        {
            _settings.CharacterState = CharacterStateEnum.KnockDownInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("KnockDown", _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.GetUpStarted)
        {
            _settings.CharacterState = CharacterStateEnum.GetUpInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("GetUp", _settings.TransitionTime, 0);
        }

        // Enemy
        if (_settings.CharacterState == CharacterStateEnum.Attack1Started)
        {
            _settings.CharacterState = CharacterStateEnum.Attack1InProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("Attack1", _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.Attack2Started)
        {
            _settings.CharacterState = CharacterStateEnum.Attack2InProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("Attack2", _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.Attack3Started)
        {
            _settings.CharacterState = CharacterStateEnum.Attack3InProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("Attack3", _settings.TransitionTime, 0);
        }

        if (_settings.CharacterState == CharacterStateEnum.Attack4Started)
        {
            _settings.CharacterState = CharacterStateEnum.Attack4InProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("Attack4", _settings.TransitionTime, 0);
        }
        if (_settings.CharacterState == CharacterStateEnum.JumpAttackStarted)
        {
            _settings.CharacterState = CharacterStateEnum.JumpAttackInProgress;
            _settings.Animator.speed = 1;
            _settings.Animator.CrossFade("JumpAttack", _settings.TransitionTime, 0);
        }

        Vector3 characterForward = new Vector3(_settings.Transform.forward.x, 0f, _settings.Transform.forward.z).normalized;
        Vector3 characterRight = new Vector3(_settings.Transform.right.x, 0f, _settings.Transform.right.z).normalized;
        Vector3 directionForward = new Vector3(_settings.MoveDirection.x, 0f, _settings.MoveDirection.z).normalized;
        _settings.StrafeDirection.z = Vector3.Dot(characterForward, directionForward);
        _settings.StrafeDirection.x = Vector3.Dot(characterRight, directionForward);
        _settings.Animator.SetFloat("IsStrafing", _settings.IsStrafing ? 1.0f : 0.0f);
        _settings.Animator.SetFloat("StrafeDirectionX", _settings.StrafeDirection.x);
        _settings.Animator.SetFloat("StrafeDirectionZ", _settings.StrafeDirection.z); 
        _settings.Animator.SetFloat("InclineAngle", _settings.InclineAngle);
        _settings.Animator.SetFloat("MovementSpeed", _settings.MoveSpeed);
        if (_settings.CharacterState == CharacterStateEnum.InAir)
            _settings.Animator.SetFloat("LandingSpeed", _settings.LandingSpeed);
    }
}