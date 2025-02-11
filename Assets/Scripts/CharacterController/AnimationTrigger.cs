using TMPro;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private TrollController _troll => GetComponentInParent<TrollController>();
    private RoleController _role => GetComponentInParent<RoleController>();
    private CharacterSettings _settings;

    public AudioClip SFX_Attack1;
    public AudioClip SFX_Attack2;
    public AudioClip SFX_Attack3;
    public AudioClip SFX_JumpAttack;
    public AudioClip SE_Dogde; 


    private void Start()
    {
        if (_troll != null)
            _settings = _troll.settings;
        else 
            _settings = _role.settings;
    }
    public void FinishTrigger()
    {
    }
    public void GetActionTarget()
    {
        _settings.ActionTarget = _settings.Target.transform.position;
    }

    public void MoveToActionTarget(int type = 0)
    {
        _settings.IsWalking = type == 1;
        _settings.IsSprinting = type == 2;
        Vector3 _targetDirection = (_settings.ActionTarget - _settings.Transform.position).normalized;
        _settings.MovementAxis.y = _targetDirection.z;
        _settings.MovementAxis.x = _targetDirection.x;
        _settings.StopMoveTime = 0f;
    }

    public void MoveToTarget(int type = 0)
    {
        _settings.IsWalking = type == 1;
        _settings.IsSprinting = type == 2;
        Vector3 _targetDirection = (_settings.Target.transform.position - _settings.Transform.position).normalized;
        _settings.ForwardAxis.y = _targetDirection.z;
        _settings.ForwardAxis.x = _targetDirection.x;
        _settings.MovementAxis = Vector2.zero;
        _settings.StopMoveTime = 0f;
    }

    public void FacingActionTarget()
    {
        Vector3 _targetDirection = (_settings.Target.transform.position - _settings.Transform.position).normalized;
        _settings.ForwardAxis.y = _targetDirection.z;
        _settings.ForwardAxis.x = _targetDirection.x;
        _settings.MovementAxis = Vector2.zero;
    }

    public void StopMove(float time = 1f)
    {
        _settings.IsWalking = false;
        _settings.IsSprinting = false;
        _settings.StopMoveTime = time;
        _settings.MovementAxis = Vector2.zero;
        _settings.ForwardAxis.y = _settings.Transform.forward.z;
        _settings.ForwardAxis.x = _settings.Transform.forward.x;
        //_settings.Animator.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void ApplyPS_JumpAttack()
    {
        _settings.PS_JumpAttack.Play();
    }

    public void ApplyRepel()
    {
        _settings.IsSprinting = true;
        _settings.IsMovingBack = true;
        _settings.StopMoveTime = 0f;
        //_settings.Animator.transform.Rotate(0, 180, 0);
    }

    public void ApplyShakeCamera(float intensity = 1f)
    {
        CameraManager.ApplyShark(intensity);
    }

    public void ApplyAttackSFX(int index)
    {
        AudioClip clip = null;
        if (index == 0) { clip = SFX_Attack1; }
        else if (index == 1) { clip = SFX_Attack2; }
        else if (index == 2) { clip = SFX_Attack3; }
        else if (index == 3) { clip = SFX_JumpAttack; }

        AudioManager.PlayOnPoint(AudioManager.SESource, clip, transform.position);
    }
    public void ApplySE_Dogde()
    {
        AudioManager.PlayOnPoint(AudioManager.SESource, SE_Dogde, transform.position);
    }
}
