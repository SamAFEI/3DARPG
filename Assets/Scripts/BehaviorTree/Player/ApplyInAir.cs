using UnityEngine;

public class ApplyInAir : BTNode
{

    protected CharacterSettings _settings;
    public ApplyInAir(CharacterSettings settings)
    {
        _settings = settings;
    }

    public override BTNodeStates Evaluate()
    {        
        //if (_settings.AirTime > 0.2f && _settings.distanceToGround > 0.3f)
        //if (!_settings.IsGrounded)
        if (!_settings.InGround)
        {
            if (_settings.CharacterState != CharacterStateEnum.JumpStarted && 
                _settings.CharacterState != CharacterStateEnum.InAir && _settings.AirTime > 0.3f)
            {
                _settings.IsAttacking = false;
                _settings.IsDodging = false;
                _settings.CharacterState = CharacterStateEnum.InAirStarted;
            }
            _settings.LandingSpeed = _settings.VerticalSpeed;
            return BTNodeStates.SUCCESS;
        }
        return BTNodeStates.FAILURE;
    }
}