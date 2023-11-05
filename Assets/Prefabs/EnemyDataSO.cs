using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="EnemyDataSO",menuName ="Enemy Data")] 
public class EnemyDataSO : ScriptableObject
{
    public Sprite sprite;
    public float Health;
    public float Damage;
    public float Speed;
}
