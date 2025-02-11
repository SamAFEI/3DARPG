using System;
using UnityEngine;

[Serializable]
public class CharacterSettings
{
    public bool IsEnabled = true;
    public CharacterController Controller;
    public Animator Animator;
    public Transform Transform;
    public Transform CameraRoot;
    public Transform MovementSpace;
    public Transform LookTarget;
    public Transform RearRayPos;
    public Transform FrontRayPos;
    public Transform CameraLookAt;
    public LayerMask EnemyLayer;
    public ParticleSystem PS_JumpAttack;
    public AudioClip SE_FootSetpLand;
    public AudioClip SE_FootSetpRun;

    public float MaxHp = 500;

    [Header("Animation Name")]
    public string IdleAnimationName = "Idle";
    public string IdleLongAnimationName = "Idle";
    public string JumpStartAnimationName = "Jump_StartBlend";
    public string JumpPoseAnimationName = "Jump_Loop";
    public string JumpEndAnimationName = "Jump_EndBlend";
    public string MovementAnimationName = "MovementBlend";
    public string DodgingAnimationName = "Dodge";
    public string AttackingAnimationName = "Attack";

    public float StairsOffset = 0.5f;
    public float GroundedOffset = -0.1f;
    public float GroundedRadius = 0.15f;
    public LayerMask GroundLayers;
    public float WalkSpeed = 2.0f;
    public float RunSpeed = 5.0f;
    public float SprintSpeed = 10.0f;
    public float InAirControlAcceleration = 3.0f;
    public float JumpHeight = 0.8f;
    public float Gravity = 20.0f;
    public float SpeedSmoothing = 10.0f;
    public float RotateSpeed = 500.0f;
    public float JumpTimeout = 0.15f;
    public float SmoothTime = 0.1f;
    public float TransitionTime = 0.025f;
    public float NextLongIdleAnimationTime = 5;
    public float rotationSpeed = 360f;

    [Header("States")]
    public CharacterStateEnum CharacterState;
    public CollisionFlags CollisionFlags;
    public LayerMask characterLayer;
    public Vector3 MoveDirection = Vector3.zero;
    public Vector3 StrafeDirection = Vector3.zero;
    public float VerticalSpeed = 0.0f;
    public float LandingSpeed = 0.0f;
    public float MoveSpeed = 0.0f;
    public float TimeSinceLastMove = 0.0f;
    public float InclineAngle;
    public float AttackIndex = 1;
    public bool IsLookAt;
    public bool IsStrafing;
    public bool IsJumpingReachedApex = false;
    public bool IsMovingBack = false;
    public bool IsMoving = false;
    public bool InGround;
    public bool InForwardGround;
    public bool InBackGround;
    public bool IsGrounded;  //CollisionFlags 判定用
    public bool IsSprinting;
    public bool IsWalking;
    public bool IsHitting;
    public bool IsKnockDowning;
    public bool IsAttacking = false;
    public bool IsDodging = false;
    public float LastJumpButtonTime = -10.0f;
    public float LastAttackButtonTime = -10.0f;
    public float LastDodgeButtonTime = -10.0f;
    public float SprintToggleTime = 0.2f;
    public float NextAttackTime = -10.0f;
    public float StopMoveTime = -10.0f;
    public float KnockDownTime;
    public float LastJumpStartHeight = 0.0f;
    public float LastFootStepTime;
    public Vector3 InAirVelocity = Vector3.zero;
    public Vector2 MovementAxis = Vector2.zero;
    public Vector2 ForwardAxis = Vector2.zero;
    public Vector3 CurrentVelocity;
    public float AirTime = 0.0f;
    public bool AnimationFoldout = false;
    public bool DebugFoldout = false;
    public float CurrentHp;

    [Header("Enemy")]
    public GameObject Target;
    public Vector3 ActionTarget;
    public AttackStateEnum AttackingState;
    public float FaceTargetTime;
    public float IdleTime;
    public float LastAttack1Time;
    public float LastAttack2Time;
    public float LastAttack3Time;
    public float LastAttack4Time;
    public float LastJumpAttackTime;

    public float distanceToGround
    {
        get
        {
            if (Physics.Raycast(Transform.position, Vector3.down, out var hit, maxDistance: 5))
                return hit.distance;
            return int.MaxValue;
        }
    }
    public float DistanceToTarget
    {
        get
        {
            if (Target == null)
                return 0;
            return (Target.transform.position - Transform.position).magnitude;
        }
    }

    public Vector3 DirectionToActionTarget
    {
        get
        {
            return (ActionTarget - Transform.position);
        }
    }

    public Vector3 SimpleDirectionToActionTarget
    {
        get
        {
            float x = Vector3.Cross(Transform.forward, ActionTarget - Transform.position).y;
            float z = Vector3.Dot(Transform.forward, ActionTarget);

            return new Vector3(x, 0, z);
        }
    }

    public void ResetState()
    {
        IsAttacking = false;
        IsDodging = false;
        IsKnockDowning = false;
        IsHitting = false;
        IsMovingBack = false;
        CharacterState = CharacterStateEnum.IdleStarted;
        AttackingState = AttackStateEnum.None;
        MovementAxis = Vector2.zero;
        ForwardAxis = Vector2.zero;
    }
}
