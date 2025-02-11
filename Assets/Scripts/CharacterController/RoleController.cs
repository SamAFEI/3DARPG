using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class RoleController : MonoBehaviour
{
    public Slider SD_Health;
    public AudioClip SFX_Hit;
    public CharacterSettings settings; //Init by OnValidate
    private CharacterAnimator _characterAnimator;
    private CharacterEngine _characterEngine;
    private PlayerBehaviour _characterBehaviour;

    private void Awake()
    {
        _characterAnimator = new CharacterAnimator(settings);
        _characterEngine = new CharacterEngine(settings);
        _characterBehaviour = new PlayerBehaviour(settings);
    }

    private void Start()
    {
        settings.CurrentHp = settings.MaxHp;
        if (SD_Health != null)
        {
            SD_Health.maxValue = settings.MaxHp;
            SD_Health.value = settings.CurrentHp;
        }
    }

    private void Update()
    {
        _characterBehaviour.Update();
        _characterAnimator.Update();
    }

    private void LateUpdate()
    {
        _characterEngine.LateUpdate();
    }

    private void OnDestroy()
    {
        _characterAnimator = null;
        _characterEngine = null;
        _characterBehaviour = null;
    }

    private void OnValidate()
    {
        InitCharacterSettings();
    }

    private void OnDrawGizmos()
    {
        /*
        Vector3 pos = settings.Transform.position;
        Vector3 spherePosition = new Vector3(pos.x, pos.y - settings.GroundedOffset, pos.z);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spherePosition, settings.GroundedRadius);

        //spherePosition = new Vector3(pos.x, pos.y - settings.GroundedOffset, pos.z + settings.StairsOffset);
        Vector3 p1 = spherePosition + settings.Transform.forward * settings.StairsOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p1, settings.GroundedRadius);

        //spherePosition = new Vector3(pos.x, pos.y - settings.GroundedOffset, pos.z - settings.StairsOffset);
        Vector3 p2 = spherePosition - settings.Transform.forward * settings.StairsOffset;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(p2, settings.GroundedRadius);
        */
    }
    private void InitCharacterSettings()
    {
        settings.Controller = GetComponent<CharacterController>();
        //settings.Animator = GetComponentInChildren<Animator>();
        settings.Transform = this.transform;
        settings.characterLayer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Enemy");
    }

    /// <summary>
    /// AttackHitBox SeedMassage("ApllHit", this)
    /// </summary>
    /// <param name="hit"></param>
    private void ApplyHit(AttackHitBox hit)
    {
        if (settings.IsHitting || settings.IsDodging) { return; }
        settings.IsHitting = true;
        settings.IsAttacking = false;
        float intensity = 10f;
        if (hit.IsKnockDown)
        {
            intensity = 20f;
            settings.KnockDownTime = 1f;
        }
        settings.ForwardAxis.y = hit.ForceDirection.z;
        settings.ForwardAxis.x = hit.ForceDirection.x;
        if (hit.ForceEnum == ForceDirectionEnum.SelfBack)
        {
            Vector3 dir = (transform.position - hit.transform.position).normalized;
            settings.ForwardAxis.y = dir.z;
            settings.ForwardAxis.x = dir.x;
        }
        AudioManager.PlayOnPoint(AudioManager.SESource, SFX_Hit, transform.position);
        settings.CurrentHp = Mathf.Clamp(settings.CurrentHp - hit.Damage, 0f, settings.MaxHp);
        if (SD_Health != null)
        {
            DOTween.To(() => SD_Health.value, x => SD_Health.value = x, settings.CurrentHp, 0.5f);
        }
        if (settings.CurrentHp <= 0)
        {
            GameManager.ApplySlowTime(1f); //SlowTime 又 SharkCamera 會很晃
        }
        else
        {
            CameraManager.ApplyShark(intensity);
        }
    }
}
