using UnityEngine;

public class CharacterEngine
{
    private CharacterSettings _settings;

    public CharacterEngine(CharacterSettings settings)
    {
        _settings = settings;
    }

    public void LateUpdate()
    {
        if (!_settings.IsEnabled) return;

        CheckGround();
        UpdateSmoothedMovementDirection();
        //ApplyIdle();
        ApplyGravity();
        //ApplyDodging(); 
        //ApplyJumping(); 
        //ApplyAirborn();  
        ApplyMovement();
        ApplyRotation();
        ResetIfGround();
    }
    private void UpdateSmoothedMovementDirection()
    {
        var forward = Vector3.forward;

        bool isSelfForward = (_settings.IsDodging || _settings.IsAttacking) && _settings.ForwardAxis.magnitude == 0;

        if (_settings.MovementSpace != null && !isSelfForward && !_settings.IsHitting)
            forward = _settings.MovementSpace.TransformDirection(forward);

        forward.y = 0;
        forward = forward.normalized;

        var right = new Vector3(forward.z, 0, -forward.x);

        var v = _settings.MovementAxis.y;
        var h = _settings.MovementAxis.x;

        if (_settings.IsAttacking || _settings.IsHitting)
        {
            v = _settings.ForwardAxis.y;
            h = _settings.ForwardAxis.x;
        }
        if (isSelfForward)
        {
            v = _settings.Transform.forward.z;
            h = _settings.Transform.forward.x;
        }

        //if (v < -0.2)
        //    _settings.IsMovingBack = true;
        //else
        //    _settings.IsMovingBack = false;

        _settings.IsMoving = Mathf.Abs(h) > 0.1 || Mathf.Abs(v) > 0.1;
        var targetDirection = h * right + v * forward;

        if (_settings.IsGrounded)
        {
            _settings.TimeSinceLastMove += Time.deltaTime;
            if (_settings.IsMoving)
                _settings.TimeSinceLastMove = 0.0f;

            if (targetDirection != Vector3.zero)
            {
                _settings.MoveDirection = Vector3.RotateTowards(_settings.MoveDirection, targetDirection,
                    _settings.RotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
                _settings.MoveDirection = _settings.MoveDirection.normalized;
            }

            var curSmooth = _settings.SpeedSmoothing * Time.deltaTime;
            var targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

            if (_settings.IsDodging)
            {
                targetSpeed *= 7f;
            }
            else if(_settings.IsSprinting)
            {
                targetSpeed *= _settings.SprintSpeed;
            }
            else if (_settings.IsWalking || _settings.IsAttacking)
            {
                targetSpeed *= _settings.WalkSpeed;
            }
            else
            {
                targetSpeed *= _settings.RunSpeed;
            }

            _settings.MoveSpeed = Mathf.Lerp(_settings.MoveSpeed, targetSpeed, curSmooth);
            //if (_settings.MoveSpeed > 0.01f && !_settings.WaitAnimationFinish &&
            //    _settings.CharacterState != CharacterStateEnum.IsMoving)
            //    _settings.CharacterState = CharacterStateEnum.IsMovingStarted;
            if (_settings.StopMoveTime > 0.0f)
            {
                _settings.MoveSpeed = 0;
            }
            _settings.IsStrafing = (_settings.IsLookAt && _settings.MoveSpeed < 8f);
        }
        else
        {
            if (_settings.CharacterState == CharacterStateEnum.JumpStarted)
                _settings.TimeSinceLastMove = 0.0f;

            if (targetDirection != Vector3.zero)
            {
                _settings.MoveDirection = Vector3.RotateTowards(_settings.MoveDirection, targetDirection,
                    _settings.RotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

                _settings.MoveDirection = _settings.MoveDirection.normalized;
            }

            if (_settings.IsMoving)
                _settings.InAirVelocity +=
                    targetDirection.normalized * Time.deltaTime * _settings.InAirControlAcceleration;
        }
    }
    private void ApplyGravity()
    {
        if (_settings.IsEnabled)
        {
            _settings.IsJumpingReachedApex = _settings.VerticalSpeed <= 0.0;

            if (_settings.IsGrounded)
                _settings.VerticalSpeed = 0.0f;
            else
                _settings.VerticalSpeed -= _settings.Gravity * Time.deltaTime;
        }
    }
    private float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        return Mathf.Sqrt(2 * targetJumpHeight * _settings.Gravity);
    }
    private void ResetIfGround()
    {
        if (_settings.IsGrounded)
        {
            _settings.AirTime = 0;
            _settings.InAirVelocity = Vector3.zero;
        }
        else
            _settings.AirTime += Time.deltaTime;
    }
    private void ApplyRotation()
    {
        if (_settings.MoveDirection != Vector3.zero && !_settings.IsHitting)
        {
            if (_settings.IsStrafing)
            {
                Vector3 dir = _settings.CameraLookAt.transform.forward;
                dir.y = 0;
                _settings.Transform.rotation = Quaternion.LookRotation(dir);
            }
            else
            {
                _settings.Transform.rotation = Quaternion.LookRotation(_settings.MoveDirection);
            }
        }
    }
    private void ApplyMovement()
    {
        Physics.Raycast(_settings.Transform.position, -_settings.Transform.up, out var hit, 3);
        var dir = Vector3.ProjectOnPlane(_settings.MoveDirection, hit.normal);

        var SlopeForward = Vector3.Cross(_settings.Transform.right, hit.normal);
        var angle = SlopeForward.y < 0
            ? -Vector3.Angle(_settings.MoveDirection, dir)
            : Vector3.Angle(_settings.MoveDirection, dir);

        if (angle > 0)
            dir = Vector3.ProjectOnPlane(_settings.MoveDirection, hit.normal);

        float strafSpeed = 1;
        if (_settings.IsStrafing && !_settings.IsWalking)
        {
            strafSpeed *= 0.8f;
        }

        var move = dir * _settings.MoveSpeed * strafSpeed;

        if (angle > 45)
            move = Vector3.zero;

        var movement = move + new Vector3(0, _settings.VerticalSpeed, 0) +
                           _settings.InAirVelocity;

        movement *= Time.deltaTime;

        _settings.CollisionFlags = _settings.Controller.Move(movement);
    }
    private void CheckGround()
    {
        _settings.IsGrounded = (_settings.CollisionFlags & CollisionFlags.CollidedBelow) != 0;

        Vector3 pos = _settings.Transform.position;
        Vector3 canterPos = new Vector3(pos.x, pos.y - _settings.GroundedOffset, pos.z);
        _settings.InGround = Physics.CheckSphere(canterPos, _settings.GroundedRadius, _settings.GroundLayers,
            QueryTriggerInteraction.Ignore);

        if (_settings.InGround)
        {
            CheckGroundIncline();
        }
    }
    private void CheckGroundIncline()
    {
        if (_settings.FrontRayPos == null) { return; }
        float rayDistance = Mathf.Infinity;
        _settings.RearRayPos.rotation = Quaternion.Euler(_settings.Transform.rotation.x, 0, 0);
        _settings.FrontRayPos.rotation = Quaternion.Euler(_settings.Transform.rotation.x, 0, 0);

        Physics.Raycast(
            _settings.RearRayPos.position,
            _settings.RearRayPos.TransformDirection(-Vector3.up),
            out RaycastHit rearHit,
            rayDistance,
             _settings.GroundLayers);
        Physics.Raycast(
            _settings.FrontRayPos.position,
            _settings.FrontRayPos.TransformDirection(-Vector3.up),
            out RaycastHit frontHit,
            rayDistance,
            _settings.GroundLayers
        );

        Vector3 hitDifference = frontHit.point - rearHit.point;
        float xPlaneLength = new Vector2(hitDifference.x, hitDifference.z).magnitude;

        _settings.InclineAngle = Mathf.Lerp(_settings.InclineAngle, Mathf.Atan2(hitDifference.y, xPlaneLength) * Mathf.Rad2Deg, 20f * Time.deltaTime);
    }

    private void ApplyIdle()
    {
        if (_settings.Controller.velocity.sqrMagnitude < 0.1f && Mathf.Abs(_settings.VerticalSpeed) < 0.01f &&
            _settings.CharacterState != CharacterStateEnum.IdleInProgress)
            _settings.CharacterState = CharacterStateEnum.IdleStarted;

        if (_settings.CharacterState != CharacterStateEnum.IdleLongStared &&
            _settings.TimeSinceLastMove > _settings.NextLongIdleAnimationTime)
        {
            _settings.NextLongIdleAnimationTime = Random.Range(4, 8);
            _settings.CharacterState = CharacterStateEnum.IdleLongStared;
            _settings.TimeSinceLastMove = 0;
        }
    }
    private void ApplyAirborn()
    {
        if (_settings.CharacterState == CharacterStateEnum.JumpStarted)
            return;

        if (_settings.distanceToGround <= 0.1f &&
            _settings.CharacterState == CharacterStateEnum.InAir &&
            _settings.IsJumpingReachedApex &&
            _settings.CharacterState != CharacterStateEnum.LandInProgress)
            _settings.CharacterState = CharacterStateEnum.LandStarted;

        if (_settings.AirTime > 0.4f)
            if (_settings.CharacterState != CharacterStateEnum.InAir &&
                _settings.CharacterState != CharacterStateEnum.LandInProgress)
                _settings.CharacterState = CharacterStateEnum.InAirStarted;

        if (_settings.CharacterState == CharacterStateEnum.InAir &&
            _settings.distanceToGround >= 0.3f)
            _settings.LandingSpeed = _settings.VerticalSpeed;

    }
    private void ApplyJumping()
    {
        if (_settings.IsAttacking) { return; }

        if (_settings.IsGrounded)
        {
            if (Time.time < _settings.LastJumpButtonTime + _settings.JumpTimeout)
            {
                _settings.VerticalSpeed = CalculateJumpVerticalSpeed(_settings.JumpHeight);
                _settings.CharacterState = CharacterStateEnum.JumpStarted;
            }
        }
    }
    private void ApplyDodging()
    {
        if (!_settings.IsGrounded) { return; }

        if (_settings.IsGrounded)
        {
            if (Time.time < _settings.LastDodgeButtonTime + _settings.JumpTimeout)
            {
                _settings.CharacterState = CharacterStateEnum.DodgeStarted;
            }
        }

        if (_settings.CharacterState == CharacterStateEnum.DodgeInProgress &&
            _settings.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            _settings.IsDodging = false;
        }
    }

}