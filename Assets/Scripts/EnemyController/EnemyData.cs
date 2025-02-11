using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public NPCState StateEnum;
    public float IdleTime;
    public bool IsAlerting;
    public float Attack1Range;
    public float Attack2Range;
    public float Attack3Range;
    public float Attack4Range;
}
