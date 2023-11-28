using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Enemy Data", menuName ="Scriptable Objects/Enemies/Enemy Data")] 
public class EnemyDataSO : ScriptableObject
{
    public Sprite sprite;
    public int Health;
    public int Damage;
    public float Speed;

}
