using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GoblinWarriorFemaleController : MonoBehaviour
{
    public CharacterSettings settings; //Init by OnValidate
    private CharacterAnimator _characterAnimator;
    private CharacterEngine _characterEngine;
    private GoblinWarriorFemaleBehaviour _characterBehaviour;
    private void Awake()
    {
        _characterAnimator = new CharacterAnimator(settings);
        _characterEngine = new CharacterEngine(settings);
        _characterBehaviour = new GoblinWarriorFemaleBehaviour(settings);
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

    private void InitCharacterSettings()
    {
        settings.Controller = GetComponent<CharacterController>();
        settings.Animator = GetComponentInChildren<Animator>();
        settings.Transform = this.transform;
    }

    private void OnDrawGizmos()
    {
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
    }
}
