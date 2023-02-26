using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterScriptableObject", order = 2)]
public class CharacterScriptableObject : ScriptableObject
{
    public string characterName;
    public int baseHp;
    public float baseSanity = 30f;
    public int baseDamage;
    public float baseSpeed;
    public float baseCooldown;
    public float baseSkillValue;
    public float baseAttackDelay;
    public float baseSpread;
    public int baseBulletCount;
    public float baseBulletSpeed;
}
