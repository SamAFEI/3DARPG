using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine.TargetTracking;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerInputs : MonoBehaviour
{
    private CharacterSettings _settings => GetComponent<RoleController>().settings;
    public Transform StartPoint;
    public AttackHitBox HitBox;
    public bool IsFireSword;
    public GameObject FX_FireSword;
    public Transform CameraRoot;
    public Transform CameraLookAt;
    public Transform CameraFollow;
    public Transform CameraLookFront;
    public float LastRestCameraTime;

    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool dodge;
    public bool sprint;
    public bool walk;
    public bool attack;
    public bool thump;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public List<Transform> lookTargets = new List<Transform>();
    public int LookAtIndex;

    #region Input
#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value)
    {
        Move(value.Get<Vector2>());
    }
    public void OnLook(InputValue value)
    {
        CameraManager.SetInputGain(Gamepad.current != null);
        if (cursorInputForLook)
        {
            Look(value.Get<Vector2>());
        }
    }
    public void OnLookAt(InputValue value)
    {
        LookAt(value.isPressed);
    }
    public void OnPrevious(InputValue value)
    {
        LookAtNext(-1);
    }
    public void OnNext(InputValue value)
    {
        LookAtNext(1);
    }
    public void OnJump(InputValue value)
    {
        Jump(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        Sprint(value.isPressed);
    }
    public void OnDodge(InputValue value)
    {
        Dodge(value.isPressed);
    }
    public void OnWalk(InputValue value)
    {
        Walk(value.isPressed);
    }
    public void OnAttack(InputValue value)
    {
        Attack(value.isPressed);
    }
    public void OnThump(InputValue value)
    {
        Thump(value.isPressed);
    }
    public void OnAttackLeft(InputValue value)
    {
        AttackLeft(value.isPressed);
    }
    public void OnHome(InputValue value)
    {
        Home(value.isPressed);
    }
#endif
    #endregion

    private void Awake()
    {
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            Debug.Log("輸入來自控制器（手把）");
            CameraManager.SetInputGain(Gamepad.current != null);
        };
    }
    private void Start()
    {
        FX_FireSword.SetActive(IsFireSword);
        _settings.LookTarget = CameraRoot;
    }
    private void Update()
    {
        LastRestCameraTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _settings.ResetState();
        }
        SearchLookTarget();
        ApplyLockedOn();
    }

    #region Action
    public void Move(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
        _settings.MovementAxis = move;
        if (!walk && move.magnitude > 0)
        {
            _settings.IsWalking = move.magnitude < 0.5f;
        }
    }
    public void Look(Vector2 newLookDirection)
    {
        look = newLookDirection;
        //CameraManager.OnCameraInputAxis(look != Vector2.zero);
    }
    public void LookAt(bool newlookAtState)
    {
        if (!_settings.IsLookAt)
        {
            if (lookTargets.Count > 0)
            {
                _settings.LookTarget = lookTargets[0];
                _settings.IsLookAt = true;
                CameraManager.ApplyInputController(false);
                CameraManager.SetBindingMode(BindingMode.LockToTargetWithWorldUp);
            }
            else
            {
                _settings.LookTarget = CameraLookFront;
                _settings.IsLookAt = false;
                CameraManager.ApplyInputController(false);
                CameraManager.SetBindingMode(BindingMode.LockToTargetWithWorldUp);
                LastRestCameraTime = 0.5f;
            }
        }
        else
        {
            _settings.LookTarget = CameraRoot;
            _settings.IsLookAt = false;
        }
    }
    public void LookAtNext(int value)
    {
        if (!_settings.IsLookAt) { return; }
        LookAtIndex += value;
        if (LookAtIndex < 0)
        {
            LookAtIndex = lookTargets.Count - 1;
        }
        if (LookAtIndex >= lookTargets.Count)
        {
            LookAtIndex = 0;
        }
        if (lookTargets.Count > 0)
        {
            _settings.LookTarget = lookTargets[LookAtIndex];
        }
    }
    public void Jump(bool newJumpState)
    {
        jump = newJumpState;
        _settings.LastJumpButtonTime = Time.time;
    }
    public void Dodge(bool newDodgeState)
    {
        dodge = newDodgeState;
        _settings.LastDodgeButtonTime = Time.time;
    }
    public void Sprint(bool newSprintState)
    {
        sprint = newSprintState;
        _settings.SprintToggleTime = Time.time;
        _settings.IsSprinting = sprint && _settings.LastDodgeButtonTime < _settings.SprintToggleTime;
    }
    public void Walk(bool newWalkState)
    {
        walk = newWalkState;
        _settings.IsWalking = walk;
    }
    public void Attack(bool newAttackState)
    {
        attack = newAttackState;
        _settings.LastAttackButtonTime = Time.time;
    }
    public void Thump(bool newThumpState)
    {
        thump = newThumpState;
    }
    public void AttackLeft(bool newAttackLeftState)
    {
        IsFireSword = !IsFireSword;

        if (IsFireSword)
            HitBox.Damage = 100;
        else
            HitBox.Damage = 10;
        FX_FireSword.SetActive(IsFireSword);
    }
    public void Home(bool newHomeState)
    {
        //transform.GetComponent<CharacterController>().enabled = false;
        //transform.position = StartPoint.position;
        //transform.GetComponent<CharacterController>().enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    #region LockedOn
    private void SearchLookTarget()
    {
        List<Collider> hits = Physics.OverlapSphere(transform.position, 20f,
                                                    _settings.EnemyLayer, QueryTriggerInteraction.Ignore).ToList();
        lookTargets.Clear();
        foreach (Collider hit in hits)
        {
            Vector3 dir = (hit.transform.position - transform.position).normalized;
            float dis = (hit.transform.position - transform.position).magnitude;
            bool _isObscured = false;

            // CharacterController & Collider 有offset時 Raycast會打到 
            // LayerMask everyone => ~0
            if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit _hit, dis, ~0, QueryTriggerInteraction.Ignore))
            {
                if (_hit.collider != hit)
                {
                    _isObscured = true;
                }
            }
            if (!_isObscured && IsVisibleFrom(hit.bounds, Camera.main) &&
                lookTargets.Where(x => x.name == hit.name).FirstOrDefault() == null)
            {
                Transform _transform = hit.transform;
                if (hit.TryGetComponent(out TrollController troll))
                {
                    _transform = troll.settings.CameraRoot;
                }
                lookTargets.Add(_transform);
            }
        }

        if (_settings.IsLookAt && lookTargets.Count == 0)
        {
            LookAt(false);
        }
    }
    private bool IsVisibleFrom(Bounds bounds, Camera camera)
    {
        //獲取Camera的錐體的六個平面
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        //判斷是否在平面數組內(或與任一面相交)
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
    private void SetCameraLookAtTransform()
    {
        Vector3 dir = (_settings.LookTarget.position - CameraRoot.position).normalized * 0.5f;
        CameraLookAt.position = CameraRoot.position + dir;
        CameraLookAt.LookAt(_settings.LookTarget);
    }
    private void ApplyLockedOn()
    {
        CameraFollow.LookAt(_settings.LookTarget);
        if (_settings.LookTarget != null)
        {
            SetCameraLookAtTransform();
            CameraManager.SetIM_LockedPostion(_settings.LookTarget.position);
        }
        CameraManager.Instance.IM_Locked.enabled = _settings.IsLookAt;
        if (!_settings.IsLookAt && LastRestCameraTime < 0f)
        {
            _settings.LookTarget = CameraRoot;
            CameraFollow.localRotation = Quaternion.identity;
            CameraManager.ApplyInputController(true);
            CameraManager.SetBindingMode(BindingMode.LazyFollow);
        }
    }
    #endregion
}