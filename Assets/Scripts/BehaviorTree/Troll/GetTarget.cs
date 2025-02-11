using UnityEngine;

public class GetTarget : BTNode
{
    protected CharacterSettings _settings;
    public GetTarget(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {
        if (!_settings.IsAttacking)
        {
            _settings.Target = GameObject.FindAnyObjectByType<PlayerInputs>().gameObject;
            _settings.ActionTarget = _settings.Target.transform.position;
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }

}
