using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("--- Must set up manually ---")]
    public Transform spawnPoint;
    public List<SpawnArea> enemySpawnAreas;
    public int enemyCountLimit = 10;


    [Header("--- Read only ---")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject corruptedEnemyPrefab;
    public float difficultyPoint;
    public float actualDifficultyPoint = 0;
    public float roomIndex;
    [SerializeField] private List<Enemy> enemiesAlive;
    [SerializeField] private bool active = false;
    public bool cleared = false;
    public bool isCorrupted = false;
    public CopiedStats copiedStats;
    public int possessedHp = 0;
    private BulletManager bulletManager;
    private int enemiesToSpawn = 0;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        bulletManager = BulletManager.Instance;
    }

    public void SetEnemyPrefabs(List<GameObject> enemies)
    {
        enemyPrefabs = enemies;
    }

    public void RecordCurrentStats(CopiedStats stats)
    {
        copiedStats = stats;
    }

    public void SpawnEnemies()
    {
        if (!cleared)
        {
            if (isCorrupted)
            {
                //spawn corrupted enemy
                active = true;
                CorruptedEnemy enemy = Instantiate(corruptedEnemyPrefab, GetRandomSpawnPoint(), Quaternion.identity, transform)
                        .GetComponent<CorruptedEnemy>();
                if (possessedHp != 0)
                {
                    enemy.currentHp = possessedHp;
                }
                enemiesAlive.Add(enemy);
                enemy.SetAssociatedRoom(this);
                enemy.copiedStats = copiedStats;

                gameManager.currentGameState = GameState.Battling;
                

            }

            //spawn normal enemies
            int currentEnemyCount = 0;
            active = true;
            if (enemiesToSpawn == 0)
            {
                while (actualDifficultyPoint < difficultyPoint && enemyCountLimit > currentEnemyCount)
                {
                    SpawnRandomEnemy();
                    currentEnemyCount++;
                }
            }
            else
            {
                while (enemiesToSpawn > 0)
                {
                    SpawnRandomEnemy();
                    enemiesToSpawn--;
                }
            }
            
            gameManager.currentGameState = GameState.Battling;
        }
    }

    private void SpawnRandomEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], GetRandomSpawnPoint(), Quaternion.identity, transform)
                    .GetComponent<Enemy>();
        actualDifficultyPoint += enemy.enemyStats.difficultyPoint;
        enemiesAlive.Add(enemy);
        enemy.SetAssociatedRoom(this);
    }

    public void EnemyKilled(Enemy enemy)
    {
        if (enemiesAlive.Contains(enemy))
        {
            if (enemy.canBeCleanedUp)
                actualDifficultyPoint -= enemy.enemyStats.difficultyPoint;
            enemiesAlive.Remove(enemy);
            if (enemiesAlive.Count == 0)
                OnAllEnemiesKilled();
        }
    }

    private void OnAllEnemiesKilled()
    {
        if (active && 
            gameManager.currentGameState == GameState.Battling)
        {
            Debug.Log("Room cleared");
            gameManager.currentGameState = GameState.Upgrading;
            active = false;
            cleared = true;
        }
    }

    public void CleanUpRoom()
    {
        int noOfEnemiesAlive = enemiesAlive.Count;
        for (int i = 0; i < noOfEnemiesAlive; i++)
        {
            if (enemiesAlive[i].canBeCleanedUp)
            {
                Destroy(enemiesAlive[i].gameObject);
                enemiesToSpawn++;
            }
            else
            {
                possessedHp = enemiesAlive[i].hp;
                Destroy(enemiesAlive[i].gameObject);
            }
        }
        enemiesAlive.Clear();
        bulletManager.ClearBullets(0f);
    }

    private Vector2 GetRandomSpawnPoint()
    {
        SpawnArea spawnArea = enemySpawnAreas[Random.Range(0, enemySpawnAreas.Count)];
        Vector2 spawnPoint = (Vector2)spawnArea.transform.position +
            new Vector2(Random.Range(-(spawnArea.widthHeight.x / 2), spawnArea.widthHeight.x / 2),
                        Random.Range(-(spawnArea.widthHeight.y / 2), spawnArea.widthHeight.y / 2));
        return spawnPoint;
    }
}
