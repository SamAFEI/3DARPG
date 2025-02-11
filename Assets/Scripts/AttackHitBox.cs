using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public Transform ForceTransform;
    public bool IsKnockDown;
    public float Damage;
    public float Power;
    public ForceDirectionEnum ForceEnum;
    public Vector3 ForceDirection;
    public Vector3 HitPoint;

    public void Awake()
    {
        if (ForceTransform == null)
        { ForceTransform = this.transform; }
    }

    protected void OnTriggerEnter(Collider other)
    {
        HitPoint = other.ClosestPoint(transform.position); 
        SetForceDirection();
        other.SendMessage("ApplyHit", this, SendMessageOptions.DontRequireReceiver);
    }

    protected virtual void SetForceDirection() 
    { 
        switch (ForceEnum)
        {
            case ForceDirectionEnum.Forward:
                ForceDirection = ForceTransform.forward; 
                break;
            case ForceDirectionEnum.Right:
                ForceDirection = ForceTransform.right;
                break;
            case ForceDirectionEnum.Left:
                ForceDirection = -ForceTransform.right;
                break;
            case ForceDirectionEnum.Up:
                ForceDirection = ForceTransform.up;
                break;
        }
    }
}

public enum ForceDirectionEnum
{
    SelfBack, Forward, Right, Left, Up,
}
