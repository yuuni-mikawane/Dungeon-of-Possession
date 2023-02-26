using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : SingletonBind<GameManager>
{
    public DifficultyScriptableObject currentDifficulty;
    public int currentRoomIndex;
    public List<Room> allRooms;
    public GameState currentGameState;
    private GameState previousGameState;
    public GameObject deathMsg;
    public TextMeshProUGUI msg;
    public TextMeshProUGUI msgCasualty;

    public int deathCount = 0;
    public float timer = 0f;
    private float timerStart;

    private void Start()
    {
        timerStart = Time.time;
    }

    private void Update()
    {
        if (currentGameState != GameState.Win)
        {
            timer = Time.time - timerStart;
        }
    }

    public void SendInNewWizard()
    {
        deathMsg.SetActive(false);
    }

    public void OnPlayerDie(string content)
    {
        deathCount++;
        msg.text = content;
        msgCasualty.text = "Casualty: " + deathCount;
        deathMsg.SetActive(true);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
