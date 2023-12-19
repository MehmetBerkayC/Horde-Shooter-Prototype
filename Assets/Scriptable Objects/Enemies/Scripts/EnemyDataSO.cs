using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Enemy Data", menuName ="Scriptable Objects/Enemies/Enemy Data")] 
public class EnemyDataSO : ScriptableObject
{
    public Sprite Sprite;
    public int BaseHealth;
    public int BaseDamage;
    public float BaseMovementSpeed;
    public int BaseExperience;
}
