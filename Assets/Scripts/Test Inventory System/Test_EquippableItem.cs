using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Buff,
    Debuff
}

[CreateAssetMenu]
public class Test_EquippableItem : Test_Item
{
    public float HealthBonus;
    public float DamageBonus;
    [Space]
    public float HealthPercentBonus;
    public float DamagePercentBonus;
    [Space]
    public EquipmentType EquipmentType;
}
