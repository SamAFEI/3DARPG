using System;

[Serializable]
public class CharacterBehaviour
{
    protected CharacterSettings _settings;

    public CharacterBehaviour(CharacterSettings settings)
    {
        _settings = settings;
        InitBehaviors();
    }
    public virtual void InitBehaviors()
    {

    }

    public virtual void Update()
    {

    }
}
