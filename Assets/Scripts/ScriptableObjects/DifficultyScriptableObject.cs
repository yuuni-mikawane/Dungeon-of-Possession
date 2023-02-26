using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DifficultyScriptableObject", order = 1)]
public class DifficultyScriptableObject : ScriptableObject
{
    public string difficultyName;
    public int floorCount;
    public int maxEnemyWaveCountPerFloor = 1;
    public int minEnemyWaveCountPerFloor = 1;
    public float enemyDifficultyPoint;
    public float enemyDifficultyMultiplier;
    public float corruptedEnemyAttackDelayScale;
}