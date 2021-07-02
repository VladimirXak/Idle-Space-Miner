using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ArmamentSavedData
{
    public List<WeaponSavedData> Weapons;
}

[Serializable]
public struct WeaponSavedData
{
    public int Id;
    public int Level;
}
