using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class LevelManager : SingletonBind<LevelManager>
{
    [Header("--- Must set up manually ---")]
    public List<DifficultyScriptableObject> allDifficulties;
    public DifficultyScriptableObject currentDifficulty;
    public List<GameObject> roomPrefabs;
    public List<Room> roomsSpawned;
    public List<GameObject> enemyPrefabs;
    public float distanceBetweenRooms = 25f;
    public bool canRepeatSpawnedRooms = false;
    public GameUIController gameUIController;

    [Header("--- Read only ---")]
    public string difficultyName;
    public int floorCount;
    public int maxEnemyWaveCountPerFloor = 1;
    public int minEnemyWaveCountPerFloor = 1;
    public float enemyDifficultyPoint;
    public float enemyDifficultyMultiplier;
    public Room currentRoom;
    public int currentRoomIndex;

    private PlayerStats player;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        player = PlayerStats.Instance;
        currentDifficulty = allDifficulties[PlayerPrefs.GetInt("dif")];
        if (currentDifficulty != null)
        {
            gameManager.currentDifficulty = currentDifficulty;
            difficultyName = currentDifficulty.difficultyName;
            floorCount = currentDifficulty.floorCount;
            maxEnemyWaveCountPerFloor = currentDifficulty.maxEnemyWaveCountPerFloor;
            minEnemyWaveCountPerFloor = currentDifficulty.minEnemyWaveCountPerFloor;
            enemyDifficultyPoint = currentDifficulty.enemyDifficultyPoint;
            enemyDifficultyMultiplier = currentDifficulty.enemyDifficultyMultiplier;
        }

        GenerateRooms();
        PlacePlayerInRoom(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (gameManager.currentGameState)
            {
                case GameState.PreBattle:
                    currentRoom.SpawnEnemies();
                    break;
                case GameState.Upgrading:
                    gameManager.currentGameState = GameState.Transitioning;
                    //transition
                    StartCoroutine(TransportToRoom(currentRoomIndex + 1));
                    break;
            }
        }
    }

    public void Respawn()
    {
        StartCoroutine(TransportToRoom(0, true));
    }

    private void GenerateRooms()
    {
        //safe checking for out of index problem when wanting no duplicate spawned rooms
        if (floorCount > roomPrefabs.Count)
            canRepeatSpawnedRooms = true;

        //spawning rooms, stacking on top of one another like a tower. First room is at y = 0
        for(int i = 0; i < floorCount; i++)
        {
            int chosenIndex = Random.Range(0, roomPrefabs.Count);
            Vector3 roomPosition = transform.position + Vector3.up * i * distanceBetweenRooms;
            Room room = Instantiate(roomPrefabs[chosenIndex], roomPosition, Quaternion.identity).GetComponent<Room>();
            room.roomIndex = i;

            if (!canRepeatSpawnedRooms)
            {
                //removing chosen room prefab from pool of rooms that can be generated - avoiding duplicate
                roomPrefabs.RemoveAt(chosenIndex);
            }

            roomsSpawned.Add(room);

            //set up room difficulty values
            room.difficultyPoint = enemyDifficultyPoint * Mathf.Pow(enemyDifficultyMultiplier, i);
            room.SetEnemyPrefabs(enemyPrefabs);

        }

        CameraController.Instance.roomCenter = roomsSpawned[0].transform;
    }

    private IEnumerator TransportToRoom(int roomIndex, bool respawnPlayer = false)
    {
        gameUIController.FadeInBlackBG(0.25f);
        yield return new WaitForSeconds(0.5f);

        if (respawnPlayer)
        {
            player.Respawn();
            currentRoom.CleanUpRoom();
        }

        //if last room
        if (roomIndex >= roomsSpawned.Count)
        {
            gameUIController.DisplayWinScreen();
            gameManager.currentGameState = GameState.Win;
        }
        else
        {
            PlacePlayerInRoom(roomIndex);
            gameUIController.FadeOutBlackBG(0.25f);
        }
        
    }

    private void PlacePlayerInRoom(int roomIndex)
    {
        player.transform.position = roomsSpawned[roomIndex].spawnPoint.position;
        currentRoom = roomsSpawned[roomIndex];
        currentRoomIndex = roomIndex;
        CameraController.Instance.roomCenter = currentRoom.transform;
        if (currentRoom.cleared)
        {
            gameManager.currentGameState = GameState.Upgrading;
        }
        else
            gameManager.currentGameState = GameState.PreBattle;
    }
}
